using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerupBehavior : MonoBehaviour
{
    // two variables initialized - enemies contained an array of all enemies, and the player
    public GameObject[] enemies;
    private PlayerControls playerController;

    [SerializeField] private AudioSource powerupSFX;

    void Start()
    {
        //on script start, the player variable is assigned the GameObject named "Player"
        GameObject player = GameObject.Find("Player");
        playerController = player.GetComponent<PlayerControls>();
        //powerupSFX = GetComponent<AudioSource>();
    }
    void Update()
    {
        // Null check for gameOver state
        // after being created in the PowerupController, the Powerup will simply move downwards
        if (playerController.gameOver)
        {
            return;
        }
        else
        {
            transform.position = transform.position - new Vector3(0, 0.03f, 0);
        }
    }
    void OnTriggerEnter(Collider other)
    {
        // if a gameobject with the tag "Player" touches this object -
        // the gameobject is destroyed, the level goes up by 1, and all enemies get 5 more HP
        if (other.gameObject.tag == "Player")
        {

            //PowerupAudioSource.clip = collectSFX;
            //powerupSFX.Play();
            Debug.Log("Level up!");
            Destroy(gameObject);
            other.GetComponent<PlayerControls>().currentLevel++;
            enemies = GameObject.FindGameObjectsWithTag("Enemy");

            foreach (GameObject enemy in enemies)
            {
                enemy.GetComponent<EnemyController>().enemyHealth += 5;
            }
        }
    }
}
