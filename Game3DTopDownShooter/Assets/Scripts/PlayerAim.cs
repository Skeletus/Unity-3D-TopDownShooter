using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAim : MonoBehaviour
{
    private Player player;
    private PlayerControls controls;

    [Header("Aim visuals")]
    // this component is on the weapon holder (child of player)
    [SerializeField] private LineRenderer aimLaser;

    [Header("Aim info")]
    [SerializeField] private Transform aim;

    [SerializeField] private bool isAimingPrecisly;
    [SerializeField] private bool isLockingToTarget;

    [Header("Camera control")]
    [SerializeField] private Transform cameraTarget;

    [Range(.5f, 1f)]
    [SerializeField] private float minCameraDistance = 1.5f;

    [Range(1f, 3f)]
    [SerializeField] private float maxCameraDistance = 4f;

    [Range(3f, 5f)]
    [SerializeField] private float cameraSensitivity = 5f;

    [Space]

    [SerializeField] private LayerMask aimLayerMask;

    private Vector2 mouseInput;
    private RaycastHit lastKnowMouseHit;

    private void Start()
    {
        player = GetComponent<Player>();
        AssignInputEvents();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            isAimingPrecisly = !isAimingPrecisly;
        }
        if (Input.GetKeyDown(KeyCode.L))
        {
            isLockingToTarget = !isLockingToTarget;
        }

        UpdateAimVisuals();
        UpdateAimPosition();
        UpdateCameraPosition();
    }


    private void UpdateAimVisuals()
    {
        Transform gunPoint = player.weapon.GetGunPoint();
        Vector3 laserDirection = player.weapon.BulletDirection();

        float laserTipLength = .5f;
        float gunDistance = 4f;

        Vector3 endPoint = gunPoint.position + laserDirection * gunDistance;

        if(Physics.Raycast(gunPoint.position, laserDirection, out RaycastHit hit, gunDistance))
        {
            endPoint = hit.point;
            laserTipLength = 0;
        }

        aimLaser.SetPosition(0, gunPoint.position);
        aimLaser.SetPosition(1, endPoint);
        aimLaser.SetPosition(2, endPoint + laserDirection * laserTipLength);
    }
    private void UpdateAimPosition()
    {
        Transform target = GetTarget();

        if(target != null && isLockingToTarget)
        {
            if (target.GetComponent<Renderer>() != null)
            {
                aim.position = target.GetComponent<Renderer>().bounds.center;
            }
            else
            {
                aim.position = target.position;
            }
        }

        aim.position = GetMouseHitInfo().point;

        if (!isAimingPrecisly)
        {
            aim.position = new Vector3(aim.position.x, transform.position.y + 1, aim.position.z);
        }
    }

    #region GET METHODS
    public Transform GetAim() => aim;
    public Transform GetTarget()
    {
        Transform target = null;

        if (GetMouseHitInfo().transform.GetComponent<Target>() != null)
        {
            target = GetMouseHitInfo().transform;
        }

        return target;
    }
    #endregion

    public bool CanAimPrecisly() => isAimingPrecisly;


    public RaycastHit GetMouseHitInfo()
    {
        Ray ray = Camera.main.ScreenPointToRay(mouseInput);

        if (Physics.Raycast(ray, out var hitInfo, Mathf.Infinity, aimLayerMask))
        {
            lastKnowMouseHit = hitInfo;
            return hitInfo;
        }

        return lastKnowMouseHit;
    }

    #region CAMERA REGION
    private void UpdateCameraPosition()
    {
        cameraTarget.position = Vector3.Lerp(
                    cameraTarget.position,
                    DesiredCameraPosition(),
                    cameraSensitivity * Time.deltaTime);
    }
    private Vector3 DesiredCameraPosition()
    {
        float actualMaxCameraDistance = player.movement.moveInput.y < -.5f ? minCameraDistance : maxCameraDistance;
        
        Vector3 desiredCameraPosition = GetMouseHitInfo().point;
        Vector3 aimDirection = (desiredCameraPosition - transform.position).normalized;

        float distanceToDesiredPosition = Vector3.Distance(transform.position, desiredCameraPosition);
        float clampedDistance = Mathf.Clamp(distanceToDesiredPosition, minCameraDistance, actualMaxCameraDistance);

        desiredCameraPosition = transform.position + aimDirection * clampedDistance;
        desiredCameraPosition.y = transform.position.y + 1;

        return desiredCameraPosition;
    }
    #endregion

    private void AssignInputEvents()
    {
        controls = player.controls;

        controls.Character.Aim.performed += context => mouseInput = context.ReadValue<Vector2>();
        controls.Character.Aim.canceled += context => mouseInput = Vector2.zero;
    }
}
