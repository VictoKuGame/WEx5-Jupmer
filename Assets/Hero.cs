using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Hero : MonoBehaviour
{
    [SerializeField] float speed;
    [SerializeField] float jumpForce;
    [SerializeField] Slider HP;
    [SerializeField] int BorderHitPoints;
    [SerializeField] int MeteorHitPoints;
    private Rigidbody2D rb;
    private Animator anim;
    private SpriteRenderer sprite;
    private bool isGrounded = false;
    public Vector3 respawn ;
    
    private States State
    {
        get { return (States)anim.GetInteger("state"); }
        set { anim.SetInteger("state", (int)value); }
    }
    void Update()
    {
        if (isGrounded)
        {
            State = States.idle;
        }
        if (Input.GetAxis("Horizontal") != 0)
        {
            Run();
        }
        if (isGrounded && Input.GetButtonDown("Jump"))
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
        HP = GameObject.Find("HPShow1").GetComponent<Slider>();
        HP.value = 100;
        rb = GetComponent<Rigidbody2D>();
        sprite = GetComponentInChildren<SpriteRenderer>();
        anim = GetComponent<Animator>();
    }
    private void Run()
    {
        if (isGrounded)
        {
            State = States.run;
        }
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
        if (!isGrounded)
        {
            State = States.jump;
        }
    }
    public enum States
    {
        idle,
        run,
        jump
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Border"))
        {
            if (HP.value >= BorderHitPoints)
            {
                HP.value -= BorderHitPoints;
                transform.position = respawn;
            }
            else
            {
                GameOverScreen1();
            }
        }
        if (collision.gameObject.CompareTag("NextLvlPass"))
        {
            LoadNextLevel();
        }
        if (collision.gameObject.CompareTag("Finish."))
        {
            FinishScreen1();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            if (HP.value >= MeteorHitPoints)
            {
                HP.value -= MeteorHitPoints;
            }
            else
            {
                GameOverScreen1();
            }
        }
    }
    public void QuitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
        Application.Quit();
    }
    private void LoadNextLevel()
    {
        HP.value = 100;
        SceneManager.LoadScene("Level2");
    }
    private void FinishScreen1()
    {
        SceneManager.LoadScene("Finish1");
    }
     private void GameOverScreen1()
    {
        SceneManager.LoadScene("GameOverS");
    }
}

