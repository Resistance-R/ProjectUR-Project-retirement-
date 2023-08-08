using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;


public class PlayerController : MonoBehaviour
{
    [SerializeField]
    private Vector2 inputVec;

    [SerializeField]
    private float speed = 2.5f;

    [SerializeField]
    private Sprite[] spriteArray;

    [Header("player's info")]
    public float HP;
    public float maxHP = 100;
    public float Sanity;
    public float maxSanity = 250;
    public int Lv;
    public int kill;
    public int EXP;
    public int[] nextEXP = {3, 5, 10, 100, 150, 210, 280, 360, 450, 600};

    private bool isInGrass;
    private float sanityDecreaseRate;

    private GameManager gameManager;
    private VignetteController vignetteController;
    private Rigidbody2D myRigid;
    private SpriteRenderer spriter;
    private Vector3 mousePosition;
    private Animator anim;

    void Awake()
    {
        myRigid = GetComponent<Rigidbody2D>();
        spriter = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();

        vignetteController = GetComponentInChildren<VignetteController>();
        gameManager = GameManager.Instance;
        HP = maxHP;
        Sanity = maxSanity;

        sanityDecreaseRate = 0.5f;
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
        Sanity -= sanityDecreaseRate * Time.deltaTime;
    }

    void Update()
    {
        if (Sanity <= 0)
        {
            gameManager.HandleSanityChange(Sanity);
        }
    }

    void LookAtMouse()
    {
        anim.SetFloat("Speed", inputVec.magnitude);
        anim.SetBool("isLookingBack", false);

        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector3 relativePosition = mousePosition - transform.position;
        float angle = Mathf.Atan2(relativePosition.y, relativePosition.x) * Mathf.Rad2Deg;

        if(GameManager.Instance.isLive == true)
        {
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

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Grass"))
        {
            vignetteController.ShowVignette();
            speed = 1.5f;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Grass"))
        {
            vignetteController.HideVignette();
            speed = 2.5f;
        }
    }

    void OnCollisionStay2D(Collision2D other)
    {
        if(other.collider.tag == "Zombie" || other.collider.tag == "Skelerton")
        {
            if(!GameManager.Instance.isLive)
            {
                return;
            }

            HP -= Time.deltaTime * 10f;

            if(HP < 0)
            {
                for(int index = 2; index < transform.childCount; index++)
                {
                    transform.GetChild(index).gameObject.SetActive(false);
                }

                anim.SetTrigger("Dead");
                GameManager.Instance.isLive = false;

                myRigid.constraints = RigidbodyConstraints2D.FreezePosition;
            }
        }
    }
}