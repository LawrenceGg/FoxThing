using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vample : Enemy
{   private Rigidbody2D bod;
    private Collider2D Col;

    [SerializeField] private float topCap;
    [SerializeField] private float botCap;
    [SerializeField] private float Speed;
    private bool facingLeft = false;
    private bool goingup = true;

    protected override void Start()
    {
        base.Start();
        bod = GetComponent<Rigidbody2D>();
        Col = GetComponent<Collider2D>();
    }
    void Update()
    {
        if (transform.position.y > topCap)
        {
            goingup = false;
        }
        else if (transform.position.y < botCap)
        {
            goingup = true;
        }
        if (goingup == true)
        {
            bod.velocity = new Vector2(0, Speed);
        }
        else bod.velocity = new Vector2(0, -Speed);
    }
    private void turning()
    {
        if (facingLeft == true)
        {
            facingLeft = false;
        }
        else facingLeft = true;
        
    }
}
