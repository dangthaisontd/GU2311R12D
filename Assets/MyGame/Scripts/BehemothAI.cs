using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using Unity.Mathematics;
using UnityEngine;

public class BehemothAI : EnemyAI,ICanTakeDamage
{
    [SerializeField]private Rigidbody2D rb;
    [SerializeField] private Transform pointA;
    [SerializeField] private Transform pointB;
    [SerializeField] private float speed;
    [SerializeField] private SpriteRenderer sp;
    [SerializeField] private Animator anim;
    private Transform target;
    private Vector3 targetPos;
    [SerializeField] private float speedMove = 3;
    [SerializeField] private float stopDistance = 1;
    [SerializeField] GameObject HurtEffect;
    private bool isDead = false;
    
    // Start is called before the first frame update
    void Start()
    {
        transform.position = pointA.position;
        target = GameObject.FindGameObjectWithTag("Player").transform;
        health = maxHealth;
    }
    void DesTroys()
    {
        Destroy(gameObject);
    }
    // Update is called once per frame
    void Update()
    {
       
        if(Vector2.Distance(transform.position, target.position) > stopDistance)
         {
            if (anim.GetCurrentAnimatorStateInfo(0).IsName("Idle"))
                return;
            if (transform.position == pointA.position)
            {
                targetPos = pointB.position;
                sp.flipX = false;
                anim.SetTrigger("IsIdle");

            }
            else if (transform.position == pointB.position)
            {
                targetPos = pointA.position;
                sp.flipX = true;
                anim.SetTrigger("IsIdle");
            }
            transform.position = Vector3.MoveTowards(transform.position, targetPos, speed * Time.deltaTime);
        }
        else if(Vector2.Distance(transform.position, target.position) < stopDistance)
        {
            if (isDead == true) return;
            //tan cong
            anim.SetTrigger("IsAttack");
            if(target.position.x - transform.position.x < 0)
            {
                sp.flipX = true;
            }
            else if(target.position.x - transform.position.x > 0)
            {
                sp.flipX = false;
            }       
        }           
     }
    public void TakeDamage(int damage, Vector2 force, GameObject instigator)
    {
        if (isDead) return;
        health -= damage;
        if (HurtEffect != null)
            Instantiate(HurtEffect, instigator.transform.position, Quaternion.identity);
        Debug.Log(health);
        if (health <= 0)
        {
            isDead = true;
            anim.SetTrigger("IsDead");
            Invoke("DesTroys", 3f);
        }
    }
}
