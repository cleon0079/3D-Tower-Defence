using UnityEngine;

public class Bullet : MonoBehaviour
{
    // Damage of the tower
    [SerializeField] int damage = 50;

    // Speed of the bullet
    [SerializeField] float speed = 40;

    // Target of the tower
    Transform target;

    // Distance between bullet and the enemy
    float distanceToTarget = 1f;

    public void SetTarget(Transform _target)
    {
        // Set the target
        target = _target;
    }

    void Update()
    {
        // If there is no target then destroy this bullet and dont run the next line
        if (target == null)
        {
            DestroyThisBullet();
            return;
        }

        // If there is a target then look at the target and move towards the target
        transform.LookAt(target.position);
        transform.Translate(Vector3.forward * speed * Time.deltaTime);

        // If the bullet hits the target then destroy the bullet and do the damage to the enemy
        Vector3 dir = target.position - transform.position;
        if (dir.magnitude < distanceToTarget)
        {
            target.GetComponent<EnemyMove>().Damage(damage);
            DestroyThisBullet();
        }
    }

    void DestroyThisBullet()
    {
        // destroy the bullet
        Destroy(this);
    }
}
