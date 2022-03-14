using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EndGamePanel : MonoBehaviour
{
    [SerializeField] private Text description;


    private void Awake()
    {
        PlayerController.WinPlayerEvent += LogScore;
        Debug.Log("Podpisalsa");
    }


    private void LogScore(int coins)
    {
        Debug.Log("LOGSCORE" + coins);
        string resultString = coins.ToString();        
        description.text = resultString;
    }

    private void OnDestroy()
    {
        PlayerController.WinPlayerEvent -= LogScore;
    }
}
