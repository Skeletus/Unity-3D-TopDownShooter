using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWeaponController : MonoBehaviour
{
    private Player playerControls;

    private void Start()
    {
        playerControls = GetComponent<Player>();
        playerControls.controls.Character.Fire.performed += context => Shoot();
    }
    private void Shoot()
    {
        GetComponentInChildren<Animator>().SetTrigger("Fire");
    }
}
