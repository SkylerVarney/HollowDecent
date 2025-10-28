using UnityEngine;
using System;

public class Movement : MonoBehaviour
{

    public float speed = 5f;
    public int facingDirection = 1;

    public Rigidbody2D rb;
    public Animator anim;

    public PlayerCombat playerCombat;

    private void Update()
    {
        if (Input.GetButtonDown("Attack"))
        {
            playerCombat.Attack();
        }
    }


    // Update is called once per frame
    void FixedUpdate()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");

        if (horizontal > 0 && transform.localScale.x < 0 ||
            horizontal < 0 && transform.localScale.x > 0)
        {
            Flip();
        }
        
        anim.SetFloat("horizontal", Math.Abs(horizontal));
        anim.SetFloat("vertical", Math.Abs(vertical));

        Vector2 input = new Vector2(horizontal * speed, vertical * speed);
        Vector2 move  = input.normalized;          // prevents fast diagonals
        rb.linearVelocity   = move * speed;

        HandleWalkingBack(vertical, horizontal);
    }

    void Flip()
    {
        facingDirection *= -1;
        Vector3 scale = transform.localScale;
        scale.x *= -1;
        transform.localScale = scale;
    }

    void HandleWalkingBack(float vertical, float horizontal)
    {
        bool movingUp = vertical > 0.01f;

        anim.SetBool("movingUp", movingUp);
    }
}
