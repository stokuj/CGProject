using UnityEngine;

/// <summary>
/// A class representing enemy patrol behavior
/// </summary>
public class EnemyPatrol : MonoBehaviour
{
    [Header ("Patrol Points")]
    /**  Ledge object       */
    [SerializeField] private Transform leftEdge;

    /**  Redge object       */
    [SerializeField] private Transform rightEdge;


    [Header("Enemy")]
    /**  Enemy object       */
    [SerializeField] private Transform enemy;

    [Header("Movement parameters")]
    /**  Speed of movemnt       */
    [SerializeField] private float speed;
    /**  Scale       */
    private Vector3 initScale;
    /**  Do i move left?       */
    private bool movingLeft;


    [Header("Idle Behaviour")]
    /** Idle duration        */
    [SerializeField] private float idleDuration;
    /** Time of idle       */
    private float idleTimer;
    /**         */

    [Header("Enemy Animator")]
    /** Animator object      */
    [SerializeField] private Animator anim;

    private void Awake()
    {
        initScale = enemy.localScale;
    }
    private void OnDisable()
    {
        anim.SetBool("moving", false);
    }
    /// <summary>
    /// Method for updating direction
    /// </summary>
    private void Update()
    {
        if (movingLeft)
        {
            if (enemy.position.x >= leftEdge.position.x)
                MoveInDirection(-1);
            else
                DirectionChange();
        }
        else
        {
            if (enemy.position.x <= rightEdge.position.x)
                MoveInDirection(1);
            else
                DirectionChange();
        }
    }
    /// <summary>
    /// Method for changing direction 
    /// </summary>
    private void DirectionChange()
    {
        anim.SetBool("moving", false);
        idleTimer += Time.deltaTime;

        if(idleTimer > idleDuration)
            movingLeft = !movingLeft;
    }
    /// <summary>
    /// Method for checking in with direction to move
    /// </summary>
    /// <param name="_direction">Given direction to move</param>
    private void MoveInDirection(int _direction)
    {
        idleTimer = 0;
        anim.SetBool("moving", true);

        //Make enemy face direction
        enemy.localScale = new Vector3(Mathf.Abs(initScale.x) * _direction,
            initScale.y, initScale.z);

        //Move in that direction
        enemy.position = new Vector3(enemy.position.x + Time.deltaTime * _direction * speed,
            enemy.position.y, enemy.position.z);
    }
}