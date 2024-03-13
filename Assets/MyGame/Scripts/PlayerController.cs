using System.Collections;
using System.Collections.Generic;
using UnityEditor.Tilemaps;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField ]private float moveSpeed = 5.0f;
    [SerializeField] private float jumFore = 5.0f;
    [SerializeField] Transform groundCheck;
    public LayerMask groundLayer;

    private Rigidbody2D rb;
    private Animator anim;
    private bool isGround;
    private bool facingRight = true;
    private int idRunning;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        idRunning = Animator.StringToHash("isRunning"); 
    }

    // Update is called once per frame
    void Update()
    {
        float horizontalInput = Input.GetAxisRaw("Horizontal");
        isGround = Physics2D.OverlapCircle(groundCheck.position, 0.2f, groundLayer);
        Debug.Log(isGround);
        if(horizontalInput!=0)
        {
           Move(horizontalInput);
        }
        else
        {
            anim.SetBool(idRunning, false);
        }
        if(isGround&&Input.GetKeyDown(KeyCode.Space))
        {
            Jump();
        }
    }
    private void Move(float dir)
    {
        rb.velocity = new Vector2(dir * moveSpeed, rb.velocity.y);
        if((dir>0 && !facingRight)||(dir< 0 &&facingRight))
        {
            Flip();
        }
        anim.SetBool(idRunning, true);
    }
    void Jump()
    {
        rb.velocity = new Vector2(rb.velocity.x, jumFore);
    }
    void Flip()
    {
        facingRight = !facingRight;
        Vector2 scale = transform.localScale;
        scale.x *= -1;// 1->-1 /// -1 --->1
        transform.localScale = scale;
    }}
