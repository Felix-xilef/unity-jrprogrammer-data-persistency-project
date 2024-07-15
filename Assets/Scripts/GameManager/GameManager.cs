using System;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

    private const string saveFileName = "saved-data.json";
    private string saveFilePath;


    public static GameManager Instance { get; private set; }

    private BestScoreData _bestScore = new();
    public BestScoreData BestScore {
        get => _bestScore;
        private set {
            _bestScore = value;

            onBestScoreUpdated?.Invoke(
                _bestScore
            );
        }
    }

    private string currentUsername;

    public Action<BestScoreData> onBestScoreUpdated;


    private void Awake() {
        if (Instance == null) {
            Instance = this;

            saveFilePath = $"{Application.persistentDataPath}/{saveFileName}";

            DontDestroyOnLoad(
                gameObject
            );

            LoadBestScore();

        } else {
            Destroy(gameObject);
        }
    }


    public void RegisterIfBestScore(int score) {
        if (score > BestScore.score) {
            BestScore = new() {
                username = currentUsername,
                score = score,
            };

            SaveBestScore();
        }
    }

    public void StartGame(string username) {
        currentUsername = username;

        SceneManager.LoadScene(
            (int) SceneId.Main
        );
    }


    private void SaveBestScore() {
        File.WriteAllText(
            saveFilePath,
            JsonUtility.ToJson(
                BestScore
            )
        );
    }

    private void LoadBestScore() {
        if (File.Exists(saveFilePath)) {
            var fileContent = File.ReadAllText(
                saveFilePath
            );

            BestScore = JsonUtility.FromJson<BestScoreData>(
                fileContent
            );

        } else {
            BestScore = new BestScoreData() {
                username = "",
                score = 0,
            };
        }
    }
}
