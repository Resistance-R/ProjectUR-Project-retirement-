using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewGunAttributes", menuName = "Gun System/Gun Attributes")]
public class GunAttributes : ScriptableObject
{
    public int maxAmmo;
    public float reloadTime;
    public float nextFire;
    public float fireRate;

    void Start()
    {
        
    }

    void Update()
    {
        
    }
}
