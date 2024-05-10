using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HPController : MonoBehaviour
{
    // Objects text and player are initialized
    public Text text;
    public GameObject player;
    private PlayerControls playerController;

    void Start()
    {
        // The player object is set to the GameObject named "Player"
        player = GameObject.Find("Player");
        playerController = player.GetComponent<PlayerControls>();
    }
    void Update()
    {
        // Null check, if gameOver = true, text displays below message
        // Otherwise, the HP text object will display the player's HP value
        if (playerController.gameOver)
        {
            text.text = "GAME OVER! RESTART THE GAME TO TRY AGAIN";
        }
        else
        {
            text.text = "HP: " + playerController.playerHealth;
        }
    }
}
