using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

[DefaultExecutionOrder(1000)]
public class BackMenuController : MonoBehaviour {

    private Button backButton;

    private struct ElementId {
        public const string BackButton = "back-button";
    }


    private void OnEnable() {
        var ui = GetComponent<UIDocument>().rootVisualElement;

        backButton = ui.Q<Button>(
            ElementId.BackButton
        );
        backButton.clicked += GoBackToMenu;
    }

    private void OnDisable() {
        backButton.clicked -= GoBackToMenu;
    }


    private void GoBackToMenu() {
        SceneManager.LoadScene(
            (int) SceneId.Menu
        );
    }
}
