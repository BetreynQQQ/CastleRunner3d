using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts
{
    public class PlayerScore : MonoBehaviour
    {
        [SerializeField] private Transform player;
        [SerializeField] private Text scoreText;
        private string score;

        private void Update()
        {
            score = ((int)(player.position.z / 2)).ToString();
            scoreText.text = score;
            DieScore();
        }

        private void DieScore()
        {
            if (Time.timeScale == 0)
                scoreText.text = "\nВы проиграли\n ваш счет: "+ score;
        }
    }
}
