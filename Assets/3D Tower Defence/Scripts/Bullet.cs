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
        target = _target;
    }

    void Update()
    {
        if (target == null)
        {
            DestroyThisBullet();
            return;
        }

        transform.LookAt(target.position);
        transform.Translate(Vector3.forward * speed * Time.deltaTime);

        Vector3 dir = target.position - transform.position;
        if (dir.magnitude < distanceToTarget)
        {
            target.GetComponent<EnemyMove>().Damage(damage);
            DestroyThisBullet();
        }
    }

    void DestroyThisBullet()
    {
        Destroy(this.gameObject);
    }
}
