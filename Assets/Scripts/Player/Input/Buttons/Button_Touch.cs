using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public enum EButtonType
{
    Continuous,
    OnRelease,
    OnPress
}
public class Button_Touch : MonoBehaviour
{
    [SerializeField] private EButtonType buttonType;

    Camera cam;
    bool isPressed;
    // Start is called before the first frame update
    void Start()
    {
        cam = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    protected virtual void StartAction()
    {

    }
    protected virtual void EndAction()
    {

    }

    public void CheckBounds(Vector2 _touchPos, TouchPhase _touchPhase)
    {
        Vector3 buttonTop = transform.position + transform.up * 0.5f * transform.lossyScale.y;
        Vector3 buttonBottom = transform.position + -transform.up * 0.5f * transform.lossyScale.y;
        Vector3 buttonLeft = transform.position + -transform.right * 0.5f * transform.lossyScale.x;
        Vector3 buttonRight = transform.position + transform.right * 0.5f * transform.lossyScale.x;


        if (_touchPos.x > cam.WorldToScreenPoint(buttonLeft).x)
        {
            if (_touchPos.x < cam.WorldToScreenPoint(buttonRight).x)
            {
                if (_touchPos.y > cam.WorldToScreenPoint(buttonBottom).y)
                {
                    if (_touchPos.y < cam.WorldToScreenPoint(buttonTop).y)
                    {
                        switch (buttonType)
                        {
                            case EButtonType.Continuous:
                                // code block
                                if (_touchPhase == TouchPhase.Began)
                                {
                                    isPressed = true;
                                    StartAction();
                                }
                                else if (_touchPhase == TouchPhase.Ended)
                                {
                                    isPressed = false;
                                    EndAction();
                                }
                                break;
                            case EButtonType.OnRelease:
                                // code block
                                if (_touchPhase == TouchPhase.Ended)
                                {
                                    StartAction();
                                }
                                break;
                            case EButtonType.OnPress:
                                // code block
                                if(_touchPhase == TouchPhase.Began)
                                {
                                    Debug.Log("OnPress Begin");
                                    if(isPressed == false)
                                    {
                                        isPressed = true;
                                        StartAction();
                                    }
                                }
                                else if (_touchPhase == TouchPhase.Ended)
                                {
                                    Debug.Log("OnPress End");
                                    isPressed = false;
                                    EndAction();
                                }
                                break;
                            default:
                                // code block
                                break;
                        }

                    }
                }
            }
        }
    }
}
