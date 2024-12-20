using System;
using UnityEngine;

public class Sword : MonoBehaviour
{
    private PlayerControls playerControls;
    private Animator myAnimator;
    private Player player;
    private ActiveWeapon activeWeapon;

    private void Awake() {
        playerControls = new PlayerControls();
        myAnimator = GetComponent<Animator>();
        player = GetComponentInParent<Player>();
        activeWeapon = GetComponentInParent<ActiveWeapon>();
    }

    private void OnEnable() {
        playerControls.Enable();
    }

    private void Start() {
        playerControls.Combat.Attack.started += _ => Attack();
    }

    private void Update() {
        MouseFollowWithOffset();
    }

    private void Attack()
    {
        myAnimator.SetTrigger("Attack");
    }

    private void MouseFollowWithOffset()
    {
        Vector3 playerScreenPoint = Camera.main.WorldToScreenPoint(player.transform.position);
        Vector3 mousePosition = Input.mousePosition;

        float angle = Mathf.Atan2(mousePosition.y, playerScreenPoint.x) * Mathf.Rad2Deg;

        if (playerScreenPoint.x > mousePosition.x)
            activeWeapon.transform.rotation = Quaternion.Euler(0, 180, angle);
        else
            activeWeapon.transform.rotation = Quaternion.Euler(0, 0, angle);
    }
}
