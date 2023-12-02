using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewGunAttributes", menuName = "Gun System/Gun Attributes")]
public class GunAttributes : ScriptableObject
{
    public string weaponName;
    public int maxAmmo;
    public float reloadTime;
    public float nextFire;
    public float fireRate;
    public float weaponDamage;
    public int enhancedDamage;

    void Start()
    {
        
    }

    void Update()
    {
        
    }
}
