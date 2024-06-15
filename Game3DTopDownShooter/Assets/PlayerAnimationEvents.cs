using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimationEvents : MonoBehaviour
{
    private WeaponVisualController weaponVisualController;

    private void Start()
    {
        weaponVisualController = GetComponentInParent<WeaponVisualController>();
    }

    public void ReloadIsOver()
    {
        weaponVisualController.ReturnRigWeightToOne();

        // re-fill bullet
    }

    public void WeaponGrabIsOver()
    {
        weaponVisualController.ReturnRigWeightToOne();
    }
}
