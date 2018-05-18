using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class playerController : MonoBehaviour
{

    public float speed; // player's speed

    public static int HOLDINGLIMIT = 200; //the longest holding frame for jump

    //store the building we players are now on
    public GameObject building;
    //store the destination building
    public GameObject destination;

    //create private reference to the rigidbody component on the player
    private Rigidbody rb;
    //determine the jumping direction
    private Vector3 jumpDirection;
    //judge whether player is on the ground
    private bool isOnGround;

    //store scores
    public static int scores;
    //judge the gameState ,0 for continue, 1 for victory,2 for lose
    public static int gameState;
    //store the holdingFrame
    public static int holdingFrame;

    void Start()
    {

        Debug.Log("activate player!!!!!!!!!!!!");

        gameState = 0;

        //give player a rigidbody
        rb = GetComponent<Rigidbody>(); //assign regidbody to player

        //init jump direction
        jumpDirection = new Vector3(transform.forward.x, 1, transform.forward.z);

        isOnGround = true; //init ison ground
        holdingFrame = 0; //init holding frame

        //init scores
        scores = 0;

        //cancel the conterforce
        rb.freezeRotation = true;

    }


    void FixedUpdate()
    {


        //change direction of player
        if (Input.GetKeyDown("w")) changeDirection();


        //jump
        if (Input.GetKey(KeyCode.Space))
        {
            if (holdingFrame < HOLDINGLIMIT)
            {
                Debug.Log("getting space!!!!!!!!!!!!");
                holdingFrame++;
            }

        }

        if (Input.GetKeyUp(KeyCode.Space))
        {
            if (isOnGround == true)
            {

                Debug.Log("Adding Force!!!!!!!!!!!");
                //move
                rb.AddForce(jumpDirection * (speed + holdingFrame));
                holdingFrame = 0;
            }
        }

        if (transform.position.y <= 0) gameState = 2;
    }

    private void OnCollisionEnter(Collision other)
    {
        //jumping failed
        if (other.collider.gameObject.tag == "Plane") gameState = 2;
        //jumping successed
        else if (other.collider.gameObject.tag == "Building" && other.collider.gameObject != building)
        {
            if (transform.position.y > other.collider.gameObject.transform.position.y)
            {
                building = other.collider.gameObject;
                jumpSuccess();
            }
        }
    }

    void changeDirection()
    {
        //determine the roattion axis
        Vector3 axis = new Vector3(0, 1, 0);
        //determine the rotation angle
        float angle = 45;
        //rotatte
        transform.Rotate(axis, angle);
        jumpDirection = new Vector3(transform.forward.x, 1, transform.forward.z);
    }

    void jumpSuccess()
    {
        //set score
        updateScore(distance());
    }

    float distance()
    {
        //get the distance from we player to the center of the building
        return Mathf.Sqrt((transform.position.x - building.transform.position.x) * (transform.position.x - building.transform.position.x) + (transform.position.z - building.transform.position.z) * (transform.position.z - building.transform.position.z));
    }

    void updateScore(float dist)
    {
        //giving a score by the distance
        if (dist > 1.4) scores += 1;
        else if (dist > 1.0) scores += 2;
        else if (dist > 0.6) scores += 4;
        else if (dist > 0.2) scores += 6;
        else scores += 10;

        //judge if we reached the destination
        if (building == destination) gameState = 1;
    }

}
