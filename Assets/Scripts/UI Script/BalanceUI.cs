using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BalanceUI : MonoBehaviour
{
    private float balancePoint;
    private float clampPoint = 2f;
    public Image BalanceProgressBar;
    private float BalanceProgress;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        BalanceCalculator();
        ImageController();
        balancePoint = Mathf.Clamp(balancePoint, -clampPoint, clampPoint);

       // Debug.Log(balancePoint);
    }

    private void BalanceCalculator()
    {
        if(transform.localEulerAngles.z > 300)
        {
            balancePoint = transform.localEulerAngles.z - 360f;
        } else
        {
            balancePoint = transform.localEulerAngles.z ;
        }
        BalanceProgress = Mathf.Abs(balancePoint);
    }
    private void ImageController()
    {
        BalanceProgressBar.fillAmount = BalanceProgress/2f;
    }
}
