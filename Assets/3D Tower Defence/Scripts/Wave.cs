using UnityEngine;

// Var for the enemy that spawn in wave
[System.Serializable]
public class Wave
{
    // Enemy's prefab
    public GameObject enemyPrefab;

    // How many we spawn in current wave
    public int count;

    // How long between two enemy spawn
    public float spawnTime;
}
