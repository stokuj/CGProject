using UnityEngine;
using System.Collections;

/// <summary>
/// A class for FireTraps traps
/// </summary>
public class Firetrap : MonoBehaviour
{   
    /**  Damage      */
    [SerializeField] private float damage;

    [Header("Firetrap Timers")]
    /**  Delay activation       */
    [SerializeField] private float activationDelay;
    /** Time of being active       */
    [SerializeField] private float activeTime;
    /**  Animator Obj       */
    private Animator anim;
    /**  SpriteRenderer Obj       */
    private SpriteRenderer spriteRend;
    

    /** Wwhen the trap gets triggered      */
    private bool triggered; 
    /**  When the trap is active and can hurt the player       */
    private bool active; 
    /// <summary>
    /// Method makes reffrence to animator and sprite renderer
    /// </summary>
    private void Awake()
    {
        anim = GetComponent<Animator>();
        spriteRend = GetComponent<SpriteRenderer>();
    }

    /// <summary>
    /// Method for triggering firetrap in case of collision
    /// </summary>
    /// <param name="collision">Collider2D object representing collision</param>
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            if (!triggered)
                StartCoroutine(ActivateFiretrap());

            if (active)
                collision.GetComponent<Health>().TakeDamage(damage);
        }
    }
    /// <summary>
    /// Method for activating firetrap with given color and delay
    /// </summary>
    private IEnumerator ActivateFiretrap()
    {
        //turn the sprite red to notify the player and trigger the trap
        triggered = true;
        spriteRend.color = Color.red; 

        //Wait for delay, activate trap, turn on animation, return color back to normal
        yield return new WaitForSeconds(activationDelay);
        spriteRend.color = Color.white; //turn the sprite back to its initial color
        active = true;
        anim.SetBool("activated", true);

        //Wait until X seconds, deactivate trap and reset all variables and animator
        yield return new WaitForSeconds(activeTime);
        active = false;
        triggered = false;
        anim.SetBool("activated", false);
    }
}