using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponSwitcher : MonoBehaviour
{
    public GameObject[] weapons;
    private int currentWeaponIndex = 0;

    private void Start()
    {
        SwitchWeapon(currentWeaponIndex);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            SwitchWeapon(0);
        }

        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            SwitchWeapon(1);
        }

        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            SwitchWeapon(2);
        }

        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            SwitchWeapon(3);
        }

        if (Input.GetKeyDown(KeyCode.Alpha5))
        {
            SwitchWeapon(4);
        }

        if (Input.GetKeyDown(KeyCode.Alpha6))
        {
            SwitchWeapon(5);
        }
    }

    public void SwitchWeapon(int newIndex)
    {
        WeaponController currentWeapon = weapons[currentWeaponIndex].GetComponent<WeaponController>();

        if (currentWeapon != null)
        {
            currentWeapon.StopReloadCoroutine();
            currentWeapon.ResetReloadingState();
        }

        weapons[currentWeaponIndex].SetActive(false);

        weapons[newIndex].SetActive(true);

        currentWeaponIndex = newIndex;
    }
}
