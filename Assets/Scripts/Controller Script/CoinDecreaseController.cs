using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CoinDecreaseController : MonoBehaviour
{
    public Text coinText;
    public Text TotalCoinText;
    public GameObject FailCanvas;

    public int totalCoin;

    private void Awake()
    {
        coinText.text = totalCoin.ToString();
    }
    private void Update()
    {
        TotalCoinScore();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Item")
        {
            EventManager.onItemDropped?.Invoke();
        }
    }
    private void ItemDropped()
    {

        if (totalCoin != 0 & totalCoin > 0)
        {
            totalCoin--;
            coinText.text = totalCoin.ToString();

        }
        else if (totalCoin == 0)
        {
           
            FailCanvas.SetActive(true);
             Time.timeScale = 0;
            coinText.text = totalCoin.ToString();
        }

    }
    private void TotalCoinScore()
    {
        if (TotalCoinText != null)
        {
            TotalCoinText.text = totalCoin.ToString();
        }

    }

    private void OnEnable()
    {
        EventManager.onItemDropped.AddListener(ItemDropped);
    }
    private void OnDisable()
    {
        EventManager.onItemDropped.RemoveListener(ItemDropped);
    }

}
