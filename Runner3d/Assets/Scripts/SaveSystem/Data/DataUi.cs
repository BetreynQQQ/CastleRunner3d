using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.SaveSystem.Data
{
    public class DataUi : MonoBehaviour
    {
        [SerializeField] private Text scoreText;
        [SerializeField] private Text coinsText;
        [SerializeField] private SaveData data;

        private void Start()
        {
            data.Load();
            scoreText.text = data.Score.ToString();
            coinsText.text = data.Coins.ToString();

        }
    }
}