using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VerticalMovement : MonoBehaviour
{
    Rigidbody2D rb;
    [Header("Jump")]
    public float jumpForce;
    [Header("Dash")]
    public float dashForce;
    [Header("Walljump")]
    public Vector2 walljumpForce;
    public float walljumpDistance;
    public float walljumpHeight;
    public LayerMask walljumpLayerMask;
    public bool isInWall;
    public float walljumpGravityScale;
    float originalGravityScale;

    HorizontalMovement horizontalMovement;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        horizontalMovement = GetComponent<HorizontalMovement>();
        originalGravityScale = rb.gravityScale;
    }

    // Update is called once per frame
    void Update()
    {
        if (!PauseMenu.instance.isPaused)
        {
            Walljump();
            if (!isInWall)
            {
                Jump();
                Dash();
            }
        }
    }

    void Jump()
    {
        if (Input.GetButtonDown("Jump"))
        {
            rb.velocity = Vector2.zero;
            rb.AddForce(Vector2.up * jumpForce);
        }
    }

    void Dash()
    {
        if (Input.GetButtonDown("Dash"))
        {
            rb.velocity = Vector2.zero;
            rb.AddForce(Vector2.right * dashForce);
        }
    }

    void Walljump()
    {
        Vector2 origin = (Vector2)transform.position + Vector2.up * walljumpHeight;
        RaycastHit2D right = Physics2D.Raycast(origin, Vector2.right, walljumpDistance, walljumpLayerMask);
        Debug.DrawRay(origin, Vector2.right * walljumpDistance, Color.yellow);
        RaycastHit2D left = Physics2D.Raycast(origin, Vector2.right, -walljumpDistance, walljumpLayerMask);
        Debug.DrawRay(origin, Vector2.right * -walljumpDistance, Color.yellow);
        int dir = 0;
        if (right.collider != null)
        {
            dir = 1;
            Debug.DrawRay(origin, Vector2.right * right.distance, Color.green);
        }
        if (left.collider != null)
        {
            dir = -1;
            Debug.DrawRay(origin, Vector2.right * -left.distance, Color.green);
        }
        if (dir == 0)
        {
            isInWall = false;
            rb.gravityScale = originalGravityScale;
        }
        else
        {
            if (dir > 0)
            {
                horizontalMovement.sr.flipX= true;
            }
            if (dir < 0)
            {
                horizontalMovement.sr.flipX = false;
            }
            isInWall = true;
            rb.gravityScale = walljumpGravityScale;
            if (Input.GetButtonDown("Jump"))
            {
                rb.velocity = Vector2.zero;
                rb.AddForce(new Vector2(walljumpForce.x * dir, walljumpForce.y));
            }
        }
        horizontalMovement.anim.SetBool("Wall", isInWall);
    }
}
