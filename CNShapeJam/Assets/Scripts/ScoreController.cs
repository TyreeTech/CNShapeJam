using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreController : MonoBehaviour
{
    // Objects text and player are initialized
    public Text text;
    private GameObject player;
    private PlayerControls playerController;
    
    void Awake()
    {
        // The player object is set to the GameObject named "Player"
        player = GameObject.Find("Player");
        playerController = player.GetComponent<PlayerControls>();
    }
    void Update()
    {
        // The text object is updated to display the score variable in the PlayerControls script
        text.text = "Score: " + playerController.score;
    }
}
