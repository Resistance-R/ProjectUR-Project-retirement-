using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUD : MonoBehaviour
{
    public enum InfoType{EXP, Lv, Kill, Time, HP, Sanity}
    public InfoType type;

    Text myText;
    Slider mySlider;

    private void Awake()
    {
        myText = GetComponent<Text>();
        mySlider = GetComponentInChildren<Slider>(); ;   
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
        }    
    }
}