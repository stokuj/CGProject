using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] private float startHealth;
    public float currentHealth { get; private set; }
    private Animator anim; 
    private bool dead;

    private void Awake()
    {
        currentHealth = startHealth;

        anim = GetComponent<Animator>();
    }

    public void TakeDamage(float _damage)
    {
        currentHealth = Mathf.Clamp(currentHealth - _damage, 0, startHealth);

        if(currentHealth > 0)
        {

            anim.SetTrigger("hurt");        }
            //ifframes
        else
        {
            if(!dead)
            {
                anim.SetTrigger("die");       
                GetComponent<PlayerMovement>().enabled = false;
                dead = true;
            }
        }
    }

    public void AddHealth(float _value)
    {
        currentHealth = Mathf.Clamp(currentHealth + _value, 0, startHealth);
    }
}
