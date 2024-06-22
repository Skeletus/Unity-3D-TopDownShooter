using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWeaponController : MonoBehaviour
{
    private Player playerControls;

    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private float bulletSpeed;
    [SerializeField] private Transform gunPoint;

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
