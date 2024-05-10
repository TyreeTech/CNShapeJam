using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextController : MonoBehaviour
{
    // this class uses two objects, the level text object and the player itself
    public Text text;
    private GameObject player;
    private PlayerControls playerController;

    void Awake()
    {
        // the player gameobject is set to the object in the hierarchy using the find function
        player = GameObject.Find("Player");
        playerController = player.GetComponent<PlayerControls>();
    }
    void Update()
    {
        // the text object is set to display the current level that the player is on
        text.text = "Level: " + playerController.currentLevel;
    }
}
