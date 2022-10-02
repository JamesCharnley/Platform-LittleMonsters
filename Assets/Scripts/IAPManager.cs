using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Purchasing;
public class IAPManager : MonoBehaviour, IStoreListener
{

    // Set private variables
    private static IStoreController m_StoreController;
    private static IExtensionProvider m_StoreExtensionProvider;
    public static string kRemoveAds = "no_ads";
    // Start is called before the first frame update
    void Start()
    {
        if (m_StoreController == null)
        {
            //configure our connection to Purchasing
            InitializePurchasing();
        }
        
    }


    /* OnInitialize is called to check if the app can connect to Unity IAP or not.
It will keep trying in the background if not connected*/
    public void OnInitialized(IStoreController controller, IExtensionProvider
    extensions)
    {
        Debug.Log("OnInitialized: PASS");
        m_StoreController = controller;
        m_StoreExtensionProvider = extensions;

        
    }
    // Called when IAP have failed to initialize and logs a message to the console
    public void OnInitializeFailed(InitializationFailureReason error)
    {
        Debug.Log("OnInitializeFailed InitializationFailureReason:" + error);
    }
    private bool IsInitialized()
    {
        // Check if both the Purchasing references are set
        return m_StoreController != null && m_StoreExtensionProvider != null;
    }

    public void InitializePurchasing()
    {
        if (IsInitialized())
        {
            return;
        }
        // Collects products and store-specific configuration details
        var builder = ConfigurationBuilder.Instance
        (StandardPurchasingModule.Instance());
        // Add a product with a Unity IAP ID, type
        builder.AddProduct(kRemoveAds, ProductType.Consumable);
        //Initialize Unity IAP with the specified listener and configuration
        // Store Controller and Extension provider are set.
        UnityPurchasing.Initialize(this, builder);
    }

    //Purchasing has been initialized.
    // Call this function when you want to purchase the a product.
    public void BuyProductID(string productId)
    {
        if (IsInitialized())
        {
            
            Product product = m_StoreController.products.WithID(productId);
            if (product != null && product.availableToPurchase)
            {
                Debug.Log("Purchasing Product");
                m_StoreController.InitiatePurchase(product);
            }
            else
            {
                Debug.Log("Product either is not found or not available for purchase");
            }
        }
        else
        {
            Debug.Log("BuyProductID FAIL. Not initialized.");
        }
    }

    // A consumable product has been purchased by this user.
    public PurchaseProcessingResult ProcessPurchase(PurchaseEventArgs args)
    {
        if (String.Equals(args.purchasedProduct.definition.id, kRemoveAds,
        StringComparison.Ordinal))
        {
            Debug.Log("Purchasing Product");
            PlayerPrefs.SetInt("noAdsPurchased", 1);
            //mainMenuScript.noAdsButton.gameObject.SetActive(false);
        }
        else
        {
            Debug.Log("Unrecognized Product");
        }
        return PurchaseProcessingResult.Complete;
    }

    // Logs a message to the console telling us when a purchase failed
    public void OnPurchaseFailed(Product product, PurchaseFailureReason failureReason)
    {
        Debug.Log(string.Format("OnPurchaseFailed: FAIL. Product: {0}, PurchaseFailureReason: { 1}", product.definition.storeSpecificId, failureReason));
    }
}

