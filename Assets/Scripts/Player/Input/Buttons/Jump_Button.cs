using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jump_Button : Button_Touch
{
    protected override void StartAction()
    {
        Debug.Log("Jump StartAction");
        FindObjectOfType<Player>().Jump();
    }
    protected override void EndAction()
    {
        
    }
}
