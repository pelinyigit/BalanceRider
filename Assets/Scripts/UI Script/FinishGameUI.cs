using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinishGameUI : MonoBehaviour
{
    public GameObject SuccessCanvas;

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            SuccessCanvas.SetActive(true);
            Time.timeScale = 0;
        }
    }
}
