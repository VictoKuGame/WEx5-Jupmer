using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hero : MonoBehaviour
{
    [SerializeField] float speed;
    [SerializeField] int lives;
    [SerializeField] float jumpForce;
    private Rigidbody2D rb;
    private SpriteRenderer sprite;
    private bool isGrounded = false;
    
    void Update()
    {
        if (Input.GetAxis("Horizontal")!=0)
        {
            Run();
        }
        if (Input.GetButtonDown("Jump")&&isGrounded)
        {
            Jump();
        }
    }
    private void FixedUpdate()
    {
        CheckGround();
    }
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        sprite = GetComponentInChildren<SpriteRenderer>();
    }
    private void Run()
    {
        Vector3 dir = transform.right * Input.GetAxis("Horizontal");
        transform.position = Vector3.MoveTowards(transform.position, transform.position + dir, speed * Time.deltaTime);
        sprite.flipX = dir.x < 0;
    }
    private void Jump()
    {
        rb.AddForce(transform.up * jumpForce, ForceMode2D.Impulse);
    }

    private void CheckGround()
    {
        Collider2D[] collider = Physics2D.OverlapCircleAll(transform.position, 0.3f);
        isGrounded = collider.Length > 1;
    }
}
