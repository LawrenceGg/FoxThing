using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bigmonkey : MonoBehaviour
{
    private Rigidbody2D bod;
    private Collider2D coll;
    private Animator anim;
    GameObject player;
    float power = 10f;
    float delay = 1f;
    bool Angry;
    bool Relaxed;
   
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        anim = GetComponent<Animator> ();
        anim.SetBool("Attacking", false);
        coll = GetComponent<Collider2D>();
        bod = GetComponent<Rigidbody2D>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject == player)
        {
            anim.SetBool("Attacking", true);
            Debug.Log("angry");
        }
    }
    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject == player)
        {
            anim.SetBool("Attacking", false);
            Debug.Log("NotAngry");
        }
    }
   public void hithim()
    {
        player.GetComponent<PlayerController>().punch();
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
