using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class UIControl : MonoBehaviour
{
    [SerializeField]
    TextMeshProUGUI pointsText;
    [SerializeField] GameObject endOfLevelUI;
    // Start is called before the first frame update
    void Start()
    {
        if(endOfLevelUI.activeSelf)
        {
            endOfLevelUI.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(pointsText != null)
        {
            pointsText.text = GameManager.instance.GetPoints().ToString();
        }
    }

    public void OpenEndOfLevelUI()
    {
        endOfLevelUI.SetActive(true);
    }

    public void Continue()
    {
        GameManager.instance.ContinueToNextLevel();
    }
}
