using Unity.Advertisement.IosSupport;
using UnityEngine;
using static Unity.Advertisement.IosSupport.ATTrackingStatusBinding;

public class IDFA_Handler : MonoBehaviour
{
    private ATTrackingStatusBinding.AuthorizationTrackingStatus m_PreviousStatus;
    private bool m_Once;

    // Start is called before the first frame update
    private void Start()
    {
        var status = ATTrackingStatusBinding.GetAuthorizationTrackingStatus();
        Debug.LogFormat("Tracking status at start: {0}", status);
        m_PreviousStatus = status;

        SkAdNetworkBinding.SkAdNetworkUpdateConversionValue(0);
        SkAdNetworkBinding.SkAdNetworkRegisterAppForNetworkAttribution();
    }

    // Update is called once per frame
    private void Update()
    {
        if(!m_Once)
        {
            m_Once = true;
            ATTrackingStatusBinding.RequestAuthorizationTracking();
            LoadAdsNow(m_PreviousStatus);
        }

        //var status = ATTrackingStatusBinding.GetAuthorizationTrackingStatus();
        //if(m_PreviousStatus != status)
        //{
        //    m_PreviousStatus = status;
        //    Debug.LogFormat("Tracking status updated: {0}", status);
        //}
       
    }

    void LoadAdsNow(AuthorizationTrackingStatus status)
    {
        if(status== AuthorizationTrackingStatus.AUTHORIZED)
        {
            Debug.LogFormat("Tracking status AUTHORIZED ", status);
        }
        else if (status == AuthorizationTrackingStatus.DENIED)
        {
            Debug.LogFormat("Tracking status DENIED ", status);
        }
        else if (status == AuthorizationTrackingStatus.RESTRICTED)
        {
            Debug.LogFormat("Tracking status RESTRICTED ", status);
        }
        else if (status == AuthorizationTrackingStatus.NOT_DETERMINED)
        {
            Debug.LogFormat("Tracking status NOT_DETERMINED ", status);
        }
        Debug.Log("Inilizing ads now ");
    }
}
