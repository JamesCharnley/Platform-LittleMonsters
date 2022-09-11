using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Action_Button : Button_Touch
{
    protected override void StartAction()
    {
        FindObjectOfType<Player>().Action();
        
    }
    protected override void EndAction()
    {

    }
}
