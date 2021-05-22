using UnityEngine;
using UnityEngine.UI;

public class EnemyMove : MonoBehaviour
{
    // Speed of the enemy
    [SerializeField] float speed = 10;

    // Total Hp of the enemy
    float totalHp;

    // Current Hp of the enemy
    [SerializeField] float currentHp = 150f;

    // A slider to show the enemy hp
    [SerializeField] Slider hpSlider;

    // Array of the waypoint for enemy to move
    Transform[] positions;


    void Start()
    {
        // Set the total hp as current hp
        totalHp = currentHp;

        // Set the waypoints
        positions = WayPoints.positions;
    }

    // Update is called once per frame
    void Update()
    {
        Move();
    }

    void Move()
    {
        int index = 0;
        // If it hits the last waypoint then we lose
        if (index > positions.Length - 1)
        {
            ReachEnd();
            // When enemy reach the end of the map we dont run the next line
            return;
        }

        // Move the enemy to the next waypoint var speed
        transform.Translate((positions[index].position - transform.position).normalized * Time.deltaTime * speed);

        // If the enemy is closs to the waypoint then we move to the next waypoint
        if (Vector3.Distance(positions[index].position, transform.position) < 0.2f)
        {
            index++;
        }
    }

    void ReachEnd()
    {
        // If the enemy reach the end point then we lose and destroy this enemy
        GameManager.instance.Lose();
        GameObject.Destroy(this.gameObject);
    }

    void OnDestroy()
    {
        // If the enemy is dead then reduce the enemy alive count
        EnemySpawner.enemyAliveCount--;
    }

    public void Damage(float _damage)
    {
        // Enemy takes the damage and shows at the slider
        currentHp -= _damage;
        hpSlider.value = currentHp / totalHp;

        // If there is no more Hp then destroy the enemy
        if (currentHp <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        // If the enemy die then destroy it
        Destroy(this.gameObject);
    }
}
