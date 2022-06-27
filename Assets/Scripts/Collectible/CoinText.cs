using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

/// <summary>
/// A class for representing ammount of collected coins
/// </summary>
public class CoinText : MonoBehaviour
{   
    /**  gui of coin object  */
    public TextMeshProUGUI coinText;
    /** Ammount of coins     */
    int coinCount;

    /// <summary>
    /// Method for incrementing number of coins
    /// </summary>
    private void OnEnable()
    {
        Coin.OnCoinCollected += IncrementCoinCount;
    }

    /// <summary>
    /// Method decreasing number of coins
    /// </summary>
    private void OnDisable()
    {
        Coin.OnCoinCollected -= IncrementCoinCount;
    }

    /// <summary>
    /// Method for chaning number of coins
    /// </summary>
    public void IncrementCoinCount()
    {
        coinCount++;
        coinText.text = $"{coinCount}";
    }
}
