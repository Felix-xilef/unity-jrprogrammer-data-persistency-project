using UnityEngine;
using UnityEngine.UI;

public class MainGameBestScoreText : MonoBehaviour {

    private Text scoreText;


    private void Start() {
        scoreText = GetComponent<Text>();

        UpdateText(
            GameManager.Instance.BestScore
        );

        GameManager.Instance.onBestScoreUpdated += UpdateText;
    }

    private void OnDestroy() {
        GameManager.Instance.onBestScoreUpdated -= UpdateText;
    }


    private void UpdateText(BestScoreData bestScore) {
        scoreText.text = $"Best Score: {bestScore.username} : {bestScore.score}";
    }
}
