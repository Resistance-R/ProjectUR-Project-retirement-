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
    private SpriteRenderer spriter;

    void Awake()
    {
        enemyRigid = GetComponent<Rigidbody2D>();
        spriter = GetComponent<SpriteRenderer>();        
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
        spriter.flipX = target.position.x < enemyRigid.position.x;    
    }
    }
