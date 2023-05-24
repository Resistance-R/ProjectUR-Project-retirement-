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
    private Animator anim;

    void Awake()
    {
        myRigid = GetComponent<Rigidbody2D>();
        spriter = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
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
        anim.SetFloat("Speed", inputVec.magnitude);
        anim.SetBool("isLookingBack", false);
        
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector3 relativePosition = mousePosition - transform.position;
        float angle = Mathf.Atan2(relativePosition.y, relativePosition.x) * Mathf.Rad2Deg;

        if (angle < 135f && angle >= 0f)
        {
            //  제 1사분면
            anim.SetBool("isLookingBack", true);

            if (inputVec.magnitude != 0)
            {
            anim.Play("BackWalk");
            }

            else
            {
                spriter.sprite = spriteArray[0];
            }

            spriter.flipX = false;
            transform.rotation = Quaternion.identity;
        }

        else if (angle >= 135f && angle <= 180f)
        {
            // 제 2사분면
            anim.SetBool("isLookingBack", true);

            if (inputVec.magnitude != 0)
            {
                anim.Play("BackWalk");
            }

            else
            {
                spriter.sprite = spriteArray[0];
            }

            spriter.flipX = true;
            transform.rotation = Quaternion.identity;
        }

        else if (angle >= 135f || angle < -135f)
        {
            // 제 3사분면
            anim.SetBool("isLookingBack", false);

            if (inputVec.magnitude != 0)
            {
                anim.Play("Walk");
            }

            else
            {
                anim.Play("Idle");
            }

            spriter.flipX = true;
            transform.rotation = Quaternion.identity;
        }

        else if (angle >= -135f && angle < -45f)
        {
            // 제 4사분면
            anim.SetBool("isLookingBack", false);

            if (inputVec.magnitude != 0)
            {
                anim.Play("Walk");
            }

            else
            {
                anim.Play("Idle");
            }
            
            spriter.flipX = false;
            transform.rotation = Quaternion.identity;
        }
    }
}