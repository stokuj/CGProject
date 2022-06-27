using UnityEngine;

/// <summary>
/// A class for collectible health objects "hearts" in game
/// </summary>
public class HealthCollectible : MonoBehaviour
{
     /**  Value of health      */
    [SerializeField] private float healthValue;

    /// <summary>
    /// Method for triggering adding of health in case of collision
    /// </summary>
    /// <param name="collision">Collider2D object representing collision</param>
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            collision.GetComponent<Health>().AddHealth(healthValue);
            gameObject.SetActive(false);
        }
    }
}