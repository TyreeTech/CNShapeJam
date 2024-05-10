using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    // Lots of stuff here! MovePatterns holds a list of all Waypoints to control enemy movement behavior
    // enemySpeed and Health are self-explanatory
    // The currentWaypoint is set to 0 to start, but initialized to a random value from 0-4 on start
    // pointValue dictates how much score each enemy is worth
    // Patrol is a bool used to turn movement on or off, unused in this iteration of the game, kept for future uses
    // particleSize, particlesPerRow, explosionRadius, explosionForce, explosionUpward are properties on enemy destruction
    // enemyMaterial controls the material applied to the enemy, target is the target waypoint in movement
    // EnemeyMoveDirection and EnemyVelocity are movement properties after a target has been decided
    private List<Transform> MovePatterns = new List<Transform>();
    public float enemySpeed;
    public int enemyHealth;    
    public int currentWaypoint;
    public int pointValue;
    public bool Patrol = true;
    public float particleSize = 0.1f;
    public int particlesPerRow = 5;
    public float explosionRadius;
    public float explosionForce;
    public float explosionUpward;
    public Material enemyMaterial;
    public Vector3 Target;
    public Vector3 EnemyMoveDirection;
    public Vector3 EnemyVelocity;
    private PlayerControls playerController;
    private Rigidbody myRigidBody;

    void Start() 
    {
        // hardcoded list, enemyhealth and the first waypoint are also set here
        MovePatterns.Add(GameObject.Find("Waypoint_1").transform);
        MovePatterns.Add(GameObject.Find("Waypoint_2").transform);
        MovePatterns.Add(GameObject.Find("Waypoint_3").transform);
        MovePatterns.Add(GameObject.Find("Waypoint_4").transform);
        MovePatterns.Add(GameObject.Find("Waypoint_5").transform);

        enemyHealth = 10;

        currentWaypoint = Random.Range(0, MovePatterns.Count);

        playerController = GameObject.Find("Player").GetComponent<PlayerControls>();
        myRigidBody = GetComponent<Rigidbody>();
    }

    void Update()
    {
        // Null check!
        if (playerController.gameOver)
        {
            return;
        }
        else
        {
            // currentWaypoint used to be ++ and move in set patterns, keeping for future-proofing
            // a Target position is set to a waypoint position, a movement vector is calculated
            // the rigidbody velocity is stored in a variable EnemyVelocity
            // if the stored movement vector has a magnitude is less than 1 (close to waypoint), a new waypoint is set
            // this new waypoint CAN be the same waypoint, but the next frame a new waypoint is selected again
            // TODO this is a potential bug waiting to happen but who needs a second array to fix it
            // anyway, if the magnitude is greater than (or equal to) 1 then the EnemyVelocity is set to a normalized value
            // but multiplied by enemySpeed to allow more user control
            if (currentWaypoint < MovePatterns.Count)
            {
                Target = MovePatterns[currentWaypoint].position;
                EnemyMoveDirection = Target - transform.position;
                EnemyVelocity = myRigidBody.velocity;
                if (EnemyMoveDirection.magnitude < 1)
                {
                    currentWaypoint = Random.Range(0, MovePatterns.Count);
                }
                else
                {
                    EnemyVelocity = EnemyMoveDirection.normalized * enemySpeed;
                }
            }
            // if the currentWaypoint is greater than the range (it can't be) then it's set to 0
            // if patrol is false, EnemyVelocity is set to 0
            else
            {
                if (Patrol)
                {
                    currentWaypoint = 0;
                }
                else
                {
                    EnemyVelocity = Vector3.zero;
                }
            }
            // after all that, the rigidbody is finally set to the EnemyVelocity, and there's a fun transform.Rotate to
            // make the cubes spin
            myRigidBody.velocity = EnemyVelocity;
            {
                transform.Rotate(new Vector3 (75, 150, 225) * Time.deltaTime);
            }
            // If an enemy is defeated, it is destroyed, the player is rewarded with the pointValue of the enemy
            // a cube-grid is built using the particles per row variable and the CreatePiece function (built later)
            // after the cube-grid is built, the physics for blowing it up are set!
            if (enemyHealth <= 0) {
                Destroy(gameObject);
                pointValue = 500 * playerController.currentLevel;
                playerController.score += pointValue;

                for (int x = 0; x < particlesPerRow; x++) {
                    for (int y = 0; y < particlesPerRow; y++) {
                        for (int z = 0; z < particlesPerRow; z++) {
                            CreatePiece(x, y, z);
                        }
                    }
                }

                Vector3 explosionPos = transform.position;
                Collider[] colliders = Physics.OverlapSphere(explosionPos, explosionRadius);
                foreach (Collider hit in colliders)
                {
                    Rigidbody rb = hit.GetComponent<Rigidbody>();
                    if (rb != null)
                    {
                        rb.AddExplosionForce(explosionForce, transform.position, explosionRadius, explosionUpward);
                    }
                }
            }
        }
    }

    void CreatePiece(int x, int y, int z)
    {
        // When the CreatePiece function is called, a GameObject piece is made and set to a created Cube
        // the cube is positioned and sized based on the xyz inputs from the function (check nested for loop in update)
        // Each cube has its own Rigidbody with mass and a set material (match enemy material)
        GameObject piece;
        piece = GameObject.CreatePrimitive(PrimitiveType.Cube);
        piece.transform.position = transform.position + new Vector3(particleSize * x, particleSize * y, particleSize * z);
        piece.transform.localScale = new Vector3(particleSize, particleSize, particleSize);
        Rigidbody pieceRigidBody = piece.AddComponent<Rigidbody>();
        pieceRigidBody.mass = particleSize;
        piece.GetComponent<Renderer>().material = enemyMaterial;
    }

    void OnTriggerEnter(Collider other)
    {
        // If the enemy collides with a player projectile, the projectile is destroyed and the enemyHealth is reduced by 1
        if (other.gameObject.tag == "Projectile")
        {
            Debug.Log("Enemy hit!");
            Destroy(other.gameObject);
            enemyHealth--;
        }
    }
}
