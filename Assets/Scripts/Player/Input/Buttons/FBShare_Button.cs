using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FBShare_Button : Button_Touch
{
    protected override void StartAction()
    {
        FBManager fb = FindObjectOfType<FBManager>();
        if (fb != null)
        {
            fb.Share();
        }
    }
}

