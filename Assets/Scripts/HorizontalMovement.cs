using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HorizontalMovement : MonoBehaviour
{
    public SpriteRenderer sr;
    public Animator anim;
    public GroundDetector ground;
    public VerticalMovement verticalMovement;
    public float speed = 5;
    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
        ground = GetComponent<GroundDetector>();
        verticalMovement = GetComponent<VerticalMovement>();
    }

    private void Update()
    {
        float horizontal = Input.GetAxis("Horizontal");
        transform.position += new Vector3(horizontal * speed * Time.deltaTime, 0, 0);
        if (verticalMovement != null && verticalMovement.isInWall)
        {

        }
        else
        {
            if (horizontal > 0)
            {
                sr.flipX = false;
            }
            if (horizontal < 0)
            {
                sr.flipX = true;
            }
        }
        anim.SetBool("Moving", horizontal != 0);
        anim.SetBool("Grounded", ground.grounded);
    }
}
