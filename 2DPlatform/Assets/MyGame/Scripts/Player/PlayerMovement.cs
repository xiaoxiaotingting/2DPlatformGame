using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerMovement : MonoBehaviour
{
    private PlayerCollision coll;
    [HideInInspector]
    public Rigidbody2D rb;
    public PlayerAnimation anim;

    [Space]
    [Header("Stats")]
    public float speed=5f;
    public float jumpForce = 50;

    [Space]
    [Header("Booleans")]
    public bool canMove;

    [Space]
    public int side;

    private Vector2 moveDir;
    private float xAxis;
    private float yAxis;

    void Start()
    {
        coll = GetComponent<PlayerCollision>();
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<PlayerAnimation>();
    }

    void Update()
    {
        //获取方向键输入
        xAxis = Input.GetAxis("Horizontal");
        yAxis = Input.GetAxis("Vertical");
        moveDir = new Vector2(xAxis, yAxis);

        if (coll.onGround)
            canMove = true;
        //移动
        Move();
        //跳跃
        if (Input.GetButtonDown("Jump"))
        {
            //anim.SetTrigger("jump");

            if (coll.onGround)
                Jump(Vector2.up);
        }
        //翻转
        if (xAxis > 0)
        {
            side = 1;
            Flip(side);
        }
        if (xAxis < 0)
        {
            side = -1;
            Flip(side);
        }
    }

    private void Jump(Vector2 dir)
    {
        rb.velocity = new Vector2(rb.velocity.x, 0);
        rb.velocity += dir * jumpForce;
        Debug.Log("跳");
    }

    private void Flip(int side)
    {
        if (side == 1)
            transform.eulerAngles = new Vector3(0, 0, 0);
        else
            transform.eulerAngles = new Vector3(0, 180, 0);
    }

    private void Move()
    {
        if (!canMove)
            return;
        //移动
        rb.velocity = new Vector2(moveDir.x * speed, rb.velocity.y);
    }
}
