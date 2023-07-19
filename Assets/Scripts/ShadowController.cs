using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShadowController : MonoBehaviour
{
    [SerializeField]
    private Vector3 offset = new Vector3(0f, -0.4f, 0f);

    private Transform monsterTransform;
    private Transform shadowTransform;

    private void Start()
    {
        monsterTransform = transform.parent;

        shadowTransform = GetComponent<Transform>();
    }

    private void LateUpdate()
    {
        shadowTransform.position = monsterTransform.position + offset;
    }
}