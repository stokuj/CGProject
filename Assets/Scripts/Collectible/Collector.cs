using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// A class representing collecting an object
/// </summary>
public class Collector : MonoBehaviour
{
    [SerializeField] private AudioClip coinSound;
    /// <summary>
    /// Method for collecing if object is collectible
    /// </summary>
    /// <param name="collision">Collider2D object representing collision</param>
    private void OnTriggerEnter2D(Collider2D collision)
    {

        iCollectible collectible = collision.GetComponent<iCollectible>();
        if(collectible != null)
        {
            SoundManager.instance.PlaySound(coinSound);
            collectible.Collect();
        }
    }
}
