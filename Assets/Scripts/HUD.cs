using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUD : MonoBehaviour
{
    public enum InfoType{EXP, Lv, Kill, Time, HP, Sanity, curAmmo, maxAmmo, selectedWeapon0 ,selectedWeapon1, selectedWeapon2}
    public InfoType type;
    
    private PlayerController PC;
    private bool lvUp = false;

    Text myText;
    Slider mySlider;

    private void Awake()
    {
        myText = GetComponent<Text>();
        mySlider = GetComponentInChildren<Slider>();
    }

    private void LateUpdate()
    {
        lvUp = GameManager.Instance.isLvUp;
        HUDControl();
    }

    private void HUDControl()
    {
        switch (type)
        {
            case InfoType.EXP:
                float curExp = GameManager.Instance.exp;
                float maxExp = GameManager.Instance.nextExp[GameManager.Instance.level];
                mySlider.value = curExp / maxExp;

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

                if (GameManager.Instance.isLive)
                {
                    WeaponController activeWeapon = FindObjectOfType<WeaponController>();
                    myText.text = activeWeapon.currentAmmo.ToString();
                }

                break;

            case InfoType.maxAmmo:

                if (GameManager.Instance.isLive)
                {
                    WeaponController activeWeaponMaxAmmo = GetActiveWeapon();
                    myText.text = activeWeaponMaxAmmo.gunAttributes.maxAmmo.ToString();
                }

                break;

            case InfoType.selectedWeapon0:

                List<Weapon> randomWeapons0 = GameManager.Instance.selectedWeapons;

                if(randomWeapons0.Count >= 3)
                {
                    myText.text = randomWeapons0[0].name;
                }

                break;

            case InfoType.selectedWeapon1:

                List<Weapon> randomWeapons1 = GameManager.Instance.selectedWeapons;

                if (randomWeapons1.Count >= 3)
                {
                    myText.text = randomWeapons1[1].name;
                }

                break;

            case InfoType.selectedWeapon2:

                List<Weapon> randomWeapons2 = GameManager.Instance.selectedWeapons;

                if (randomWeapons2.Count >= 3)
                {
                    myText.text = randomWeapons2[2].name;
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