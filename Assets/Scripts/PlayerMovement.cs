using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    public Animator animator;

    public float dirX;
    public bool isFacingLeft;

    private Rigidbody2D rb;
    private BoxCollider2D coll;

    [SerializeField] private LayerMask jumpableGround;

    [SerializeField] private AudioClip runningSoundClip;




    // Start is called before the first frame update
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        coll = GetComponent<BoxCollider2D>();

       
    }

    // Update is called once per frame
    private void Update()
    {
        dirX = Input.GetAxisRaw("Horizontal");

        animator.SetFloat("Speed", Mathf.Abs(dirX));

       

        if( Input.GetButtonDown("Jump") && IsGrounded())
        {
            GetComponent<Rigidbody2D>().velocity = new Vector2(rb.velocity.x, 7);
        }

    }


    private void FixedUpdate()
    {
        rb.velocity = new Vector2(dirX * 3.5f, rb.velocity.y);

        if (dirX < 0 && !isFacingLeft) 
        {
            transform.localScale = new Vector2(-transform.localScale.x, transform.localScale.y);
            isFacingLeft = true;
        }
        if (dirX > 0 && isFacingLeft) 
        {
            transform.localScale = new Vector2(-transform.localScale.x, transform.localScale.y);
            isFacingLeft= false;
        }



    }

    private bool IsGrounded()
    {
       return Physics2D.BoxCast(coll.bounds.center, coll.bounds.size, 0f, Vector2.down, .1f, jumpableGround);
    }
}
