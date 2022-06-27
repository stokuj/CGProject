using UnityEngine;

/// <summary>
/// A class for projectiles
/// </summary>
public class Projectile : MonoBehaviour
{
     /**  Speed projectile      */
    [SerializeField] private float speed;
     /**  Projection direction      */
    private float direction;
     /**  Hit      */
    private bool hit;
     /**  Lifetime      */
    private float lifetime;

     /**  Animator obj      */
    private Animator anim;
     /**  BoxCollider Obj      */
    private BoxCollider2D boxCollider;


    /// <summary>
    /// Method for  initializion collider and animator
    /// </summary>
    private void Awake()
    {
        anim = GetComponent<Animator>();
        boxCollider = GetComponent<BoxCollider2D>();
    }
    /// <summary>
    /// Method for  updating postion of projectile
    /// </summary>
    private void Update()
    {
        if (hit) return;
        float movementSpeed = speed * Time.deltaTime * direction;
        transform.Translate(movementSpeed, 0, 0);

        lifetime += Time.deltaTime;
        if (lifetime > 5) gameObject.SetActive(false);
    }

    /// <summary>
    /// Method for  tringgering damage in case of collision with projectile
    /// </summary>
    /// <param name="collision">Collider2D object representing collision</param>
    private void OnTriggerEnter2D(Collider2D collision)
    {
        anim.SetTrigger("explode");

        hit = true;
        boxCollider.enabled = false;

        if (collision.tag == "Enemy")
            collision.GetComponent<Health>().TakeDamage(1); 
    }
    /// <summary>
    /// Method for seting direction of projectile
    /// </summary>
    /// <param name="_direction">Given direction</param>
    public void SetDirection(float _direction)
    {
        lifetime = 0;
        direction = _direction;
        gameObject.SetActive(true);
        hit = false;
        boxCollider.enabled = true;

        float localScaleX = transform.localScale.x;
        if (Mathf.Sign(localScaleX) != _direction)
            localScaleX = -localScaleX;

        transform.localScale = new Vector3(localScaleX, transform.localScale.y, transform.localScale.z);
    }
    /// <summary>
    /// Method for  deactivating
    /// </summary>
    private void Deactivate()   
    {
        gameObject.SetActive(false);
    }
}