using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Twitter_ShareButton : MonoBehaviour
{
    public void OnClick()
    {
        OpenTwitter();
    }

    public void OpenTwitter()
    {
        string twitterAddress = "http://twitter.com/intent/tweet";
        string message = "GET THIS AWERSOME GAME";//text string
        string descriptionParameter = "Punchy Punch";
        string appStoreLink = "https://play.google.com/store/apps/details?id = com.growlgamesstudio.pizZapMania";
        Application.OpenURL(twitterAddress + "?text=" + WWW.EscapeURL(message + "n" + descriptionParameter + "n" + appStoreLink));
    }
}
