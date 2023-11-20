using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUD : MonoBehaviour
{
    public enum InfoType{EXP, Lv, Kill, Time, HP, Sanity, curAmmo, maxAmmo}
    public InfoType type;

    private PlayerController PC;

    Text myText;
    Slider mySlider;

    private void Awake()
    {
        myText = GetComponent<Text>();
        mySlider = GetComponentInChildren<Slider>();
    }

    private void LateUpdate()
    {

        switch (type) 
        {
            case InfoType.EXP:

                break;

            case InfoType.Lv:

                break;

            case InfoType.Kill:

                break;

            case InfoType.Time:

                break;

            case InfoType.HP:

                PlayerController playerControllerHP = FindObjectOfType<PlayerController>();
                float curHP = playerControllerHP.HP;
                mySlider.value = curHP;

                break;

            case InfoType.Sanity:

                PlayerController playerControllerSanity = FindObjectOfType<PlayerController>();
                float curSaity = playerControllerSanity.Sanity;
                mySlider.value = curSaity;

                break;

            case InfoType.curAmmo:

                    if(GameManager.Instance.isLive)
                    {
                        WeaponController activeWeapon = FindObjectOfType<WeaponController>();
                        myText.text = activeWeapon.currentAmmo.ToString();
                    }

                break;

            case InfoType.maxAmmo:

                if(GameManager.Instance.isLive)
                {
                    WeaponController activeWeaponMaxAmmo = GetActiveWeapon();
                    myText.text = activeWeaponMaxAmmo.gunAttributes.maxAmmo.ToString();
                }

                break;
        }    
    }

    private WeaponController GetActiveWeapon()
    {
        WeaponController[] weapons = FindObjectsOfType<WeaponController>();
        foreach (var weapon in weapons)
        {
            if (weapon.gameObject.activeSelf)
            {
                return weapon;
            }
        }
        return null;
    }
}