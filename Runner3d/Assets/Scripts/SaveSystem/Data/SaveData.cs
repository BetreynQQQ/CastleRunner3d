using SecondChanceSystem.SaveSystem;
using UnityEditor;
using UnityEngine;

namespace Assets.Scripts.SaveSystem.Data
{
    [CreateAssetMenu]
    public class SaveData : ScriptableObject
    {

        [SerializeField] private int _score;
        [SerializeField] private int _coins;

        public int Score { get { return _score; } private set { Score = value; } }
        public int Coins { get { return _coins; } private set { Coins = value; } }


        public void SaveCoins(int coins)
        {
            Debug.Log("Получено монет -> " + coins);
            Load();
           Debug.Log("Загруженно с базы -> "+ _coins);
            _coins += coins;
           Debug.Log("Выгруженно в базу -> " + _coins);

            Save();
        }

        public void SaveScore(int score)
        {
            Load();
            if (_score > score)
                return;
            _score = score;
            Save();
        }

        public void Load()
        {
            SaveData loadData = (SaveData)SaveLoadSystem.LoadData(this);
            if (loadData == null)
                return;
            _score = loadData._score;
            _coins = loadData._coins;            
        }

        private void Save()
        {

            //Debug.Log(coins + "Сохраняем");
            SaveLoadSystem.SaveData(this);
            
        }
    }
}