using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerupController : MonoBehaviour
{
    // variables for nextDrop
    // (initial float means first powerup spawns 15 seconds in)
    // dropRate is how often powerups are made
    // powerup holds the powerup
    private float nextDrop = 15.0f;
    public float dropRate;
    public GameObject powerup;
    public GameObject powerupEmitter;
    private GameObject player;
    private PlayerControls playerController;

    void Start()
    {
        // powerupEmitter and player are assigned
        // their corresponding objects in the scene
        powerupEmitter = GameObject.Find("PowerupEmitter");
        player = GameObject.Find("Player");
        playerController = player.GetComponent<PlayerControls>();
    }

    void Update()
    {
        // Null check for gameOver state
        if (playerController.gameOver)
        {
            return;
        }

        // At the interval decided by dropRate,
        //    powerupPoosition is set to a random x from -6 to 6
        // above the game the powerupEmitter itself is moved to that position,
        //   and the nextDrop is set
        // a powerup is instantiated at the emitter's position and rotation

        if (Time.time > nextDrop)
        {
            float randomX = Random.Range(-6.0f, 6.0f);
            Vector3 powerupPosition = new Vector3(randomX, 6.0f, 0.0f);
            transform.position = powerupPosition;
            nextDrop = Time.time + dropRate;

            /*****************************\
            |**** Add your code below ****|
            \*****************************/

              Instantiate(powerup, transform.position, transform.rotation);

            /*****************************\
            |**** Add your code above ****|
            \*****************************/
        }
    }
}
