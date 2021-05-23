using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour
{
    // Attack one time every sec
    [SerializeField] float attackTime = 1;
    
    // To time the tower's attack time
    float timer = 0;

    // Prefab of the bullet
    [SerializeField] GameObject bulletPrefab;

    // Where the tower fire
    [SerializeField] Transform firePosition;

    // The head of the tower
    [SerializeField] Transform towerHead;

    // Put the enemys in the list that is in the attack area
    List<GameObject> inRangeEnemys = new List<GameObject>();

    void OnTriggerEnter(Collider _collider)
    {
        // If the enemys go in the range then put in the list
        if (_collider.tag == "Enemy")
        {
            inRangeEnemys.Add(_collider.gameObject);
        }
    }

    void OnTriggerExit(Collider _collider)
    {
        // If the enemys go out the range then remove it from the list
        if (_collider.tag == "Enemy")
        {
            inRangeEnemys.Remove(_collider.gameObject);
        }
    }

    void Start()
    {
        // Let the tower hit the target at the frist time
        timer = attackTime;
    }

    void Update()
    {
        // If there is enemy in the attack range
        if (inRangeEnemys.Count > 0 && inRangeEnemys[0] != null)
        {
            // Get the enemy position
            Vector3 targetPosition = inRangeEnemys[0].transform.position;

            // Move the head of the tower to look at the enemy
            targetPosition.y = towerHead.position.y;
            towerHead.LookAt(targetPosition);
        }       
        
        // Then we start timing the tower and attack the enemy every 1s
        timer += Time.deltaTime;
        if (inRangeEnemys.Count > 0 && timer >= attackTime)
        {
            timer = 0;
            Attack();
        }      
        // If the first enemy move away and renew the list
        else if (inRangeEnemys.Count > 0 && inRangeEnemys[0] == null)
        {
            UpdateEnemys();
        }
    }

    void Attack()
    {
        // If there is no more enemys or the list of the enemy didnt update then update the list of in range enemy
        if (inRangeEnemys[0] == null)
        {
            UpdateEnemys();
        }

        // If there is enemy in range then shoot the frist enemy
        if (inRangeEnemys.Count > 0)
        {
            GameObject bullet = GameObject.Instantiate(bulletPrefab, firePosition.position, firePosition.rotation);
            bullet.GetComponent<Bullet>().SetTarget(inRangeEnemys[0].transform);
        }
        else
        {
            // Set the CD back
            timer = attackTime;
        }
    }

    void UpdateEnemys()
    {
        // Remove all the null object thats in the attack range list
        List<int> emptyIndex = new List<int>();
        int i;

        // Loop at the null object thats in the list and put it in the index list
        for (i = 0; i < inRangeEnemys.Count; i++)
        {
            if (inRangeEnemys[i] == null)
            {
                emptyIndex.Add(i);
            }
        }

        // Remove it from the attack range list
        for (i = 0; i < emptyIndex.Count; i++)
        {
            inRangeEnemys.RemoveAt(emptyIndex[i] - i);
        }
    }
}
