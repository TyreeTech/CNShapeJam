using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StarGenerator : MonoBehaviour
{
    // The starfield generator uses the following variables
    // whiteGlow dictates the type of material used in the created stars, and the stars use the following four vars
    // The size, the rate at which they're created, the time until the next star is made, the speed at which it "falls"
    public Material whiteGlow;
    public float starSize;
    public float starRate;
    private float nextStar;
    public float starSpeed;

    
    void Update() 
    {
        // Based on the rate dictated by nextStar, a randomly sized star is created as a cube above the scene
        // it's given a rigidbody, assigned the whiteGlow material, does NOT use gravity, but uses velocity
        if (Time.time > nextStar)
        {
            starSize = Random.Range(0.01f, 0.05f);
            nextStar = Time.time + starRate;
            GameObject star = GameObject.CreatePrimitive(PrimitiveType.Cube);
            star.transform.parent = gameObject.transform;
            star.transform.position = new Vector3(Random.Range(-6.0f,6.0f),8,5);
            star.transform.localScale = new Vector3(starSize, starSize, starSize);
            star.AddComponent<Rigidbody>();
            star.GetComponent<Renderer>().material = whiteGlow;
            star.GetComponent<Rigidbody>().useGravity = false;
            star.GetComponent<Rigidbody>().velocity = new Vector3(0, -starSize * starSpeed, 0);
        }
    }
}