using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations.Rigging;

public enum GrabType
{
    SideGrab,
    BackGrab
};

public class WeaponVisualController : MonoBehaviour
{
    private Animator animator;
    private bool isGrabbingWeapon;
    private Rig rig;

    #region Gun transform region
    [SerializeField] private Transform[] gunsList;

    [SerializeField] private Transform pistol;
    [SerializeField] private Transform revolver;
    [SerializeField] private Transform autoRifle;
    [SerializeField] private Transform shotgun;
    [SerializeField] private Transform rifle;

    private Transform currentGun;
    #endregion

    [Header("Rig")]
    [SerializeField] private float rigWeightIncreaseRate;
    private bool shouldIncrease_RigWeight;

    [Header("Left Hand IK")]
    [SerializeField] private TwoBoneIKConstraint leftHandIK;
    [SerializeField] private Transform leftHandIKTarget;
    [SerializeField] private float leftHandIKWeightIncreaseRate;
    private bool shouldIncrease_LeftHandIKWeight;



    private void Start()
    {
        animator = GetComponentInChildren<Animator>();
        rig = GetComponentInChildren<Rig>();
        SwitchOn(pistol);
    }

    private void Update()
    {
        WeaponSwitch();

        if (Input.GetKeyDown(KeyCode.R) && isGrabbingWeapon == false)
        {
            animator.SetTrigger("Reload");
            ReduceRigWeight();
        }

        UpdateRigWeight();
        UpdateLeftHandIKWeight();
    }

    private void UpdateLeftHandIKWeight()
    {
        if (shouldIncrease_LeftHandIKWeight)
        {
            leftHandIK.weight += leftHandIKWeightIncreaseRate * Time.deltaTime;

            if (leftHandIK.weight >= 1)
            {
                shouldIncrease_LeftHandIKWeight = false;
            }
        }
    }

    private void UpdateRigWeight()
    {
        if (shouldIncrease_RigWeight)
        {
            rig.weight += rigWeightIncreaseRate * Time.deltaTime;

            if (rig.weight >= 1)
            {
                shouldIncrease_RigWeight = false;
            }
        }
    }

    private void ReduceRigWeight()
    {
        rig.weight = .15f;
    }

    private void PlayWeaponGrabAnimation(GrabType grabType)
    {
        leftHandIK.weight = 0f;
        ReduceRigWeight();
        animator.SetFloat("WeaponGrabType", ((float)grabType));
        animator.SetTrigger("WeaponGrab");
        SetBusyGrabbingWeapon(true);
    }

    public void SetBusyGrabbingWeapon(bool isBusy)
    {
        isGrabbingWeapon = isBusy;
        animator.SetBool("BusyGrabbingWeapon", isGrabbingWeapon);
    }

    public void MaximizeRigWeight()
    {
        shouldIncrease_RigWeight = true;
    }

    public void MaximizeLeftHandWeight()
    {
        shouldIncrease_LeftHandIKWeight = true;
    }

    private void WeaponSwitch()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1)) // 1 in keyboard
        {
            SwitchOn(pistol);
            SwitchAnimationLayer(1);
            PlayWeaponGrabAnimation(GrabType.SideGrab);
        }
        if (Input.GetKeyDown(KeyCode.Alpha2)) // 2 in keyboard
        {
            SwitchOn(revolver);
            SwitchAnimationLayer(1);
            PlayWeaponGrabAnimation(GrabType.SideGrab);
        }
        if (Input.GetKeyDown(KeyCode.Alpha3)) // 3 in keyboard
        {
            SwitchOn(autoRifle);
            SwitchAnimationLayer(1);
            PlayWeaponGrabAnimation(GrabType.BackGrab);
        }
        if (Input.GetKeyDown(KeyCode.Alpha4)) // 4 in keyboard
        {
            SwitchOn(shotgun);
            SwitchAnimationLayer(2);
            PlayWeaponGrabAnimation(GrabType.BackGrab);
        }
        if (Input.GetKeyDown(KeyCode.Alpha5)) // 5 in keyboard
        {
            SwitchOn(rifle);
            SwitchAnimationLayer(3);
            PlayWeaponGrabAnimation(GrabType.BackGrab);
        }
    }

    private void SwitchOn(Transform gunTransform)
    {
        SwitchOffGuns(); // desactivate all guns
        // activate given gun
        gunTransform.gameObject.SetActive(true);
        currentGun = gunTransform;

        AttachLeftHand();
    }

    private void SwitchOffGuns()
    {
        for(int i = 0; i < gunsList.Length; i++)
        {
            gunsList[i].gameObject.SetActive(false);
        }
    }

    private void AttachLeftHand()
    {
        Transform targetTransform = currentGun.GetComponentInChildren<LeftHandTargetTransform>().transform;

        leftHandIKTarget.localPosition = targetTransform.localPosition;
        leftHandIKTarget.localRotation = targetTransform.localRotation;
    }

    private void SwitchAnimationLayer(int layerIndex)
    {
        for (int i = 0; i < animator.layerCount; i++)
        {
            animator.SetLayerWeight(i, 0);
        }

        animator.SetLayerWeight(layerIndex, 1);
    }
}
