using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class aboutCamera : MonoBehaviour
{
    private Transform originalCameraTransform;
    private bool isShaking = false;

    private float shakeDuration = 0.3f;
    public float shakeMagnitude = 0.1f;

    private void Start()
    {
        originalCameraTransform = transform;
    }

    private void Update()
    {
        if (isShaking)
        {
            Vector3 shakePosition = originalCameraTransform.position + Random.insideUnitSphere * shakeMagnitude;
            transform.position = shakePosition;
        }
    }

    public void StartShake()
    {
        isShaking = true;
        Invoke("StopShake", shakeDuration);
    }

    public void StopShake()
    {
        isShaking = false;
        transform.position = originalCameraTransform.position;
    }
}