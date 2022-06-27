using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// A class representing controller of camera
/// </summary>
public class CameraController : MonoBehaviour
{
    /**  Speed of camera       */
    [SerializeField] private float speed;
    /**  Transform Object       */
    [SerializeField] private Transform player;
    /**  Player object       */
    [SerializeField] private float aheadDistance;
    /** How far we should we see ahed        */
    [SerializeField] private float cameraSpeed;
    /**  Speed of chaning ahed       */
    private float currentPosX;
    /**  Velocity vertor      */
    private Vector3 velocity = Vector3.zero;
    private float lookAhead;


    /// <summary>
    /// Method for updating camera position
    /// </summary>
    private void Update()
    {
        transform.position = new Vector3(player.position.x + lookAhead, transform.position.y, transform.position.z);
        lookAhead = Mathf.Lerp(lookAhead, (aheadDistance * player.localScale.x), Time.deltaTime * cameraSpeed);
    }

    public void MoveToNewRoom(Transform _newRoom)
    {
        currentPosX = _newRoom.position.x;
    }

}
