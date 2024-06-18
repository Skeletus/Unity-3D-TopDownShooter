using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimationEvents : MonoBehaviour
{
    private PlayerWeaponVisuals weaponVisualController;

    private void Start()
    {
        weaponVisualController = GetComponentInParent<PlayerWeaponVisuals>();
    }

    public void ReloadIsOver()
    {
        weaponVisualController.MaximizeRigWeight();

        // re-fill bullet
    }

    public void ReturnRig()
    {
        weaponVisualController.MaximizeRigWeight();
        weaponVisualController.MaximizeLeftHandWeight();
    }

    public void WeaponGrabIsOver()
    {
        weaponVisualController.SetBusyGrabbingWeapon(false);
    }
}
