using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointerGenerator : MonoBehaviour
{
    [SerializeField]
    private Sprite cursorSprite;

    private SpriteRenderer spriter;

    void Start()
    {
        Cursor.visible = false;

        GameObject cursorObject = new GameObject("MousePointer");
        spriter = cursorObject.AddComponent<SpriteRenderer>();
        spriter.sprite = cursorSprite;
    }

    void Update()
    {
        Vector3 cursorPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        cursorPosition.z = 0f;

        spriter.transform.position = cursorPosition;
    }
}
