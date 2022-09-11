using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartGame_Button : Button_Touch
{
    protected override void StartAction()
    {
        SceneManager.LoadScene(1);
    }
}
