using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileScript : MonoBehaviour
{
    // player is used to store the "Player" object, which contains the projectileForce needed for this script
    // TODO - just put the projectileForce variable here unless player needs it?
    private GameObject player;
    private PlayerControls playerController;

    void Start()
    {
        // Sets GameObject "Player" to variable player
        player = GameObject.Find("Player");
        playerController = player.GetComponent<PlayerControls>();
    }
    void Update()
    {
        // all projectiles contain this code to move them upwards at projectileForce
        transform.position = transform.position + new Vector3(0, playerController.projectileForce, 0);
    }
}
