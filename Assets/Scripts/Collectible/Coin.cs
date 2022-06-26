using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using TMPro;

public class Coin : MonoBehaviour, iCollectible
{
    public static event Action OnCoinCollected;

    public void Collect()
    {
        Destroy(gameObject);
        OnCoinCollected?.Invoke();
    }
}