using Assets.Scripts.SaveSystem.Data;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts
{
    public class PlayerScore : MonoBehaviour
    {
        [SerializeField] private Transform player;
        [SerializeField] private Text scoreText;        
        private int score;
        
        private void Update()
        {
            score = ((int)(player.position.z / 2));
            scoreText.text = score.ToString();
                                
        }

        
    }
}
