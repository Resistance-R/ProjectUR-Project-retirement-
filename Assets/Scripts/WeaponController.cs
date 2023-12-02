using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponController : MonoBehaviour
{
    private Transform gunTransform;
    private SpriteRenderer gunSpriteRenderer;
    private aboutCamera cameraScript;
    private Coroutine reloadCoroutine;

    public string weaponType;
    public int currentAmmo;
    public GunAttributes gunAttributes;

    public GameObject bulletPrefab;
    public GameObject dynamitePrefab;
    public Transform firePoint;

    private int pellets = 3;
    private float spreadAngle = 20f;
    private float bulletSpeed = 10f;
    private bool isReloading = false;

    void Start()
    {
        gunTransform = transform;
        gunSpriteRenderer = GetComponent<SpriteRenderer>();
        cameraScript = Camera.main.GetComponent<aboutCamera>();
        InitializeGun();
        SetPlayerWeapon();
    }

    void Update()
    {
        WeaponRotate();

        if (Input.GetMouseButtonDown(0) && Time.time > gunAttributes.nextFire)
        {
            Shoot();
            gunAttributes.nextFire = Time.time + gunAttributes.fireRate;
        }

        if (Input.GetKeyDown(KeyCode.R) || currentAmmo == 0)
        {
            StartCoroutine(Reload());
        }

        if(!gameObject.activeSelf)
        {
            StopCoroutine(Reload());
            isReloading = false;
        }
    }

    void WeaponRotate()
    {
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePosition.z = 0f;
        Vector3 directionToCursor = mousePosition - gunTransform.position;

        float angle = Mathf.Atan2(directionToCursor.y, directionToCursor.x) * Mathf.Rad2Deg;

        gunTransform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));

        if (angle < 135f && angle >= 0f)
        {
            // 제 1사분면
            gunSpriteRenderer.flipX = false;
            gunSpriteRenderer.flipY = false;
            gunSpriteRenderer.sortingOrder = 9;
        }

        else if (angle >= 135f && angle <= 180f)
        {
            // 제 2사분면
            gunSpriteRenderer.flipX = false;
            gunSpriteRenderer.flipY = true;
            gunSpriteRenderer.sortingOrder = 9;
        }

        else if (angle >= -135f && angle < -45f)
        {
            // 제 4사분면
            gunSpriteRenderer.flipX = false;
            gunSpriteRenderer.flipY = false; 
            gunSpriteRenderer.sortingOrder = 11;
        }
        
        else if (angle >= 135f || angle < -135f)
        {
            // 제 3사분면
            gunSpriteRenderer.flipX = false;
            gunSpriteRenderer.flipY = true;
            gunSpriteRenderer.sortingOrder = 11;
        }
    }

    IEnumerator ShakeForDuration(float duration)
    {
        cameraScript.StartShake();
        yield return new WaitForSeconds(duration);
        cameraScript.StopShake();
    }

    IEnumerator Reload()
    {
        if (!isReloading && currentAmmo < gunAttributes.maxAmmo)
        {
            isReloading = true;
            currentAmmo = 0;

            if (reloadCoroutine != null)
            {
                StopCoroutine(reloadCoroutine);
            }

            reloadCoroutine = StartCoroutine(ReloadCoroutine());
            yield return reloadCoroutine;

            isReloading = false;
        }
    }
    
    IEnumerator ReloadCoroutine()
    {
        yield return new WaitForSeconds(gunAttributes.reloadTime);

        currentAmmo = gunAttributes.maxAmmo;
        reloadCoroutine = null;
    }

    public void InitializeGun()
    {
        currentAmmo = gunAttributes.maxAmmo;
        isReloading = false;
        gunAttributes.nextFire = 0f;
    }

    void SetPlayerWeapon()
    {
        Weapon SARevolver = new Weapon();
        SARevolver.name = "Single Action Revolver";
        SARevolver.damage = gunAttributes.weaponDamage;
        SARevolver.attributes = gunAttributes;

        Weapon DARevolver = new Weapon();
        DARevolver.name = "Double Action Revolver";
        DARevolver.damage = gunAttributes.weaponDamage;
        DARevolver.attributes = gunAttributes;

        Weapon DBShotgun = new Weapon();
        DBShotgun.name = "Double Barrel Shotgun";
        DBShotgun.damage = gunAttributes.weaponDamage;
        DBShotgun.attributes = gunAttributes;

        Weapon PAShotgun = new Weapon();
        PAShotgun.name = "Pump Action Shotgun";
        PAShotgun.damage = gunAttributes.weaponDamage;
        PAShotgun.attributes = gunAttributes;

        Weapon LARifle = new Weapon();
        LARifle.name = "Lever Action Rifle";
        LARifle.damage = gunAttributes.weaponDamage;
        LARifle.attributes = gunAttributes;

        Weapon Dynamite = new Weapon();
        Dynamite.name = "Dynamite";
        Dynamite.maxAmmo = gunAttributes.maxAmmo;
        Dynamite.attributes = gunAttributes;


        GameManager.Instance.availableWeapons.Add(SARevolver);
        GameManager.Instance.availableWeapons.Add(DARevolver);
        GameManager.Instance.availableWeapons.Add(DBShotgun);
        GameManager.Instance.availableWeapons.Add(PAShotgun);
        GameManager.Instance.availableWeapons.Add(LARifle);
        GameManager.Instance.availableWeapons.Add(Dynamite);
    }

    public void StopReloadCoroutine()
    {
        if (reloadCoroutine != null)
        {
            StopCoroutine(reloadCoroutine);
        }
    }

    public void ResetReloadingState()
    {
        isReloading = false;
    }

    private void Shoot()
    {
        if(weaponType == "SARevolver")
        {
            if(currentAmmo > 0)
            {
                currentAmmo--;
                cameraScript.shakeMagnitude = 0.03f;
                StartCoroutine(ShakeForDuration(0.05f));
                Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
                GameManager.Instance.weaponDamage = gunAttributes.weaponDamage;
            }
        }

        if(weaponType == "DARevolver")
        {
            if (currentAmmo > 0)
            {
                currentAmmo--;
                cameraScript.shakeMagnitude = 0.03f;
                StartCoroutine(ShakeForDuration(0.05f));
                Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
                GameManager.Instance.weaponDamage = gunAttributes.weaponDamage;
            }
        }

        if(weaponType == "DBShotgun")
        {
            if (currentAmmo > 0)
            {
                currentAmmo--;

                for (int i = 0; i < pellets; i++)
                {
                    float randomSpread = Random.Range(-spreadAngle, spreadAngle);

                    GameObject bullet = Instantiate(bulletPrefab, firePoint.position, Quaternion.identity);

                    bullet.transform.rotation = Quaternion.Euler(0, 0, firePoint.eulerAngles.z * randomSpread * 3000);

                    Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
                    rb.velocity = bullet.transform.right * bulletSpeed;
                }
                GameManager.Instance.weaponDamage = gunAttributes.weaponDamage;
                cameraScript.shakeMagnitude = 0.04f;
                StartCoroutine(ShakeForDuration(0.05f));
            }
        }

        if(weaponType == "PAShotgun")
        {
            if (currentAmmo > 0)
            {
                currentAmmo--;

                for (int i = 0; i < pellets; i++)
                {
                    float randomSpread = Random.Range(-spreadAngle, spreadAngle);

                    GameObject bullet = Instantiate(bulletPrefab, firePoint.position, Quaternion.identity);

                    bullet.transform.rotation = Quaternion.Euler(0, 0, firePoint.eulerAngles.z * randomSpread * 3000);

                    Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
                    rb.velocity = bullet.transform.right * bulletSpeed;
                }
                GameManager.Instance.weaponDamage = gunAttributes.weaponDamage;
                cameraScript.shakeMagnitude = 0.04f;
                StartCoroutine(ShakeForDuration(0.05f));
            }
        }

        if(weaponType == "LARifle")
        {
            if (currentAmmo > 0)
            {
                currentAmmo--;
                cameraScript.shakeMagnitude = 0.03f;
                StartCoroutine(ShakeForDuration(0.05f));
                Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
                GameManager.Instance.weaponDamage = gunAttributes.weaponDamage;
            }
        }

        if(weaponType == "Dynamite")
        {
            if(currentAmmo > 0)
            {
                currentAmmo--;
                GameObject bomb = Instantiate(dynamitePrefab, firePoint.position, Quaternion.identity);
            }
        }
    }
}