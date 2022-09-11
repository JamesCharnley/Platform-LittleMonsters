using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HorizontalMove_Button : Button_Touch
{
    [SerializeField]
    float value = 1;
    protected override void StartAction()
    {
        if(value > 0)
        {
            InputManager.instance.horizontalAxisPos = value;
        }
        else if(value < 0)
        {
            InputManager.instance.horizontalAxisNeg = value;
        }
    }
    protected override void EndAction()
    {
        if (value > 0)
        {
            InputManager.instance.horizontalAxisPos = 0;
        }
        else if (value < 0)
        {
            InputManager.instance.horizontalAxisNeg = 0;
        }
    }
}
