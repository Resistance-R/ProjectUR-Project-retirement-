using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponController : MonoBehaviour
{
    private Transform gunTransform;
    private SpriteRenderer gunSpriteRenderer;

    void Start()
    {
        gunTransform = transform;
        gunSpriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        WeaponRotate();
    }

    void WeaponRotate()
    {
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePosition.z = 0f;
        Vector3 directionToCursor = mousePosition - gunTransform.position;

        float angle = Mathf.Atan2(directionToCursor.y, directionToCursor.x) * Mathf.Rad2Deg;

        gunTransform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));

        if(angle < 90f && angle >= 0f)
        {
            //제 1사분면
            gunSpriteRenderer.flipX = false;
            gunSpriteRenderer.flipY = false;
            gunSpriteRenderer.sortingOrder = 9;
        }

        else if(angle >= 90f && angle <= 180f)
        {
            //제 2사분면
            gunSpriteRenderer.flipX = false;
            gunSpriteRenderer.flipY = true;
            gunSpriteRenderer.sortingOrder = 9;
        }

        else if(angle >= -90f || angle < -0f)
        {
            //제 3사분면
            gunSpriteRenderer.flipX = false;
            gunSpriteRenderer.flipY = true;
            gunSpriteRenderer.sortingOrder = 11;
        }

        else if(angle >= -90f && angle < 90f)
        {
            //제 4사분면
            gunSpriteRenderer.flipX = false;
            gunSpriteRenderer.flipY = false;
            gunSpriteRenderer.sortingOrder = 11;
        }
    }
}