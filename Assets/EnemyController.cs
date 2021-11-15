using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [SerializeField] float timeToDisapear;
    [SerializeField] Rigidbody2D rb;
    Animator anim;
    //*Called before the first frame update, determines the target which is the player and its walking and destruction animation .
    void Start()
    {
        anim = GetComponentInChildren<Animator>();
    }
    //*When players bullet touches the enemy it changes its animation and sets its off.
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Ground")|| collision.CompareTag("Hero"))
        {
            anim.SetTrigger("Boom1");
            rb.constraints = RigidbodyConstraints2D.FreezePosition;
            Destroy(gameObject, timeToDisapear);
        }
    }

}


