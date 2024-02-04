using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowIDFA : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject IDFA;
    void Start()
    {
        StartCoroutine(Show());
    }

    IEnumerator Show()
    {
        yield return new WaitForSeconds(2f);
        IDFA.SetActive(true);

    }
}
