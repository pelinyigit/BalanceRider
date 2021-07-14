using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RestartButton : MonoBehaviour
{
    public GameObject SuccessCanvas;
    public GameObject FailCanvas;
    public GameObject StartCanvas;

    public void PlayAgain()
    {
        SceneManager.LoadScene("BalanceRider");
        SuccessCanvas.SetActive(false);
        FailCanvas.SetActive(false);
        StartCanvas.SetActive(false);
        Time.timeScale = 1;
    }

}
