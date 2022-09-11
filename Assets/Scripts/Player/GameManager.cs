using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Advertisements;

public class GameManager : MonoBehaviour, IUnityAdsListener
{

    string gameID = "4879589";

    int scenecount = 0;
    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(this);
        SceneManager.sceneLoaded += NewScene;
        Advertisement.AddListener(this);
        Advertisement.Initialize(gameID, false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void NewScene(Scene _scene, LoadSceneMode _mode)
    {
        scenecount++;
        if (scenecount > 0)
        {
            ShowAd();
        }
        
        
    }

    void ShowAd()
    {
        const string PlacementId = "Rewarded_Android";
        if(Advertisement.IsReady())
        {
            Advertisement.Show(PlacementId);
        }
    }

    private void OnApplicationQuit()
    {
        Advertisement.RemoveListener(this);
    }
    private void OnDestroy()
    {
        Advertisement.RemoveListener(this);
    }
    public void OnUnityAdsDidError(string message)
    {
        //YOUR CODE
        Debug.Log(message);
    }

    public void OnUnityAdsDidFinish(string placementId, ShowResult result)
    {
        //YOUR CODE
        switch (result)
        {
            case ShowResult.Finished:
                Debug.Log("Ad Finished -- Result: Player viewed complete Ad");
                break;
            case ShowResult.Skipped:
                Debug.Log("Ad Finished -- Result: Player Skipped Ad "); break;
            case ShowResult.Failed:
                Debug.Log("Problem showing Ad "); break;
        }
    }

    public void OnUnityAdsDidStart(string placementId)
    {
        //YOUR CODE
        Debug.Log("Ad Started");
    }

    public void OnUnityAdsReady(string placementId)
    {
        //YOUR CODE
        Debug.Log("Ad Ready");
    }
}

