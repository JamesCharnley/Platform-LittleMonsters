using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RemoveAds_Button : MonoBehaviour
{
    private void Start()
    {
        if(PlayerPrefs.HasKey("noAdsPurchased"))
        {
            if(PlayerPrefs.GetInt("noAdsPurchased") == 1)
            {
                transform.gameObject.SetActive(false);
            }
        }
    }
    public void OnClick()
    {
        IAPManager iAPManager = FindObjectOfType<IAPManager>();
        if(iAPManager != null)
        {
            iAPManager.BuyProductID("no_ads");
        }
    }
}
