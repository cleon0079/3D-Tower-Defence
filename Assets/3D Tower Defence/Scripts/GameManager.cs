using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    // End game UI and text
    [SerializeField] GameObject endUI;
    [SerializeField] Text endText;

    // If we have lose or not
    [System.NonSerialized] public bool isLose = false;

    // Get all the enemy spawner to stop spawning enemy when we lose
    [SerializeField] GameObject enemySpawnerGameObject01;
    [SerializeField] GameObject enemySpawnerGameObject02;
    [SerializeField] GameObject enemySpawnerGameObject03;
    EnemySpawner enemySpawner01;
    EnemySpawner enemySpawner02;
    EnemySpawner enemySpawner03;

    public LayerMask towerBaseMask;
    Ray ray;
    RaycastHit hitInfo; 
    TowerBase lastHit = null;

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

        enemySpawner01 = enemySpawnerGameObject01.GetComponent<EnemySpawner>();
        enemySpawner02 = enemySpawnerGameObject02.GetComponent<EnemySpawner>();
        enemySpawner03 = enemySpawnerGameObject03.GetComponent<EnemySpawner>();
    }

    void Update()
    {
        ChangeTheBaseColor();
    }

    public void ChangeTheBaseColor()
    {
        if (lastHit != null)
        {
            lastHit.OutBase();
            lastHit = null;
        }

        ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out hitInfo, 1000f, towerBaseMask))
        {
            TowerBase towerBase = hitInfo.collider.gameObject.GetComponent<TowerBase>();
            towerBase.OnBase();
            lastHit = towerBase;
        }
    }

    public void Win()
    {
        // When win show up the UI
        endUI.SetActive(true);
        endText.text = "Win";
        isLose = false;
    }

    public void Lose()
    {
        // When lose show up the UI
        isLose = true;
        endUI.SetActive(true);
        endText.text = "Lose";

        // Stop spawning new enemy
        enemySpawner01.Stop();
        enemySpawner02.Stop();
        enemySpawner03.Stop();
    }

    public void Retry()
    {
        // Reload game scene
        isLose = false;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void MainMenu()
    {
        // Load Mainmenu scene
        isLose = false;
        SceneManager.LoadScene(0);
    }
}