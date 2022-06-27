using UnityEngine;

/// <summary>
/// A enemy damage class 
/// </summary>
public class EnemyDamage : MonoBehaviour
{
    /** Damage of an enemy        */
    [SerializeField] protected float damage;

    /// <summary>
    /// Method for damaging an object in case of collision
    /// </summary>
    /// <param name="collision">Collider2D object representing collision</param>
    protected void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
            collision.GetComponent<Health>().TakeDamage(damage);
    }
}