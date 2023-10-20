using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponController : MonoBehaviour
{
    private Transform gunTransform;
    private SpriteRenderer gunSpriteRenderer;

    public string weaponType;

    public GameObject bulletPrefab;
    public Transform firePoint;

    private float fireRate = 0.5f;
    private float nextFire = 0f;

    void Start()
    {
        gunTransform = transform;
        gunSpriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        firePoint = this.gameObject.transform;

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

    void Shoot()
    {
        if (weaponType == "SARevolver")
        {
            Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        }
        if (weaponType == "DARevolver")
        {
            // 예를 들어, 발사 간격을 길게 설정
            Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        }
    }
}