using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Pooman : Enemy
{
    private Rigidbody2D dop;
    private Collider2D colli;
    

   [SerializeField] private float leftCap;
   [SerializeField] private float rightCap;
   [SerializeField] private float jumpLength;
   [SerializeField] private float jumpHeight;
   [SerializeField] private LayerMask ground;
    private bool facingLeft = true;

    protected override void Start()
    {
        base.Start();
        dop = GetComponent<Rigidbody2D>();
        colli = GetComponent<Collider2D>();      
    }   
    private void Update()
    {
        if(anim.GetBool("Jumpoo"))
        {
            if(colli.IsTouchingLayers(ground))
            {
                anim.SetBool("Landoo", true);
                anim.SetBool("Jumpoo", false);
            }
        }

    }

    private void MovePoo()
    {
        if ((facingLeft) && colli.IsTouchingLayers(ground))
        {
            if (transform.position.x < leftCap)
            {
                dop.velocity = new Vector2(jumpLength, jumpHeight);
                transform.localScale = new Vector2(-1, 1);
                facingLeft = false;
                anim.SetBool("Jumpoo", true);
            }
            else
            {
                dop.velocity = new Vector2(-jumpLength, jumpHeight);
                anim.SetBool("Jumpoo", true);

            }
        }
        if ((facingLeft == false) && colli.IsTouchingLayers(ground))
        {
            if (transform.position.x > rightCap)
            {
                dop.velocity = new Vector2(-jumpLength, jumpHeight);
                transform.localScale = new Vector2(1, 1);
                facingLeft = true;
                anim.SetBool("Jumpoo", true);
            }
            else
            {
                dop.velocity = new Vector2(jumpLength, jumpHeight);
                anim.SetBool("Jumpoo", true);

            }
        }
    }

   
}


