using KZ.Utils;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Purchasing;

[RequireComponent(typeof(UnityInAppPurchasing))]
public class InAppsManager : SingletonX<InAppsManager>
{
    #region Instance + Members

    public InAppKey[] InAppKeys;
    UnityInAppPurchasing InAppPurchasing;
    public bool InAppInitialized => InAppPurchasing.IsInitialized();

    #endregion

    #region MonoBehaviour
    void Awake()
    {
        if (m_Instance == null)
            m_Instance = this;
    }

    IEnumerator Start()
    {
        yield return new WaitForSeconds(2f);
        InAppPurchasing = GetComponent<UnityInAppPurchasing>();
        InAppPurchasing.Initialize();
    }

    public void BuyProduct(PurchaseType type, int index)
    {
        InAppPurchasing.BuyProduct(InAppKeys[index].Id, type);
    }


    public Product GetProductDetail(int index)
    {
        if (InAppInitialized)
            return InAppPurchasing.GetProductDetail(InAppKeys[index].Id);
        else
            return null;
    }
    public void RestorePurchase()
    {
        InAppPurchasing.RestorePurchases();
    }

    #endregion

    public void OnPurchaseComplete(PurchaseType type)
    {
        ThreadDispatcher.Enqueue(() =>
        {
            switch (type)
            {
                case PurchaseType.Remove_Ads: InAppPurchaseMethods.RemoveAds(); break;
                case PurchaseType.Unlock_All: InAppPurchaseMethods.UnlockAll(); break;
                case PurchaseType.Add_Coins1000: InAppPurchaseMethods.AddCoins1000(); break;
                case PurchaseType.Add_Coins2500: InAppPurchaseMethods.Add_Coins2500(); break;
                case PurchaseType.Add_Coins5000: InAppPurchaseMethods.Add_Coins5000(); break;
                case PurchaseType.Add_Coins10000: InAppPurchaseMethods.Add_Coins10000(); break;
            }
        });
    }
}

public enum PurchaseType
{
    Remove_Ads,
    Unlock_All,
    Add_Coins1000,
    Add_Coins2500,
    Add_Coins5000,
    Add_Coins10000,
    Scene_Unlock,
    BundleUnlock,
}

#region InAppKey Serializable

[System.Serializable]
public class InAppKey
{
    public string Id;
    public ProductType ProductType;
    public PurchaseType PurchaseType;
    public string PriceInDollars;

    public InAppKey(string id, ProductType productType, PurchaseType purchaseType, string priceInDollar)
    {
        this.Id = id;
        this.ProductType = productType;
        this.PurchaseType = purchaseType;
        this.PriceInDollars = priceInDollar;
    }
}
#endregion