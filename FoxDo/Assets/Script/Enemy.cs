using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    protected Animator anim;
    protected Rigidbody2D rb;
    protected AudioSource hit;

    protected virtual void Start()
    {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        hit = GetComponent<AudioSource>();
    }
    public void Jumpood()
    {
        anim.SetTrigger("Death");
        hit.Play();
    }
    private void Death()
    {
        Destroy(gameObject);
    }
}
