using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnLeftSignals : MonoBehaviour
{
 
    public GameObject LeftOutline;
    public GameObject RightOutline;
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            LeftOutline.SetActive(true);
             RightOutline.SetActive(false);
        }

    }
 
}
