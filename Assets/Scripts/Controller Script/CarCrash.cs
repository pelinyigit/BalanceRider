using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarCrash : MonoBehaviour
{
   public GameObject FailCanvas;
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            FailCanvas.SetActive(true);
            Time.timeScale = 0;
        }
    }
}
