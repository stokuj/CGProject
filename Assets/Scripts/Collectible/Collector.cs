using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collector : MonoBehaviour
{
    [SerializeField] private AudioClip coinSound;
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
