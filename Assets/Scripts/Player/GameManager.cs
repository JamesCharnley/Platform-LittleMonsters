using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Advertisements;

public class GameManager : MonoBehaviour, IUnityAdsListener
{
    int currentPoints = 0;

    string gameID = "4879589";

    int scenecount = 0;

    public static GameManager instance;

    AudioSource audioSource;
    [SerializeField] AudioClip playerDieSound;

    public bool playAds = true;
    // Start is called before the first frame update
    void Start()
    {
        if(instance == null)
        {
            DontDestroyOnLoad(this.gameObject);
            instance = this;
        }
        else
        {
            Destroy(this.gameObject);
        }
        audioSource = GetComponent<AudioSource>();
        SceneManager.sceneLoaded += NewScene;

        Advertisement.AddListener(this);
        Advertisement.Initialize(gameID, false);

        if (PlayerPrefs.HasKey("noAdsPurchased"))
        {
            if(PlayerPrefs.GetInt("noAdsPurchased") == 1)
            {
                playAds = false;
            }
            
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PlayerDied()
    {
        audioSource.clip = playerDieSound;
        audioSource.Play();
        currentPoints = 0;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void AddPoints(int _number)
    {
        currentPoints += _number;
    }
    public int GetPoints()
    {
        return currentPoints;
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
        if(PlayerPrefs.HasKey("noAdsPurchased"))
        {
            if(PlayerPrefs.GetInt("noAdsPurchased") == 0)
            {
                const string PlacementId = "Rewarded_Android";
                if (Advertisement.IsReady())
                {
                    Advertisement.Show(PlacementId);
                }
            }
            
        }
        else
        {
            const string PlacementId = "Rewarded_Android";
            if (Advertisement.IsReady())
            {
                Advertisement.Show(PlacementId);
            }
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

    bool reachedEndOfLevel = false;
    public void EndOfLevel()
    {
        

        // run checks for acheivments
        LevelInfo levelInfo = FindObjectOfType<LevelInfo>();
        if (levelInfo != null)
        {
            if(GetPoints() == levelInfo.GetMaxPoints())
            {
                // got all available points achievment
                Social.ReportProgress("CgkI_ouZr90CEAIQAg", 100, (bool success) => { });
            }
            if(SceneManager.GetActiveScene().buildIndex == 1)
            {
                Social.ReportProgress("CgkI_ouZr90CEAIQAQ", 100, (bool success) => { });
            }
        }

        if(PlayerPrefs.HasKey("noAdsPurchased"))
        {
            if(PlayerPrefs.GetInt("noAdsPurchased") == 0)
            {
                // open score UI
                reachedEndOfLevel = true;
                UIControl uIControl = FindObjectOfType<UIControl>();
                if (uIControl != null)
                {
                    uIControl.OpenEndOfLevelUI();
                }
            }
        }
        else
        {
            // open score UI
            reachedEndOfLevel = true;
            UIControl uIControl = FindObjectOfType<UIControl>();
            if (uIControl != null)
            {
                uIControl.OpenEndOfLevelUI();
            }
        }
        if (PlayerPrefs.HasKey("noAdsPurchased"))
        {
            if (PlayerPrefs.GetInt("noAdsPurchased") == 1)
            {
                FindObjectOfType<UIControl>().Continue();
            }
        }
        
    }

    public void UpdateJumpMilestone()
    {
        Social.ReportProgress("CgkI_ouZr90CEAIQAw", 100, (bool success) => { });
    }
    public void ContinueToNextLevel()
    {
        // load next level
        LevelInfo levelInfo = FindObjectOfType<LevelInfo>();
        if(levelInfo != null)
        {
            SceneManager.LoadScene(levelInfo.GetNextSceneIndex());
        }
    }
}

