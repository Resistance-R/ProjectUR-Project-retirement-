using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [SerializeField]
    private float speed;

    [SerializeField]
    private Rigidbody2D target;

    [SerializeField]
    private bool isLive;

    private Rigidbody2D enemyRigid;
    private Collider2D coll;
    private SpriteRenderer spriter;
    private Animator anim;

    public float maxHP = 100f;
    private float curruntHP;

    void Awake()
    {
        isLive = true;
        curruntHP = maxHP;
        enemyRigid = GetComponent<Rigidbody2D>();
        spriter = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
        coll = GetComponent<Collider2D>();      
    }

    private void Update()
    {
        Live();
        Dead();
    }

    void FixedUpdate()
    {
        Move();
    }

    private void Move()
    {
        Vector2 dirVec = target.position - enemyRigid.position;
        Vector2 nextVec = dirVec.normalized * speed * Time.fixedDeltaTime;
        enemyRigid.MovePosition(enemyRigid.position + nextVec);
        enemyRigid.velocity = Vector2.zero;
    }

    private void LateUpdate()
    {
        LookAtthePlayer();
    }

    private void LookAtthePlayer()
    {
        if(isLive)
        {
            spriter.flipX = target.position.x < enemyRigid.position.x;
        }
    }

    public void Live()
    {
        if(curruntHP > 0f)
        {
            isLive = true;
            enemyRigid.simulated = true;
            spriter.sortingOrder = -1;
            coll.enabled = true;
        }
    }

    public void Dead()
    {
        if(isLive && curruntHP <= 0f)
        {
            isLive = false;
            enemyRigid.simulated = false;
            spriter.sortingOrder = -2;
            coll.enabled = false;
            anim.SetTrigger("Dead");
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.collider.tag == "Bullet")
        {
            curruntHP -= GameManager.Instance.weaponDamage;
        }
        Debug.Log(curruntHP);
    }
}
