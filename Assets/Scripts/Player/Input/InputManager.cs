using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class InputManager : MonoBehaviour
{
    static public InputManager instance;
    public float horizontalAxis
    {
        get
        {
            return horizontalAxisNeg + horizontalAxisPos;
        }
    }
    public float horizontalAxisPos = 0;
    public float horizontalAxisNeg = 0;

    Button_Touch[] buttons;

    private void Start()
    {
        if(instance == null)
        {
            SceneManager.sceneLoaded += GetButtons;
            instance = this;
            buttons = FindObjectsOfType<Button_Touch>();
        }
        
    }
    public void GetButtons(Scene _scene, LoadSceneMode _mode)
    {
        buttons = FindObjectsOfType<Button_Touch>();
    }
    private void Update()
    {
        foreach (Touch touch in Input.touches)
        {

            foreach (Button_Touch _button in buttons)
            {
                _button.CheckBounds(touch.position, touch.phase);
            }
        }
    }
}
