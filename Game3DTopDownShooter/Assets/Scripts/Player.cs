using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public PlayerControls controls { get; private set; }
    public PlayerAim playerAim { get; private set; } // read only
    public PlayerMovement movement { get; private set; }

    private void Awake()
    {
        controls = new PlayerControls();
        playerAim = GetComponent<PlayerAim>();
        movement = GetComponent<PlayerMovement>();
    }

    private void OnEnable()
    {
        controls.Enable();
    }

    private void OnDisable()
    {
        controls.Disable();
    }
}
