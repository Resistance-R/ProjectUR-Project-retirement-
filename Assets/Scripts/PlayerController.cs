using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


public class PlayerController : MonoBehaviour
{
    [SerializeField]
    private Vector2 inputVec;

    [SerializeField]
    private float speed = 2.5f;

    [SerializeField]
    private Sprite[] spriteArray;

    private Rigidbody2D myRigid;
    private SpriteRenderer spriter;
    private Vector3 mousePosition;

    void Awake()
    {
        myRigid = GetComponent<Rigidbody2D>();
        spriter = GetComponent<SpriteRenderer>();
    }


    void FixedUpdate() 
    {
        Vector2 nextVec = inputVec * speed * Time.fixedDeltaTime;
     myRigid.MovePosition(myRigid.position + nextVec);   
    }

    void OnMove(InputValue value)
    {
        inputVec = value.Get<Vector2>();
    }

    void LateUpdate() 
    {
        LookAtMouse();
    }

    void LookAtMouse()
    {
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector3 relativePosition = mousePosition - transform.position;
        float angle = Mathf.Atan2(relativePosition.y, relativePosition.x) * Mathf.Rad2Deg;

        if (angle < -45f && angle >= -135f)
        {
            // 왼쪽 위 영역
            spriter.sprite = spriteArray[0];
            spriter.flipX = true;
            // 뒤를 돈 캐릭터의 스프라이트를 표시 (회전 필요)   
            transform.rotation = Quaternion.Euler(0f, 180f, -angle);
        }
        else if (angle >= -45f && angle < 45f)
        {
            // 오른쪽 위 영역
            spriter.sprite = spriteArray[0];
            // 뒤를 돈 스프라이트를 X축으로 Flip하여 표시
            transform.rotation = Quaternion.Euler(0f, 180f, 0f);
            transform.localScale = new Vector3(-1f, 1f, 1f);
        }
        else if (angle >= 45f && angle < 135f)
        {
            // 오른쪽 아래 영역
            spriter.sprite = spriteArray[1];
            // 정면을 보는 스프라이트를 표시
            transform.rotation = Quaternion.identity;
            transform.localScale = new Vector3(1f, 1f, 1f);
        }
        else
        {
            // 왼쪽 아래 영역
            spriter.flipX = true;
            transform.rotation = Quaternion.identity;
            transform.localScale = new Vector3(-1f, 1f, 1f);
        }
    }
}
