using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class MainMenuController : MonoBehaviour
{
    private void OnEnable()
    {
        VisualElement root = GetComponent<UIDocument>().rootVisualElement;

        Button newGameButton = root.Q<Button>("NewGameButton");
        Button shootingRangeButton = root.Q<Button>("ShootingRangeButton");
        Button settingsButton = root.Q<Button>("SettingsButton");
        Button exitButton = root.Q<Button>("ExitButton");

        shootingRangeButton.clicked += () => SceneManager.LoadScene(2); //TODO: SceneManagerEnum
        settingsButton.clicked += () => SceneManager.LoadScene(3);
        settingsButton.clicked += () => Application.Quit();
    }
}
