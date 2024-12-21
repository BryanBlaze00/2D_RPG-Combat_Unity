using System;
using System.Collections;
using UnityEngine;

public class Sword : MonoBehaviour
{
    [SerializeField] private GameObject slashAnimPrefab;
    [SerializeField] private Transform slashAnimSpawnPoint;
    [SerializeField] private Transform swordCollider;
    [SerializeField] private float swordAttackCD = 0.5f;

    private PlayerControls playerControls;
    private Animator myAnimator;
    private Player player;
    private ActiveWeapon activeWeapon;
    private bool attackButtonDown, isAttacking = false;

    private GameObject slashAnim;

    private void Awake()
    {
        playerControls = new PlayerControls();
        myAnimator = GetComponent<Animator>();
        player = GetComponentInParent<Player>();
        activeWeapon = GetComponentInParent<ActiveWeapon>();
    }

    private void OnEnable()
    {
        playerControls.Enable();
    }

    private void Start()
    {
        playerControls.Combat.Attack.started += _ => StartAttacking();
        playerControls.Combat.Attack.canceled += _ => StopAttacking();
    }

    private void Update()
    {
        MouseFollowWithOffset();
        Attack();
    }

    private void StartAttacking()
    {
        attackButtonDown = true;
    }

    private void StopAttacking()
    {
        attackButtonDown = false;
    }

    private void Attack()
    {
        if (attackButtonDown && !isAttacking)
        {
            isAttacking = true;
            myAnimator.SetTrigger("Attack");
            swordCollider.gameObject.SetActive(true);

            slashAnim = Instantiate(slashAnimPrefab, slashAnimSpawnPoint.position, Quaternion.identity);
            slashAnim.transform.parent = this.transform.parent;

            StartCoroutine(AttackCDRoutine());
        }
    }

    private IEnumerator AttackCDRoutine()
    {
        yield return new WaitForSeconds(swordAttackCD);
        isAttacking = false;        
    }

    public void DoneAttackingAnimEvent() => swordCollider.gameObject.SetActive(false);

    public void SlashUpAnimEvent()
    {
        AnimFlipLeft_and_xAngle(-180);
    }

    public void SlashDownAnimEvent()
    {
        AnimFlipLeft_and_xAngle(0);
    }

    private void AnimFlipLeft_and_xAngle(int xAngle)
    {
        slashAnim.transform.rotation = Quaternion.Euler(xAngle, 0, 0);

        if (player.FacingLeft)
        {
            slashAnim.GetComponent<SpriteRenderer>().flipX = true;
        }
    }

    private void MouseFollowWithOffset()
    {
        Vector3 playerScreenPoint = Camera.main.WorldToScreenPoint(player.transform.position);
        Vector3 mousePosition = Input.mousePosition;

        /*
            *** Weapon will follow the mouse with offset ***

            * atan2(y,x) returns the angle whose tangent is the quotient of two specified numbers.
            * The return value is the angle in radians in the range of -pi to pi.
            * The atan2 function is useful in calculating the angle from a specified point to the origin.
            * The following example calculates the angle from the point (x, y) to the origin.
            * float angle = Mathf.Atan2(y, x) * Mathf.Rad2Deg;
        */
        float angle = Mathf.Atan2(mousePosition.y, playerScreenPoint.x) * Mathf.Rad2Deg;

        if (playerScreenPoint.x > mousePosition.x)
        {
            activeWeapon.transform.rotation = Quaternion.Euler(0, 180, angle);
            swordCollider.transform.rotation = Quaternion.Euler(0, 180, 0);
        }
        else
        {
            activeWeapon.transform.rotation = Quaternion.Euler(0, 0, angle);
            swordCollider.transform.rotation = Quaternion.Euler(0, 0, 0);
        }
    }
}
