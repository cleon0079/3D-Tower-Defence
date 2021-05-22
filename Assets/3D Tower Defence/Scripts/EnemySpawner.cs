using System.Collections;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    // How many enemy still alive
    public static int enemyAliveCount = 0;

    // How many waves and there var
    [SerializeField] Wave[] waves;

    // Position that enemy spawn
    [SerializeField] Transform start;

    // How long in two waves
    [SerializeField] float waveTime = 1;

    // Start is called before the first frame update
    void Start() => StartCoroutine(SpawnEnemy());

    // When we lose then stop to spawn the enemy
    public void Stop() => StopCoroutine(SpawnEnemy());

    IEnumerator SpawnEnemy()
    {
        // Loop all the wave we make
        foreach (Wave wave in waves)
        {
            for (int i = 0; i < wave.count; i++)
            {
                // Spawn a new enemy at the start position
                GameObject.Instantiate(wave.enemyPrefab, start.position, Quaternion.identity);
                enemyAliveCount++;

                // If its not the last enemy then wait for the spawn time and spawn a new enemy
                if (i != wave.count - 1)
                {
                    yield return new WaitForSeconds(wave.spawnTime);
                }
            }

            // If there is enemy still alive the dont run the next line
            while (enemyAliveCount > 0)
            {
                yield return null;
            }

            // All the enemy has been spawn so start a new wave
            yield return new WaitForSeconds(waveTime);
        }

        // If all the wave has been load and there is still enemy alive then dont run next line
        while (enemyAliveCount > 0)
        {
            yield return null;
        }

        // When all the wave has been load and no more enemy left so you win
        GameManager.instance.Win();
    }
}
