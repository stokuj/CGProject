using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// A class representing player respawn
/// </summary>
public class PlayerRespawn : MonoBehaviour
{
     /**  Sound of checkpoint      */
    [SerializeField] private AudioClip checkpointSound;
     /**  Checkpoint Obj      */
    private Transform currentCheckpoint;
     /**  Health of player      */
    private Health playerHealth;
    
    /// <summary>
    /// Method for  initializion of health system
    /// </summary>
    private void Awake()
        playerHealth = GetComponent<Health>();
    }


    /// <summary>
    /// Method for  respawning
    /// </summary>
    public void Respawn()
    {
        transform.position = currentCheckpoint.position;
        playerHealth.Respawn();
        Camera.main.GetComponent<CameraController>().MoveToNewRoom(currentCheckpoint.parent);
    }

    /// <summary>
    /// Method for  tringgering checkpoint animation and sound
    /// </summary>
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.transform.tag == "Checkpoint")
        {
            currentCheckpoint = collision.transform;
            SoundManager.instance.PlaySound(checkpointSound);
            collision.GetComponent<Collider2D>().enabled = false;
            collision.GetComponent<Animator>().SetTrigger("Appear");
        }
    }
}
