using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class parot : Enemy
{
    private Rigidbody2D bod;
    private Collider2D Col;

    [SerializeField] private float left;
    [SerializeField] private float right;
    [SerializeField] private float Speed;
    private bool facingLeft = true;

    protected override void Start()
    {
        base.Start();
        bod = GetComponent<Rigidbody2D>();
        Col = GetComponent<Collider2D>();
    }
    void Update()
    {
        if (transform.position.x < left)
        {
            facingLeft = false;
        }
        else if (transform.position.x > right)
        {
            facingLeft = true;
        }
        if (facingLeft == true)
        {
            bod.velocity = new Vector2(-Speed, 0);
            transform.localScale = new Vector2(1, 1);
        }
        else
        {
            bod.velocity = new Vector2(Speed, 0);
            transform.localScale = new Vector2(-1, 1);
        }
    }
}