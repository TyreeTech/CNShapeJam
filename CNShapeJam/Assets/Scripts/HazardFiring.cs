using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// the HazardFiring class dictates how enemy projectiles (hazards) are spawned
public class HazardFiring : MonoBehaviour
{
    // GameObjects hazard and player are used, 
    // the hazard also needs a target position to calculate its movedirection
    // The Transform hazardSpawn dictates the starting position
    // fireRate, nextFire, and speed decide the rate and...speed at which hazards act
    public GameObject hazard;
    public GameObject player;
    public Vector3 target;
    public Vector3 HazardMoveDirection;
    public Transform hazardSpawn;
    public float fireRate;
    private float nextFire;
    public float speed;
    private PlayerControls playerController;

    void Start()
    {
        // GameObject named Player is assigned to variable player
        player = GameObject.Find("Player");
        playerController = player.GetComponent<PlayerControls>();
    }
    void Update()
    {
        // Null check for GameOver
        if (playerController.gameOver)
        {
            return;
        }

        // Target is set to the the Player's position, 
        // and a vector is made from the current position to the target
        
        target = player.transform.position;

        HazardMoveDirection = target - transform.position;

        if (Time.time > nextFire)
        {
            nextFire = Time.time + fireRate;

            /*****************************\
            |**** Add your code below ****|
            \*****************************/


            /*****************************\
            |**** Add your code above ****|
            \*****************************/
        }
    }
}
