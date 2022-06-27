using UnityEngine;

/// <summary>
/// A class for holding fireballs objects
/// </summary>
public class EnemyFireballHolder : MonoBehaviour
{
    /** Enemy object        */
    [SerializeField] private Transform enemy;

    /// <summary>
    /// Method for updating fireballs
    /// </summary>
    private void Update()
    {
        transform.localScale = enemy.localScale;
    }
}