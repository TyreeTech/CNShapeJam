using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSpaceController : MonoBehaviour
{
    //whenever any object with a collider/rigidbody exits the GameSpace object (3D, surrounds scene) it is destroyed
    void OnTriggerExit(Collider other)
    {
        Destroy(other.gameObject);
    }
}
