using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class PlayerController : MonoBehaviour
{
    //Start() variables
    private Rigidbody2D rb;
    private Animator anim;
    private Collider2D coll;

    //FSM
    private enum State { idle, running, jumping, falling, hurt, wallgrab, swing}
    private State state = State.idle;

    //Inspector variables
    [SerializeField] private float ropejumpvertical = 10f;
    [SerializeField] private float ropejumphoriz = 10f;
    [SerializeField] private LayerMask ground;
    [SerializeField] private float speed = 5f;
    [SerializeField] private float jumpForce = 10f;
    [SerializeField] private int cherries = 0;
    [SerializeField] private float powers = 6f;
    [SerializeField] private TextMeshProUGUI cherryText;
    [SerializeField] private float hurtForce = 10f;
    [SerializeField] private float punchForce = 10f;
    [SerializeField] private bool punchable = false;
    [SerializeField] private AudioSource cherry;
    [SerializeField] private AudioSource footstep;
    [SerializeField] private AudioSource powerup;
    [SerializeField] private AudioSource hit;
    [SerializeField] private bool canropejump = true;
    GameObject player;



    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        coll = GetComponent<Collider2D>();
        player = GameObject.FindGameObjectWithTag("Player");
    }
    private void Update()
    {
        if (state != State.hurt)
        {
            Movement();
        }
        AnimationState();
        anim.SetInteger("state", (int)state); //sets animation based on Enumerator state       
    }
    private void gethit ()
    {
        hit.Play();
    }    
    private void OnTriggerEnter2D(Collider2D collision) //Trigger for Collectables
    {
        if (collision.tag == "Collectable")
        {
            cherry.Play();
            Destroy(collision.gameObject); //Cherry destroy
            cherries += 1;
            cherryText.text = cherries.ToString(); //Converting number to string
        }
        if (collision.tag == "Powerup")
        {
            powerup.Play();
            jumpForce = jumpForce + powers;
            Destroy(collision.gameObject);         
        }
        if (collision.tag == "Monkey");
        punchable = true;
        Debug.Log("punch");       
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Monkey");
        punchable = false;
        Debug.Log("nopunch");
    }
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            Enemy enemy = other.gameObject.GetComponent<Enemy>();
            if (state == State.falling)
            {
                enemy.Jumpood();
                Jump();
            }
            else
            {
                state = State.hurt;
                if (other.gameObject.transform.position.x > transform.position.x)
                {
                    //Enemy is to my right therefore should be damaged and move left
                    rb.velocity = new Vector2(-hurtForce, hurtForce);
                }
                else
                {
                    //Enemy is to my left therefore i Should be damaged and move right
                    rb.velocity = new Vector2(hurtForce, hurtForce);
                }
            }

        }
        if (other.gameObject.tag == "Ground")
        {
            canropejump = true;
            player.GetComponent<grapple>().Activate();
            Debug.Log("thatworks");
        }    
    }
    private void OnCollisionExit2D(Collision2D other)
    {
        if (other.gameObject.tag == "Ground")
        {

        }
    }
    private void Movement()
    {
        float hDirection = Input.GetAxis("Horizontal");
        //Moving left
        if (hDirection < 0)
        {
            rb.velocity = new Vector2(-speed, rb.velocity.y);
            transform.localScale = new Vector2(-1, 1);
        }
        //Moving right
        else if (hDirection > 0)
        {
            rb.velocity = new Vector2(speed, rb.velocity.y);
            transform.localScale = new Vector2(1, 1);
        }
        //Jumping
        if (Input.GetButtonDown("Jump") && coll.IsTouchingLayers(ground))
        {
            Jump();
        }
    }
    private void Jump()
    {
        rb.velocity = new Vector2(rb.velocity.x, jumpForce);
        state = State.jumping;
    }
    public void RopeJump()
    {
        float hDirection = Input.GetAxis("Horizontal");
        if (canropejump == true)
        {
            if (hDirection < 0)
            {
                rb.velocity = new Vector2(-ropejumphoriz,ropejumpvertical);
                
            }
            else if (hDirection > 0)
            {
                rb.velocity = new Vector2(ropejumphoriz, ropejumpvertical);
                
            }
            else
            {
                rb.velocity = new Vector2(rb.velocity.x, ropejumpvertical);
            }
            canropejump = false;
            state = State.jumping;
        }

       
    }  
    private void AnimationState()
    {
        if (state == State.jumping)
        {
            if (rb.velocity.y < .1f)
            {
                state = State.falling;
            }
        }
        else if (state == State.falling)
        {
            if (coll.IsTouchingLayers(ground))
            {
                state = State.idle;
            }
        }
        else if (state == State.hurt)
        {
            if (Mathf.Abs(rb.velocity.x) < .1f)
            {
                state = State.idle;
            }
        }

        else if (Mathf.Abs(rb.velocity.x) > 2f)
        {
            //Moving
            state = State.running;
        }
        else
        {
            state = State.idle;
        }
    }   
        private void Footstep()
    {
        footstep.Play();
       
    }
    public void punch()
    {
        if (punchable == true){
            rb.velocity = new Vector2(-punchForce, punchForce*2);
            state = State.hurt;
        }
    }
    

}