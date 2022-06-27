using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// A class representing player attack
/// </summary>
public class PlayerAttack : MonoBehaviour
{
     /**  Cooldown between attacks      */
    [SerializeField] private float attackCooldown;
     /**  firePoint aiming for firebals      */
    [SerializeField] private Transform firePoint;
     /**  fireballs list - array      */
    [SerializeField] private GameObject[] fireballs;
     /**  Sound of fireball      */
    [SerializeField] private AudioClip fireballSound;
     /**  Animator Obj      */
    private Animator anim;
     /**  PlayerMovement Obj      */
    private PlayerMovement playerMovement;
     /**  CooldownTimer      */
    private float cooldownTimer = Mathf.Infinity;
    /// <summary>
    /// Method for initializing plaer componets and animator
    /// </summary>
    private void Awake()
    {
        anim = GetComponent<Animator>();
        playerMovement = GetComponent<PlayerMovement>();
    }

    /// <summary>
    /// Method for updating if player can attack
    /// </summary>
    private void Update()
    {
        if (Input.GetMouseButton(0) && cooldownTimer > attackCooldown && playerMovement.canAttack())
        {
            Attack();
        }

        cooldownTimer += Time.deltaTime;
    }

    /// <summary>
    /// Method for attacking
    /// </summary>
    private void Attack()
    {
        SoundManager.instance.PlaySound(fireballSound);
        anim.SetTrigger("fireballskill");
        cooldownTimer = 0;
        //pool fireballs
        fireballs[FindFireball()].transform.position = firePoint.position;
        fireballs[FindFireball()].GetComponent<Projectile>().SetDirection(Mathf.Sign(transform.localScale.x));
    }

    /// <summary>
    /// Method for firaball attack
    /// </summary>
    private int FindFireball()
    {
        for (int i = 0; i < fireballs.Length; i++)
        {
            if (!fireballs[i].activeInHierarchy)
                return i;
        }
        return 0;
    }

}
