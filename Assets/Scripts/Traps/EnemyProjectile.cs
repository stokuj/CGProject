using UnityEngine;

/// <summary>
/// A class representing enemy projectiles
/// </summary>
public class EnemyProjectile : EnemyDamage
{
    /**  Speed      */
    [SerializeField] private float speed;
    /**  Reset time       */
    [SerializeField] private float resetTime;
    /**  Lifetime       */
    private float lifetime;
    /**  Animator obj      */
    private Animator anim;
    /**  BoxCollider obj       */
    private BoxCollider2D coll;
    /**  Bool fo hit       */
    private bool hit;

    /// <summary>
    /// Method making refference to animator and collider
    /// </summary>
    private void Awake()
    {
        anim = GetComponent<Animator>();
        coll = GetComponent<BoxCollider2D>();
    }

    /// <summary>
    /// Method for activating projectile
    /// </summary>
    public void ActivateProjectile()
    {
        hit = false;
        lifetime = 0;
        gameObject.SetActive(true);
        coll.enabled = true;
    }
    /// <summary>
    /// Method for updating projectile position
    /// </summary>
    private void Update()
    {
        if (hit) return;
        float movementSpeed = speed * Time.deltaTime;
        transform.Translate(movementSpeed, 0, 0);

        lifetime += Time.deltaTime;
        if (lifetime > resetTime)
            gameObject.SetActive(false);
    }
    /// <summary>
    /// Method for triggering explode animation in case of collision
    /// </summary>
    /// <param name="collision">Collider2D object representing collision</param>
    private void OnTriggerEnter2D(Collider2D collision)
    {
        hit = true;
        base.OnTriggerEnter2D(collision); //Execute logic from parent script first
        coll.enabled = false;

        if (anim != null)
            anim.SetTrigger("explode"); //When the object is a fireball explode it
        else
            gameObject.SetActive(false); //When this hits any object deactivate arrow
    }
    /// <summary>
    /// Method for deactivating projectile
    /// </summary>
    private void Deactivate()
    {
        gameObject.SetActive(false);
    }
}