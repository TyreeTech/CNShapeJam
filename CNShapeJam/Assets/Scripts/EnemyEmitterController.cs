using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyEmitterController : MonoBehaviour
{
    // variables declared to contain the Enemy object that will be instantiated
    // the player (needed for the gameover null check and the currentLevel variable)
    // spawn rate is how often enemies will spawn
    // nextSpawn is given an initial value so that the one
    // in the scene doesn't get doubled up with an immediate spawn
    public GameObject Enemy;
    private PlayerControls playerController;
    public float spawnRate;
    private float nextSpawn = 5.0f;

    void Start()
    {
        // player is set to the GameObject in the scene, "Player"
        GameObject player = GameObject.Find("Player");
        playerController = player.GetComponent<PlayerControls>();

    }
    void Update()
    {
        // Null check
        if (playerController.gameOver)
        {
            return;
        }
        // Usual time delay code
        if (Time.time > nextSpawn)
        {
            nextSpawn = Time.time + spawnRate;

            /*****************************\
            |**** Add your code below ****|
            \*****************************/

            for (int i = 0; i < playerController.currentLevel; i++)
            {
              Debug.Log("Loop works!");
              float randomX = Random.Range(-6.0f, 6.0f);
              Vector3 enemyPosition = new Vector3(randomX, 6, 0);
              transform.position = enemyPosition;
              Instantiate(Enemy, transform.position, transform.rotation);
            }

            /*****************************\
            |**** Add your code above ****|
            \*****************************/
        }
    }
}
