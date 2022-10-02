using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.SerializableAttribute]
public struct ButtonStates
{
    public Transform button;
    public bool isPressed;
}

public class Player : MonoBehaviour
{
    [SerializeField]
    ButtonStates[] buttons;

    Rigidbody2D rb;

    [SerializeField]
    float m_moveSpeed = 10;
    [SerializeField]
    float maxVelocity;
    [SerializeField]
    float m_airControl = 10;

    [SerializeField]
    float m_jumpForce = 10;
    [SerializeField]
    float m_secondJumpForce = 10;

    [SerializeField]
    float voltForce = 10;
    bool isVolting = false;

    bool isJumping = false;
    bool maxJumpsReached = false;

    Vector3 prevPos;
    Camera cam;

    [SerializeField] Animator anim;

    [SerializeField] AudioClip jumpSound1;
    [SerializeField] AudioClip jumpSound2;
    [SerializeField] AudioClip killSound;
    [SerializeField] AudioClip voltSound;

    AudioSource audioSource;
    // Start is called before the first frame update
    void Start()
    {
        cam = Camera.main;
        rb = GetComponent<Rigidbody2D>();
        currentVoltCutoff = voltCutoffMin;
        audioSource = GetComponent<AudioSource>();
    }


    [SerializeField]
    float voltEndVelocity = 1;
    [SerializeField] float voltCutoffMin = 1;
    [SerializeField] float voltCutoffMax = 1;
    [SerializeField] float voltCutoffChangeSpeed = 1;
    float currentVoltCutoff = 0;

    [SerializeField]
    LayerMask voltDamageLayer;

    float horizontalAxis = 0;
    // Update is called once per frame
    void Update()
    {
        
        //Move(Input.GetAxisRaw("Horizontal"));
        if(Input.GetAxisRaw("Horizontal") != 0)
        {
            horizontalAxis = Input.GetAxisRaw("Horizontal");
        }
        else
        {
            horizontalAxis = InputManager.instance.horizontalAxis;
        }
        Move(horizontalAxis);

        if (Input.GetKeyDown(KeyCode.W) || buttons[3].isPressed)
        {
            buttons[3].isPressed = false;
            Jump();

        }

        if(isJumping)
        {
            if(transform.position.y < prevPos.y)
            {
                if(IsGrounded())
                {
                    isJumping = false;
                    maxJumpsReached = false;
                }
            }
        }

        if(buttons[2].isPressed || Input.GetKeyDown(KeyCode.Space))
        {
            buttons[2].isPressed = false;
            Action();
        }

        if(isVolting)
        {
            if(rb.velocity.magnitude <= voltEndVelocity)
            {
                isVolting = false;
                currentVoltCutoff = voltCutoffMin;
            }
            else
            {
                if(IsGrounded())
                {
                    if (currentVoltCutoff < voltCutoffMax)
                    {
                        currentVoltCutoff += voltCutoffChangeSpeed * Time.deltaTime;
                    }
                    float cutoffDir = 0;
                    if(rb.velocity.normalized.x < 0)
                    {
                        cutoffDir = -1;
                    }
                    else
                    {
                        cutoffDir = 1;
                    }
                    Vector2 newVelocity = new Vector2(rb.velocity.normalized.x * rb.velocity.magnitude - (currentVoltCutoff * cutoffDir) * Time.deltaTime, rb.velocity.y);
                    rb.velocity = newVelocity;
                }
                RaycastHit2D hit = Physics2D.CircleCast(rb.position, 1, rb.velocity.normalized, 1, voltDamageLayer);
                if(hit)
                {
                    Destroy(hit.transform.gameObject);
                }
                
            }
        }

        prevPos = transform.position;
        anim.SetBool("IsGrounded", IsGrounded());
    }

    private void Move(float _axis)
    {
        if(!isVolting && IsGrounded())
        {
            if(_axis != 0)
            {
                anim.SetBool("IsIdle", false);
            }
            else
            {
                anim.SetBool("IsIdle", true);
            }
            if (rb.velocity.magnitude < maxVelocity)
            {
                if (IsGrounded())
                {
                    rb.AddForce(transform.right * _axis * m_moveSpeed * Time.deltaTime, ForceMode2D.Force);
                }
                else
                {
                    //rb.AddForce(transform.right * _axis * m_airControl * Time.deltaTime, ForceMode2D.Force);
                }
            }


            if (_axis == 0 && IsGrounded() && !isJumping)
            {
                rb.velocity = Vector3.zero;
            }
        }
        else if(isVolting && IsGrounded() && !isJumping)
        {
            if(_axis != 0)
            {
                float dir = rb.velocity.normalized.x;
                if(dir < 0)
                {
                    dir = -1;
                }
                else if(dir > 0)
                {
                    dir = 1;
                }
                if(dir != _axis)
                {
                    isVolting = false;
                    currentVoltCutoff = voltCutoffMin;
                    rb.velocity = Vector3.zero;
                }
            }
        }
        
    }

    public void Jump()
    {
        if(!maxJumpsReached)
        {
            float jumpForce = m_jumpForce;
            if (!IsGrounded() && isJumping)
            {
                audioSource.clip = jumpSound2;
                audioSource.Play();
                maxJumpsReached = true;
                jumpForce = m_secondJumpForce;
            }
            isJumping = true;
            if(IsGrounded())
            {
                audioSource.clip = jumpSound1;
                audioSource.Play();
                anim.SetTrigger("Jump");
                rb.velocity = rb.velocity.normalized * (rb.velocity.magnitude / 1.5f);
            }
            isVolting = false;
            currentVoltCutoff = voltCutoffMin;
            rb.AddForce(transform.up * jumpForce, ForceMode2D.Impulse);
            GameManager.instance.UpdateJumpMilestone();
        }
        
    }

    public void Action()
    {
        if(!isVolting && horizontalAxis != 0 && IsGrounded())
        {
            audioSource.clip = voltSound;
            audioSource.Play();
            anim.SetTrigger("Attack");
            isVolting = true;
            rb.AddForce(rb.velocity.normalized * voltForce, ForceMode2D.Impulse);
        }
    }

    [SerializeField]
    float groundedDistance;
    [SerializeField]
    LayerMask layerMask;
    bool IsGrounded()
    {
        Debug.DrawLine(transform.position, transform.position + -transform.up * groundedDistance, Color.green, 0);
        RaycastHit2D hit = Physics2D.Raycast(transform.position, -transform.up, groundedDistance, ~layerMask);
        if (hit)
        {
            
            return true;
        }
        else
        {
            return false;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.transform.tag == "Point")
        {
            GameManager.instance.AddPoints(1);
            Destroy(collision.gameObject);
        }
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Enemy enemy = collision.transform.GetComponent<Enemy>();
        if (enemy != null)
        {
            if (isVolting)
            {
                audioSource.clip = killSound;
                audioSource.Play();
                enemy.Die();
            }
            else
            {
                // kill player
                GameManager.instance.PlayerDied();
            }
        }
    }

}
