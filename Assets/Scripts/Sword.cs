using System;
using UnityEngine;

public class Sword : MonoBehaviour
{
    [SerializeField] private GameObject slashAnimPrefab;
    [SerializeField] private Transform slashAnimSpawnPoint;

    private PlayerControls playerControls;
    private Animator myAnimator;
    private Player player;
    private ActiveWeapon activeWeapon;

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
        playerControls.Combat.Attack.started += _ => Attack();
    }

    private void Update()
    {
        MouseFollowWithOffset();
    }

    private void Attack()
    {
        myAnimator.SetTrigger("Attack");

        slashAnim = Instantiate(slashAnimPrefab, slashAnimSpawnPoint.position, Quaternion.identity);
        slashAnim.transform.parent = this.transform.parent;
    }

    public void SlashUpAnimEvent()
    {
        FlipLeft_X_Angle(-180);
    }

    public void SlashDownAnimEvent()
    {
        FlipLeft_X_Angle(0);
    }

    private void FlipLeft_X_Angle(int xAngle)
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

        float angle = Mathf.Atan2(mousePosition.y, playerScreenPoint.x) * Mathf.Rad2Deg;

        if (playerScreenPoint.x > mousePosition.x)
        {
            activeWeapon.transform.rotation = Quaternion.Euler(0, 180, angle);
        }
        else
        {
            activeWeapon.transform.rotation = Quaternion.Euler(0, 0, angle);
        }
    }
}
