using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ParkingScript : MonoBehaviour {
    public Image parking;
	public float fillRate=0.25f;
	public delegate void TaskComplete();
	public static event TaskComplete OnTaskComplete;
    void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag.Equals("Player"))
        {
            if (parking.fillAmount < 1)
            {
//				Debug.Log (fillRate);
				parking.fillAmount += Time.deltaTime * fillRate;
            }
            else
            {
                showGameOver();
            }
        }
    }

    void OnTriggerExit(Collider other)
    {
		if (other.gameObject.tag.Equals("Player"))
        {

            parking.fillAmount = 0f;
        }
    }

    public void showGameOver()
    {
		parking.gameObject.SetActive (false);
		if (OnTaskComplete != null) {
			OnTaskComplete ();
		}
    }
}
