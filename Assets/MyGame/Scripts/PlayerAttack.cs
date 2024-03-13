using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    private Animator anim;
    private int idAttack;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        idAttack = Animator.StringToHash("isAttack");
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.R))
        {
            Attack();
        }
    }
    void  Attack()
    {
        anim.SetTrigger(idAttack);

    }
}
