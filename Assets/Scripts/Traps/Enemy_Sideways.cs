using UnityEngine;

/// <summary>
/// A Enemy Sideways class 
/// </summary>
public class Enemy_Sideways : MonoBehaviour
{
    /**   Distance of movemnt      */
    [SerializeField] private float movementDistance;
    /** Distance        */
    [SerializeField] private float speed;
    /** Speed       */
    [SerializeField] private float damage;
    /** Damage     */
    private bool movingLeft;
    /** Left edge distance       */
    private float leftEdge;
    /** Right edge distance       */
    private float rightEdge;

    /// <summary>
    /// Method for defining corenrs of movement
    /// </summary>
    private void Awake()
    {
        leftEdge = transform.position.x - movementDistance;
        rightEdge = transform.position.x + movementDistance;
    }

    /// <summary>
    /// Method for updating position of trap
    /// </summary>
    private void Update()
    {
        if (movingLeft)
        {
            if (transform.position.x > leftEdge)
            {
                transform.position = new Vector3(transform.position.x - speed * Time.deltaTime, transform.position.y, transform.position.z);
            }
            else
                movingLeft = false;
        }
        else
        {
            if (transform.position.x < rightEdge)
            {
                transform.position = new Vector3(transform.position.x + speed * Time.deltaTime, transform.position.y, transform.position.z);
            }
            else
                movingLeft = true;
        }
    }

    /// <summary>
    /// Method for triggering damage of player in case of collision
    /// </summary>
    /// <param name="collision">Collider2D object representing collision</param>
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            collision.GetComponent<Health>().TakeDamage(damage);
        }
    }
}