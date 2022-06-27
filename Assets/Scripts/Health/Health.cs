using UnityEngine;
using System.Collections;

/// <summary>
/// A class representing health system
/// </summary>
public class Health : MonoBehaviour
{
    [Header ("Health")]
    /**  Starting health      */
    [SerializeField] private float startingHealth;
    /**  Current health      */
    public float currentHealth { get; private set; }
    /**  Animator Obj      */
    private Animator anim;
    /**  Is object dead      */
    private bool dead;

    [Header("iFrames")]
    /**  Frames duration      */
    [SerializeField] private float iFramesDuration;
    /**  Number of flashes after being damaged      */
    [SerializeField] private int numberOfFlashes;
    /**  SpriteRenderer      */
    private SpriteRenderer spriteRend;

    [Header("Components")]
    /**  List of objects componets to be disabled      */
    [SerializeField] private Behaviour[] components;
    /**  Is object invulnerable      */
    private bool invulnerable;

    [Header("Death Sound")]
    /**  Sound of death      */
    [SerializeField] private AudioClip deathSound;
    /**  Sound of being hurt      */
    [SerializeField] private AudioClip hurtSound;
    /// <summary>
    /// Method with refrence to health, animator and sprit renderer
    /// </summary>
    private void Awake()
    {
        currentHealth = startingHealth;
        anim = GetComponent<Animator>();
        spriteRend = GetComponent<SpriteRenderer>();
    }
    /// <summary>
    /// Method represents taking damage 
    /// </summary>
    public void TakeDamage(float _damage)
    {
        if (invulnerable) return;
        currentHealth = Mathf.Clamp(currentHealth - _damage, 0, startingHealth);

        if (currentHealth > 0)
        {
            anim.SetTrigger("hurt");
            StartCoroutine(Invunerability());
            SoundManager.instance.PlaySound(hurtSound);
        }
        else
        {
            if (!dead)
            {
                anim.SetTrigger("die");

                //Deactivate all attached component classes


                if(GetComponent<PlayerMovement>() != null)
                    GetComponent<PlayerMovement>().enabled = false;

                if(GetComponentInParent<EnemyPatrol>() != null)
                    GetComponentInParent<EnemyPatrol>().enabled = false;

                if(GetComponent<MeleeEnemy>() != null)
                    GetComponent<MeleeEnemy>().enabled = false;


                anim.SetBool("grounded", true);
                anim.SetTrigger("die");

                dead = true;
                SoundManager.instance.PlaySound(deathSound);
            }
        }
    }
    /// <summary>
    /// Method for adding health ( in case of collecting hearths)
    /// </summary>
    public void AddHealth(float _value)
    {
        currentHealth = Mathf.Clamp(currentHealth + _value, 0, startingHealth);
    }
    /// <summary>
    /// Method for respawning
    /// </summary>
    public void Respawn()
    {
        dead = false;
        AddHealth(startingHealth);
        anim.ResetTrigger("die");
        anim.Play("Idle");
        StartCoroutine(Invunerability());

        if (GetComponent<PlayerMovement>() != null)
            GetComponent<PlayerMovement>().enabled = true;

        if (GetComponentInParent<EnemyPatrol>() != null)
            GetComponentInParent<EnemyPatrol>().enabled = true;

        if (GetComponent<MeleeEnemy>() != null)
            GetComponent<MeleeEnemy>().enabled = true;
    }

    /// <summary>
    /// Method for being invulnerable for given time
    /// </summary>
    private IEnumerator Invunerability()
    {
        invulnerable = true;
        Physics2D.IgnoreLayerCollision(10, 11, true);
        for (int i = 0; i < numberOfFlashes; i++)
        {
            spriteRend.color = new Color(1, 0, 0, 0.5f);
            yield return new WaitForSeconds(iFramesDuration / (numberOfFlashes * 2));
            spriteRend.color = Color.white;
            yield return new WaitForSeconds(iFramesDuration / (numberOfFlashes * 2));
        }
        Physics2D.IgnoreLayerCollision(10, 11, false);
        invulnerable = false;
    }
    /// <summary>
    /// Method for deactivating system
    /// </summary>
    private void Deactivate()
    {
        gameObject.SetActive(false);
    }
}