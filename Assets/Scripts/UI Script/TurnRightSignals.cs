using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnRightSignals : MonoBehaviour
{

    public GameObject RightOutline;
    public GameObject LeftOutline;
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            RightOutline.SetActive(true);
             LeftOutline.SetActive(false);
        }

    }

}
