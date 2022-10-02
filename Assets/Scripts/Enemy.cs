using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{

    [SerializeField] float moveSpeed = 5;
    int currentDirection = 1;

    Transform w1;
    Transform w2;
    Transform currentW;
    Rigidbody2D rb;
    [SerializeField] float attackDistance = 1;
    [SerializeField] Animator anim;

    GameObject player;
    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<Player>().gameObject;

        w1 = transform.GetChild(0);
        w2 = transform.GetChild(1);
        w1.transform.parent = null;
        w2.transform.parent = null;
        currentW = w1;
        rb = GetComponent<Rigidbody2D>();

        anim.SetBool("IsIdle", false);
    }

    // Update is called once per frame
    void Update()
    {
        if(isDead)
        {
            Destroy(GetComponent<Rigidbody2D>());
            Destroy(GetComponent<Enemy>());
            return;
        }
        float dist = Vector3.Distance(currentW.transform.position, transform.position);

        if(dist < 1.0f)
        {
            // change waypoint
            if(currentW == w1)
            {
                currentW = w2;
            }
            else
            {
                currentW = w1;
            }
            currentDirection = currentDirection * -1;
        }
        else
        {
            rb.position = Vector2.MoveTowards(rb.position, currentW.position, moveSpeed * Time.deltaTime);
        }

        dist = Vector3.Distance(player.transform.position, transform.position);   
        if(dist < attackDistance)
        {
            anim.SetTrigger("Attack");
        }
    }
    bool isDead = false;
    public void Die()
    {
        Destroy(GetComponent<BoxCollider2D>());
        anim.SetTrigger("Die");
        isDead = true;
    }
}
