using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FBShare_Button : MonoBehaviour
{
    public void OnClick()
    {
        FBManager fb = FindObjectOfType<FBManager>();
        if (fb != null)
        {
            fb.Share();
        }
    }
}

