
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Facebook.Unity;
using System.Linq;
using UnityEngine.UI;
using TMPro;
public class FBManager : MonoBehaviour
{
    
    [SerializeField] GameObject[] logs;
    bool isActivated = false;
    void LogToScreen(string _text)
    {
        foreach(GameObject textGO in logs)
        {
            TextMeshProUGUI text = textGO.GetComponent<TextMeshProUGUI>();
            if(text.text == "zzzzzzzzzzzzzzzzzzzz")
            {
                text.text = _text;
                break;
            }
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        LogToScreen("Awake");
        foreach(GameObject textGO in logs)
        {
            TextMeshProUGUI text = textGO.GetComponent<TextMeshProUGUI>();
            //text.SetText("zzzzzzzzzzzzzzzzzzzz");
        }
        if (!FB.IsInitialized)
        { // if not initialized
            LogToScreen("FB !initialised");
            FB.Init(); // initialize
            if (FB.IsInitialized)
            {
                FB.ActivateApp();
                Debug.Log("Facebook Activated");
                LogToScreen("Facebook Activated");
            }
            else
            {
                Debug.Log("Could not initialize Facebook");
                LogToScreen("Could not initialize Facebook");
            }
        }
        else
        {
            LogToScreen("FB Initialized before Activate");
            // Send an app activation event to Facebook when your app is activated.
            FB.ActivateApp(); // Activate event
            Debug.Log("Facebook Activated");
            LogToScreen("Facebook Activated");
        }

    }

    public void Share()
    {
        if(!FB.IsInitialized)
        {
            LogToScreen("FB !init share func");
            Debug.Log("Share(): !FB.Initialized");
        }
        if(FB.IsInitialized && !isActivated)
        {
            isActivated = true;
            Debug.Log("Share(): FB.Initialised-> FB.Activate()");
            FB.ActivateApp();
        }
        if (!FB.IsLoggedIn)
        {
            Debug.Log("!Logged in-> Attempt Login");
            // Debug.Log("User Not Logged In");
            var permissions = new List<string>() { "email" };
            FB.LogInWithReadPermissions(permissions, callback: onLogin);
            
            LogToScreen("Not Logged in");
        }
        else
        {
            Debug.Log("ShareLink");
            LogToScreen("Share Link");
            FB.ShareLink(
            contentURL: new System.Uri("https://play.google.com/store/apps/details?id=com.grimstargames.littlemonstersje"),
            callback: onShare);

        //contentTitle: "GrimStarGames - LittleMonsters: Jungle Edition",
            //contentDescription: "Like and Share my page",
        }
    }

    private void onLogin(ILoginResult result)
    {
        if(result.Error != null)
        {
            Debug.Log("Login Error: " + result.Error);
            LogToScreen("Login Error: " + result.Error);
        }
        if (result.Cancelled)
        {
            Debug.Log(result.Error);
            Debug.Log(" user cancelled login");
            LogToScreen(" user cancelled login");
        }
        else
        {
            Share(); // call share() again
            Debug.Log("Call Share From OnLogin");
            LogToScreen("Call Share From OnLogin");
        }
    }
    private void onShare(IShareResult result)
    {
        if (result.Cancelled || !string.IsNullOrEmpty(result.Error))
        {
            Debug.Log("sharelink error: " + result.Error);
            LogToScreen("sharelink error: " + result.Error);
        }
        else if (!string.IsNullOrEmpty(result.PostId))
        {
            Debug.Log("Link shared");
            LogToScreen("Link shared");
        }
        Debug.Log("OnShare");
    }

    void Update()
    {

    }
}

