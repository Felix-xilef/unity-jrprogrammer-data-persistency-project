using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

[DefaultExecutionOrder(1000)]
public class MainMenuController : MonoBehaviour {

    private Label bestScoreText;
    private TextField usernameInput;

    private Button startButton;
    private Button exitButton;


    private struct ElementId {
        public const string BestScoreText = "best-score-text";
        public const string UsernameInput = "username-input";
        public const string StartButton = "start-button";
        public const string ExitButton = "exit-button";
    }


    private void Start() {
        GameManager.Instance.onBestScoreUpdated += UpdateBestScoreText;
    }

    private void OnEnable() {
        var ui = GetComponent<UIDocument>().rootVisualElement;

        bestScoreText = ui.Q<Label>(
            ElementId.BestScoreText
        );
        UpdateBestScoreText(
            GameManager.Instance.BestScore
        );

        usernameInput = ui.Q<TextField>(
            ElementId.UsernameInput
        );

        startButton = ui.Q<Button>(
            ElementId.StartButton
        );
        startButton.clicked += StartGame;

        exitButton = ui.Q<Button>(
            ElementId.ExitButton
        );
        exitButton.clicked += ExitGame;
    }

    private void OnDisable() {
        startButton.clicked -= StartGame;
        exitButton.clicked -= ExitGame;
    }

    private void OnDestroy() {
        GameManager.Instance.onBestScoreUpdated -= UpdateBestScoreText;
    }


    private void UpdateBestScoreText(BestScoreData bestScore) {
        bestScoreText.text = $"{bestScore.username} - {bestScore.score}";
    }

    private void StartGame() {
        GameManager.Instance.StartGame(
            usernameInput.value
        );
    }

    private void ExitGame() {
        #if UNITY_EDITOR
            EditorApplication.ExitPlaymode();
        #else
            Application.Quit();
        #endif
    }
}
