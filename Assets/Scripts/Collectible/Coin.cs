using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using TMPro;
/// <summary>
/// A class representing coin object
/// </summary>
public class Coin : MonoBehaviour, iCollectible
{
    /**  Event in case of collision with coin    */
    public static event Action OnCoinCollected;

    /// <summary>
    /// Method for collecting the coins
    /// </summary>
    public void Collect()
    {
        Destroy(gameObject);
        OnCoinCollected?.Invoke();
    }
}