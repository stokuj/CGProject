using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// A class representing healthbar
/// </summary>
public class Healthbar : MonoBehaviour
{

    /**  Health Obj      */
    [SerializeField] private Health playerHealth;
    /**  Total health bar      */
    [SerializeField] private Image totalhealthBar;
    /**  Current health      */
    [SerializeField] private Image currenthealthBar;

    /// <summary>
    /// Method initalizing healthbar at beginning of game
    /// </summary>
    private void Start()
    {
        totalhealthBar.fillAmount = playerHealth.currentHealth / 10;
    }
    /// <summary>
    /// Method for updating healthbar in gametime
    /// </summary>
    private void Update()
    {
        currenthealthBar.fillAmount = playerHealth.currentHealth/10;
    }
}
