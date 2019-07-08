using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(Rigidbody2D))]
public class PlayerMovement : MonoBehaviour
{
    [SerializeField]
    float velocity;
    [SerializeField]
    bool isTopDown = false;
    Rigidbody2D rb;

    float verticalMove;
    float horizontalMove;
    [SerializeField]
    float verticalForce = 10.0f;
    bool isGrounded;
    bool wasPressed = false;

    private void Awake()
    {
        isGrounded = true;
        rb = GetComponent<Rigidbody2D>();
    }

    void moveCharacter()
    {
        if (isTopDown == false)
        {
            rb.gravityScale = 1.0f;
            horizontalMove = Input.GetAxisRaw("Horizontal") * velocity;
            Vector2 targetVelocity = new Vector2(horizontalMove, rb.velocity.y);
            Vector2 m_velocity = Vector2.zero;
            rb.velocity = Vector2.SmoothDamp(rb.velocity, targetVelocity, ref m_velocity, 0.05f);
        }
        else
        {
            rb.gravityScale = 0.0f;
            horizontalMove = Input.GetAxisRaw("Horizontal") * velocity;
            verticalMove = Input.GetAxisRaw("Vertical") * velocity;
            Vector2 targetVelocity = new Vector2(horizontalMove, verticalMove);
            Vector2 m_velocity = Vector2.zero;
            rb.velocity = Vector2.SmoothDamp(rb.velocity, targetVelocity, 
                ref m_velocity, 0.05f);
        }
    }

    void jump()
    {
        if(Input.GetKey(KeyCode.Space) && isGrounded)
        {
            rb.AddForce(Vector2.up * verticalForce);
            isGrounded = false;
        }
    }

    void betterJump()
    {
        float fallMultiplier = 2.5f;
        float lowJumpMultiplier = 2.0f;

        if(Input.GetButtonDown("Jump") && wasPressed )
        {
            rb.velocity = Vector2.up * verticalForce;
            
        }
        else
        {
            wasPressed = false; 
        }
        

        if (rb.velocity.y < 0){
            rb.velocity += Vector2.up * Physics2D.gravity.y * (fallMultiplier - 1) * Time.fixedDeltaTime;
        }else if(rb.velocity.y > 0 && !Input.GetButton("Jump")){
            rb.velocity += Vector2.up * Physics2D.gravity.y * (lowJumpMultiplier - 1) * Time.fixedDeltaTime;
        }

        
    }
    // Start is called before the first frame update
    void Start()
    {
     
        //rb.MovePosition(rb.position + velocity);
    }

    private void FixedUpdate()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, 1.0f);
        for (int i = 0; i < colliders.Length; i++)
        {
            if (colliders[i].gameObject != gameObject)
            {
                isGrounded = true;
                wasPressed = true;
            }
        }
        if (isTopDown == false)
        {
            betterJump();
        }
        moveCharacter();
        
    }

    // Update is called once per frame
    void Update()
    {

       
    }

    //private void OnTriggerStay2D(Collider2D collision)
    //{
    //    Debug.Log("Hello");
    //    isGrounded = true;
    //    wasPressed = true;
    //}
}
