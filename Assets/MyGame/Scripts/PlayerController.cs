using System.Collections;
using System.Collections.Generic;
using UnityEditor.Tilemaps;
using UnityEngine;

public class PlayerController : MonoBehaviour,ICanTakeDamage
{
    [SerializeField ]private float moveSpeed = 5.0f;
    [SerializeField] private float jumFore = 5.0f;
    [SerializeField] Transform groundCheck;
    public LayerMask groundLayer;

    private Rigidbody2D rb;
    private Animator anim;
    private bool isGround;
    private bool facingRight = true;
    private int idRunningId;
    private int isDeadId;
    private int isJumId;
    [SerializeField] int maxHealth=100;
    private int currentHealth;
    public bool isPlayerDead=false;
    [SerializeField] private int _coin = 0;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        idRunningId = Animator.StringToHash("isRunning");
        isDeadId = Animator.StringToHash("isDead");
        isJumId = Animator.StringToHash("IsJum");
        currentHealth = maxHealth;
        EventManagerGame.onHealth?.Invoke(currentHealth);
    }

    // Update is called once per frame
    void Update()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        isGround = Physics2D.OverlapCircle(groundCheck.position, 0.2f, groundLayer);
        if(horizontalInput!=0 &&isPlayerDead==false)
        {
           Move(horizontalInput);    
        }
        else
        {
            anim.SetBool(idRunningId, false);
        }
        if(isGround&&Input.GetKeyDown(KeyCode.Space)&&isPlayerDead==false)
        {
            Jump();
        }
        if (Input.GetKeyUp(KeyCode.Space) && isPlayerDead == false)
        {
            NotJump();
        }
    }
    private void Move(float dir)
    {
        rb.velocity = new Vector2(dir * moveSpeed, rb.velocity.y);
        if((dir>0 && !facingRight)||(dir< 0 &&facingRight))
        {
            Flip();
        }
        anim.SetBool(idRunningId, true);
    }
    void Jump()
    {
        rb.velocity = new Vector2(rb.velocity.x, jumFore);
        anim.SetBool(isJumId,true);
        AudioManager.Instance.PlaySoundEffectMusic(AudioManager.Instance.jumAudio);
    }
     void NotJump()
    {
        anim.SetBool(isJumId, false);
    }
    void Flip()
    {
        facingRight = !facingRight;
        Vector2 scale = transform.localScale;
        scale.x *= -1;// 1->-1 /// -1 --->1
        transform.localScale = scale;
    }

    public void TakeDamage(int damage, Vector2 force, GameObject instigator)
    {
        if(isPlayerDead) return;
        currentHealth -= damage;
        EventManagerGame.onHealth?.Invoke(currentHealth);
        Debug.Log("Player Health" + currentHealth);
        if(currentHealth < 0)
        {
            isPlayerDead = true;
            currentHealth=0;
            anim.SetTrigger(isDeadId);
            //Gamemanger da chet
        }
   
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Coin"))
        {
            _coin += 1;
            Destroy(other.gameObject);
            EventManagerGame.onCoin?.Invoke(_coin);
            AudioManager.Instance.PlaySoundEffectMusic(AudioManager.Instance.Colect);
        }
    }

    
}
