using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    // End game UI and text
    public GameObject endUI;
    public Text endText;

    private EnemySpawner enemySpawner;

    public static GameManager instance;

    void Awake()
    {
        // Null check for the instance
        if(instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this);
        }
        enemySpawner = GetComponent<EnemySpawner>();
    }

    public void Win()
    {
        // When win show up the UI
        endUI.SetActive(true);
        endText.text = "Win";
    }

    public void Lose()
    {
        // Stop spawning new enemy
        enemySpawner.Stop();

        // When lose show up the UI
        endUI.SetActive(true);
        endText.text = "Lose";
    }

    public void Retry()
    {
        // Reload game scene
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void MainMenu()
    {
        // Load Mainmenu scene
        SceneManager.LoadScene(0);
    }
}