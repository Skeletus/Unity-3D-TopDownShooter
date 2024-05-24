using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponVisualController : MonoBehaviour
{
    [SerializeField] private Transform[] gunsList;

    [SerializeField] private Transform pistol;
    [SerializeField] private Transform revolver;
    [SerializeField] private Transform autoRifle;
    [SerializeField] private Transform shotgun;
    [SerializeField] private Transform rifle;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1)) // 1 in keyboard
        {
            SwitchOn(pistol);
        }
        if (Input.GetKeyDown(KeyCode.Alpha2)) // 2 in keyboard
        {
            SwitchOn(revolver);
        }
        if (Input.GetKeyDown(KeyCode.Alpha3)) // 3 in keyboard
        {
            SwitchOn(autoRifle);
        }
        if (Input.GetKeyDown(KeyCode.Alpha4)) // 4 in keyboard
        {
            SwitchOn(shotgun);
        }
        if (Input.GetKeyDown(KeyCode.Alpha5)) // 5 in keyboard
        {
            SwitchOn(rifle);
        }
    }

    private void SwitchOn(Transform gun)
    {
        SwitchOffGuns(); // desactivate all guns
        // activate given gun
        gun.gameObject.SetActive(true);
    }

    private void SwitchOffGuns()
    {
        for(int i = 0; i < gunsList.Length; i++)
        {
            gunsList[i].gameObject.SetActive(false);
        }
    }
}
