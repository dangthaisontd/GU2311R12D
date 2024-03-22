using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFire : MonoBehaviour
{
    public Transform pointFire;
    public float projectiveFore = 20f;
    public GameObject projectivePrefabs;
    public Animator anim;
    private Rigidbody2D rb;
    private int isAttackId;
    private PlayerController player;
    // Start is called before the first frame update
    void Start()
    {
        isAttackId = Animator.StringToHash("isAttack");
        player = GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetButtonDown("Fire1"))
        {
            if (player.isPlayerDead == true)
                return;
            anim.SetTrigger(isAttackId);
            GameObject projevtive = Instantiate(projectivePrefabs, pointFire.position, Quaternion.identity);
            rb = projevtive.GetComponent<Rigidbody2D>();
            if (rb != null)
            {
                AudioManager.Instance.PlaySoundEffectMusic(AudioManager.Instance.fireAudio);
                if (transform.localScale.x == 1)
                {
                    rb.AddForce(pointFire.right * projectiveFore, ForceMode2D.Impulse);
                }
                else
                {
                    rb.AddForce(-pointFire.right * projectiveFore, ForceMode2D.Impulse);
                }
            }
        }
    }
}
