using UnityEngine;
/// <summary>
/// A class representing melee enemy.
/// </summary>
public class MeleeEnemy : MonoBehaviour
{

    [Header ("Attack Parameters")]
     /**  Cooldown between attacks      */
    [SerializeField] private float attackCooldown;
     /**  Range      */
    [SerializeField] private float range;
     /**  Damage of enemy      */
    [SerializeField] private int damage;


    [Header("Collider Parameters")]
     /**  Distanve of collisions */     
    [SerializeField] private float colliderDistance;
     /**  BoxCollider2D Obj     */
    [SerializeField] private BoxCollider2D boxCollider;


    [Header("Player Layer")]
    [SerializeField] private LayerMask playerLayer;
     /**  Cooldown timer     */
    private float cooldownTimer = Mathf.Infinity;

     /**  Animator Obj     */
    private Animator anim;
     /**  Health Obj     */
    private Health playerHealth;
     /**  EnemyPatrol Obj      */
    private EnemyPatrol enemyPatrol;
    /// <summary>
    /// Method initialization of enemy patrol and animator
    /// </summary>
    private void Awake()
    {
        anim = GetComponent<Animator>();
        enemyPatrol = GetComponentInParent<EnemyPatrol>();
    }
    /// <summary>
    /// Method for updating behaveour of enemy, if he see player, they dalay of damage and attack
    /// </summary>
    private void Update()
    {
        cooldownTimer += Time.deltaTime;

        //Attack only when player in sight?
        if (PlayerInSight())
        {
            if (cooldownTimer >= attackCooldown)
            {
                cooldownTimer = 0;
                anim.SetTrigger("meleeAttack");
            }
        }

        if (enemyPatrol != null)
            enemyPatrol.enabled = !PlayerInSight();
    }
    /// <summary>
    /// Method difines when enemy see player
    /// </summary>
    private bool PlayerInSight()
    {
        RaycastHit2D hit = 
            Physics2D.BoxCast(boxCollider.bounds.center + transform.right * range * transform.localScale.x * colliderDistance,
            new Vector3(boxCollider.bounds.size.x * range, boxCollider.bounds.size.y, boxCollider.bounds.size.z),
            0, Vector2.left, 0, playerLayer);

        if (hit.collider != null)
            playerHealth = hit.transform.GetComponent<Health>();

        return hit.collider != null;
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(boxCollider.bounds.center + transform.right * range * transform.localScale.x * colliderDistance,
            new Vector3(boxCollider.bounds.size.x * range, boxCollider.bounds.size.y, boxCollider.bounds.size.z));
    }
    /// <summary>
    /// Method for damaging player by enemy in situation that player is in range of enemy
    /// </summary>
    private void DamagePlayer()
    {
        if (PlayerInSight())
            playerHealth.TakeDamage(damage);
    }
}