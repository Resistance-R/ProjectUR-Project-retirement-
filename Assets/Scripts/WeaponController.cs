using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponController : MonoBehaviour
{
    private Transform gunTransform;
    private SpriteRenderer gunSpriteRenderer;
    private aboutCamera cameraScript;

    public string weaponType;
    public float weaponDamage;

    public GameObject bulletPrefab;
    public GameObject dynamitePrefab;
    public Transform firePoint;

    private float fireRate = 0.5f;
    private float nextFire = 0f;

    private int pellets = 3;
    private float spreadAngle = 20f;
    private float bulletSpeed = 10f;


    void Start()
    {
        gunTransform = transform;
        gunSpriteRenderer = GetComponent<SpriteRenderer>();
        cameraScript = Camera.main.GetComponent<aboutCamera>();
    }

    void Update()
    {
        WeaponRotate();

        if (Input.GetMouseButtonDown(0) && Time.time > nextFire)
        {
            nextFire = Time.time + fireRate;
            Shoot();
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

    void Shoot()
    {
        if(weaponType == "SARevolver")
        {
            cameraScript.shakeMagnitude = 0.03f;
            StartCoroutine(ShakeForDuration(0.05f));
            Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
            GameManager.Instance.weaponDamage = 10f;
        }

        if(weaponType == "DARevolver")
        {
            cameraScript.shakeMagnitude = 0.03f;
            StartCoroutine(ShakeForDuration(0.05f));
            Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
            GameManager.Instance.weaponDamage = 15f;
        }

        if(weaponType == "DBShotgun")
        {
            for (int i = 0; i < pellets; i++)
            {
                float randomSpread = Random.Range(-spreadAngle, spreadAngle);

                GameObject bullet = Instantiate(bulletPrefab, firePoint.position, Quaternion.identity);

                bullet.transform.rotation = Quaternion.Euler(0, 0, firePoint.eulerAngles.z * randomSpread * 3000);

                Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
                rb.velocity = bullet.transform.right * bulletSpeed;
            }
            GameManager.Instance.weaponDamage = 10f;
            cameraScript.shakeMagnitude = 0.04f;
            StartCoroutine(ShakeForDuration(0.05f));
        }

        if(weaponType == "PAShotgun")
        {
            for (int i = 0; i < pellets; i++)
            {
                float randomSpread = Random.Range(-spreadAngle, spreadAngle);

                GameObject bullet = Instantiate(bulletPrefab, firePoint.position, Quaternion.identity);

                bullet.transform.rotation = Quaternion.Euler(0, 0, firePoint.eulerAngles.z * randomSpread * 3000);

                Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
                rb.velocity = bullet.transform.right * bulletSpeed;
            }
            GameManager.Instance.weaponDamage = 7f;
            cameraScript.shakeMagnitude = 0.04f;
            StartCoroutine(ShakeForDuration(0.05f));
        }

        if(weaponType == "LARifle")
        {
            cameraScript.shakeMagnitude = 0.03f;
            StartCoroutine(ShakeForDuration(0.05f));
            Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
            GameManager.Instance.weaponDamage = 20f;
        }

        if(weaponType == "Dynamite")
        {
            GameObject bomb = Instantiate(dynamitePrefab, firePoint.position, Quaternion.identity);
        }
    }
}