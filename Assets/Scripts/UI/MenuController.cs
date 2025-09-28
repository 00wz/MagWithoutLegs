using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{
    [SerializeField] private UIDocument menuDocument;

    private void Awake()
    {
        if (menuDocument == null)
            menuDocument = GetComponent<UIDocument>();

        var root = menuDocument.rootVisualElement;
        var playButton = root.Q<Button>("PlayButton");
        var exitButton = root.Q<Button>("ExitButton");

        if (playButton != null)
            playButton.clicked += OnPlayClicked;
        if (exitButton != null)
            exitButton.clicked += OnExitClicked;
    }

    private void OnPlayClicked()
    {
        SceneManager.LoadScene("MainScene");
    }

    private void OnExitClicked()
    {
        Application.Quit();
        #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
        #endif
    }
}
