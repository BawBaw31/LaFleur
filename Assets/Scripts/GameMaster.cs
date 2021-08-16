using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameMaster : MonoBehaviour
{
    public void GoToGameScene() {
        SceneManager.LoadScene("Game");
    }

    public void GoToMainMenu() {
        SceneManager.LoadScene("MainMenu");
    }

    public void GoToScoresScene() {
        SceneManager.LoadScene("Scores");
    
    }

    public void GoToPostScoreScene() {
        SceneManager.LoadScene("PostScore");
    }

    public void Restart() {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void Quit() {
        Application.Quit();
    }

}
