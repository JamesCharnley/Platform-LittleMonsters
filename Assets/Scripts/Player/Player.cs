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
    float m_airControl = 10;

    [SerializeField]
    float m_jumpForce = 10;

    bool isJumping = false;

    Vector3 prevPos;
    Camera cam;
    // Start is called before the first frame update
    void Start()
    {
        cam = Camera.main;
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        //Move(Input.GetAxisRaw("Horizontal"));
        if(Input.GetAxisRaw("Horizontal") != 0)
        {
            horizontalAxis = Input.GetAxisRaw("Horizontal");
        }
        Move(horizontalAxis);

        if (Input.GetKeyDown(KeyCode.Space) || buttons[3].isPressed)
        {
            buttons[3].isPressed = false;
            if(IsGrounded())
            {
                Jump();
            }
            
        }

        if(isJumping)
        {
            if(transform.position.y < prevPos.y)
            {
                if(IsGrounded())
                {
                    isJumping = false;
                }
            }
        }

        prevPos = transform.position;

        InputCheck();
    }

    private void Move(float _axis)
    {
        if(IsGrounded())
        {
            rb.AddForce(transform.right * _axis * m_moveSpeed * Time.deltaTime, ForceMode2D.Force);
        }
        else
        {
            rb.AddForce(transform.right * _axis * m_airControl * Time.deltaTime, ForceMode2D.Force);
        }

        if(_axis == 0 && IsGrounded() && !isJumping)
        {
            rb.velocity = Vector3.zero;
        }
    }

    private void Jump()
    {
        isJumping = true;
        rb.AddForce(transform.up * m_jumpForce, ForceMode2D.Impulse);
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

    float horizontalAxis = 0;
    void InputCheck()
    {
        
        //Vector3 buttonTop = buttons[0].transform.position + transform.up * 0.5f * buttons[0].transform.lossyScale.y;
        //Debug.DrawLine(buttonTop + transform.right * 5, buttonTop + -transform.right * 5, Color.green, 0);
        foreach (Touch touch in Input.touches)
        {
            
            //Vector3 buttonTop = buttons[0].transform.position + transform.up * 0.5f * buttons[0].transform.lossyScale.y;
            //Debug.DrawLine(buttonTop + transform.right, buttonTop + -transform.right, Color.green, 0);
            
            int count = 0;
            foreach(ButtonStates _button in buttons)
            {
                Vector3 buttonTop = _button.button.position + transform.up * 0.5f * _button.button.lossyScale.y;
                Vector3 buttonBottom = _button.button.position + -transform.up * 0.5f * _button.button.lossyScale.y;
                Vector3 buttonLeft = _button.button.position + -transform.right * 0.5f * _button.button.lossyScale.x;
                Vector3 buttonRight = _button.button.position + transform.right * 0.5f * _button.button.lossyScale.x;

                
                if (touch.position.x > cam.WorldToScreenPoint(buttonLeft).x)
                {
                    if(touch.position.x < cam.WorldToScreenPoint(buttonRight).x)
                    {
                        if(touch.position.y > cam.WorldToScreenPoint(buttonBottom).y)
                        {
                            if (touch.position.y < cam.WorldToScreenPoint(buttonTop).y)
                            {
                                if(count == 0 || count == 1)
                                {
                                    if(touch.phase == TouchPhase.Began)
                                    {
                                        buttons[count].isPressed = true;
                                        Debug.Log("Button " + count.ToString() + " Pressed");
                                    }
                                    else if(touch.phase == TouchPhase.Ended)
                                    {
                                        buttons[count].isPressed = false;
                                        Debug.Log("Button " + count.ToString() + " Released");
                                    }
                                }
                                else if(count == 2 || count == 3)
                                {
                                    if(touch.phase == TouchPhase.Ended)
                                    {
                                        buttons[count].isPressed = true;
                                    }
                                }
                                
                            }
                        }
                    }
                }
                count++;
            }
        }

        horizontalAxis = 0;
        if(buttons[0].isPressed)
        {
            horizontalAxis -= 1;
            //Debug.Log("Left button pressed");
        }
        if(buttons[1].isPressed)
        {
            horizontalAxis += 1;
            //Debug.Log("Right button pressed");
        }
    }
}
