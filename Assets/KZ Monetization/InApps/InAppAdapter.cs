using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Purchasing;
using System.Collections;
using UnityEngine.Events;

namespace Mobile.InApps
{
    [DisallowMultipleComponent]
    public class InAppAdapter : MonoBehaviour
    {
        [SerializeField] PurchaseType m_PurchaseType;
        [SerializeField] Text LocalizedPriceText;
        [SerializeField] bool IsRestore = false;

        int index => (int)m_PurchaseType;

        void OnEnable()
        {
            if (!IsRestore)
                StartCoroutine(SetLocalPrice());
        }

        public void BuyInApp()
        {
            AdsManager.Instance.ExtendAppOpenTime();
            InAppsManager.Instance.BuyProduct(m_PurchaseType, index);
        }

        public void RestoreInApps()
        {
            InAppsManager.Instance.RestorePurchase();
        }

        IEnumerator SetLocalPrice()
        {
            string price = InAppsManager.Instance.InAppKeys[index].PriceInDollars;
            string id = "InAppId_" + InAppsManager.Instance.InAppKeys[index].Id;
            SetPriceTag(PlayerPrefs.GetString(id, price));

            Product p = InAppsManager.Instance.GetProductDetail(index); // GetProductDetail(value) also verifys InApp initialization!
            yield return new WaitForSecondsRealtime(.1f);

            if (p != null)
            {
                if (p.availableToPurchase)
                {
                    string localPrice = p.metadata.localizedPriceString.ToString();
                    PlayerPrefs.SetString(id, localPrice);
                    SetPriceTag(localPrice);
                }
            }
        }

        void SetPriceTag(string price)
        {
            if (LocalizedPriceText != null)
                LocalizedPriceText.text = price;
        }
    }
}
