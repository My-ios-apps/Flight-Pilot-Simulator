using Mobile.InApps;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Purchasing;

public class UnityInAppPurchasing : MonoBehaviour, IStoreListener
{
    private IStoreController m_StoreController;
    private IAppleExtensions m_AppleExtensions;
    private IExtensionProvider m_StoreExtensionProvider;
    private IGooglePlayStoreExtensions m_GooglePlayStoreExtensions;
    bool m_IsGooglePlayStoreSelected = false;
    List<string> productIdsForRestore;
    bool IsRestoring = false;

    public void Start()
    {
        IsRestoring = false;
        productIdsForRestore = new List<string>();
    }

    public void Initialize()
    {
        if (m_StoreController == null)
            InitializePurchasing();
    }

    void InitializePurchasing()
    {
        if (IsInitialized())
            return;

        var module = StandardPurchasingModule.Instance();
        var builder = ConfigurationBuilder.Instance(module);
        m_IsGooglePlayStoreSelected = Application.platform == RuntimePlatform.Android && module.appStore == AppStore.GooglePlay;


        int length = InAppsManager.Instance.InAppKeys.Length;
        for (int i = 0; i < length; i++)
            builder.AddProduct(InAppsManager.Instance.InAppKeys[i].Id, InAppsManager.Instance.InAppKeys[i].ProductType);

        UnityPurchasing.Initialize(this, builder);
    }


    public bool IsInitialized()
    {
        return m_StoreController != null && m_StoreExtensionProvider != null;
    }

    [HideInInspector] public PurchaseType m_PurchaseType;
    bool m_PurchaseInProgress;

    public void BuyProduct(string productId, PurchaseType type)
    {
        if (!m_PurchaseInProgress)
        {
            m_PurchaseInProgress = true;

            m_PurchaseType = type;
            BuyProductID(productId);
        }
    }

    void BuyProductID(string productId)
    {
        productIdsForRestore.Clear();

        if (IsInitialized())
        {
            Product product = m_StoreController.products.WithID(productId);
            if (product != null && product.availableToPurchase)
            {
                Debug.Log(string.Format("Purchasing product asychronously: '{0}'", product.definition.id));
                m_StoreController.InitiatePurchase(product, "developerPayload");
            }
            else
            {
                Debug.Log("BuyProductID: FAIL. Not purchasing product, either is not found or is not available for purchase");
                m_PurchaseInProgress = false;
            }
        }
        else
        {
            Debug.Log("BuyProductID FAIL. Not initialized.");
            InitializePurchasing();
            m_PurchaseInProgress = false;
        }
    }

    public void RestorePurchases()
    {
        if (!IsInitialized())
        {
            Debug.Log("RestorePurchases FAIL. Not initialized.");
            return;
        }

        Debug.Log("RestorePurchases Started ...");

        if (m_IsGooglePlayStoreSelected)
        {
            m_GooglePlayStoreExtensions.RestoreTransactions(OnTransactionsRestored);
        }
        else if (Application.platform == RuntimePlatform.IPhonePlayer || Application.platform == RuntimePlatform.OSXPlayer)
        {
            IsRestoring = true;
            m_AppleExtensions.RestoreTransactions(OnTransactionsRestored);
        }
        else
        {
            Debug.Log("RestorePurchases FAIL. Not supported on this platform. Current = " + Application.platform);
        }
    }

    public Product GetProductDetail(string productId)
    {
        return m_StoreController.products.WithID(productId);
    }
    private void OnTransactionsRestored(bool success)
    {
        Debug.Log("Transactions restored." + success);
        if (success)
        {
            for (int i = 0; i < productIdsForRestore.Count; i++)
            {
                for (int j = 0; j < InAppsManager.Instance.InAppKeys.Length; j++)
                {
                    var purchaseType = InAppsManager.Instance.InAppKeys[i].PurchaseType;
                    var productType = InAppsManager.Instance.InAppKeys[i].ProductType;
                    if (InAppsManager.Instance.InAppKeys[j].Id == productIdsForRestore[i])
                    {
                        if (productType == ProductType.NonConsumable ||
                            productType == ProductType.Subscription)
                        {
                            InAppsManager.Instance.OnPurchaseComplete(purchaseType);
                        }
                        break;
                    }
                }
            }

            if (IsRestoring)
            {
                MobileToast.Show(productIdsForRestore.Count + " Products Restored Successfully", true);
                IsRestoring = false;
            }

            productIdsForRestore.Clear();
        }
    }

    //private void OnTransactionsRestored(bool success)
    //{
    //    //int RestoredCount = 0;
    //    if (success)
    //    {
    //        int length = InAppsManager.Instance.InAppKeys.Length;
    //        for (int i = 0; i < length; i++)
    //        {
    //            var purchaseType = InAppsManager.Instance.InAppKeys[i].PurchaseType;
    //            var productType = InAppsManager.Instance.InAppKeys[i].ProductType;

    //            if (productType == ProductType.NonConsumable ||
    //                productType == ProductType.Subscription)
    //            {
    //                InAppsManager.Instance.OnPurchaseComplete(purchaseType);
    //              //  RestoredCount++;
    //            }
    //        }

    //        //MobileToast.Show_DevMode(RestoredCount + " Products Restored Successfully");
    //    }
    //}

    //  
    // --- IStoreListener
    //

    public void OnInitialized(IStoreController controller, IExtensionProvider extensions)
    {
        Debug.Log("OnInitialized: PASS");

        m_StoreController = controller;
        m_StoreExtensionProvider = extensions;
        m_AppleExtensions = extensions.GetExtension<IAppleExtensions>();
        m_GooglePlayStoreExtensions = extensions.GetExtension<IGooglePlayStoreExtensions>();
    }


    public void OnInitializeFailed(InitializationFailureReason error)
    {
        Debug.Log("OnInitializeFailed InitializationFailureReason:" + error);
    }

    public void OnInitializeFailed(InitializationFailureReason error, string message)
    {
        Debug.Log("OnInitializeFailed InitializationFailureReason:" + error + message);
    }

    public PurchaseProcessingResult ProcessPurchase(PurchaseEventArgs args)
    {
        for (int i = 0; i < InAppsManager.Instance.InAppKeys.Length; i++)
        {
            if (String.Equals(args.purchasedProduct.definition.id, InAppsManager.Instance.InAppKeys[i].Id, StringComparison.Ordinal))
            {
                Debug.Log(string.Format("ProcessPurchase: PASS. Product: '{0}'", args.purchasedProduct.definition.id));
                InAppsManager.Instance.OnPurchaseComplete(m_PurchaseType);
                break;
            }
        }

        if (IsRestoring)
            productIdsForRestore.Add(args.purchasedProduct.definition.id);

        m_PurchaseInProgress = false;
        return PurchaseProcessingResult.Complete;
    }

    public void OnPurchaseFailed(Product product, PurchaseFailureReason failureReason)
    {
        //Failed Invoke
        Debug.Log(string.Format("OnPurchaseFailed: FAIL. Product: '{0}', PurchaseFailureReason: {1}", product.definition.storeSpecificId, failureReason));
        m_PurchaseInProgress = false;
    }
}