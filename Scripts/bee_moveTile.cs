using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bee_moveTile : MonoBehaviour
{
    private Transform trans; // Transform of the bee object
    public GameObject hexagon; //One of the hexagon objects
    public Vector3[] sides;
    public float speed; // Speed this bee moves at
    public float rotate; // Parameter to help tune bees direction of movement
    private Vector3 move; // Direction this bee will move in
    private Vector3 startPoint; //The place this bee started at before the current movement

    public float x_bound; //The xbound for movement
    public float y_bound; // The ybound for movment

    public GameObject SetupManager;
    


    private Vector3[] sidePositions()
        {
        // Calculate the direction vectors for each side of the hexagon
        Vector3[] sideDirections = new Vector3[6];
        Vector3 hexagonPosition = hexagon.transform.position;
        float hexagonRadius = hexagon.transform.localScale.x / 2;
        for (int i = 0; i < 6; i++)
            {
            // Calculate the position of the midpoint of the side
            Vector3 sideMidpoint = hexagonPosition + Quaternion.Euler(0, 0, i * 60) * Vector2.up * hexagonRadius;

            // Calculate the direction of the side
            Vector3 sideDirection = (sideMidpoint - hexagonPosition).normalized;

            // Store the direction of the side
            sideDirections[i] = sideDirection;
            }

        return sideDirections;
        }


    // Start is called before the first frame update
    void Start()
        {
        trans = GetComponent<Transform>();
        sides = sidePositions();
        SetupManager = GameObject.Find("SetUp Manager");
        speed = SetupManager.GetComponent<SetUp>().speed;

        // Invokes changeDirection every 2 seconds
         // InvokeRepeating("changeDirection", 0f, 2f);

        // Invokes changeDirection+ every 2/3 *speed seconds
        InvokeRepeating("changeDirectionplus", 0f, 2.9f / speed);
        }

    


    // Gives a new random direction and speed for the bee to move
    // Only allows directions that maximise tiling
    void changeDirection()
        {
        startPoint = trans.position;
        float XDirection = Mathf.Cos(rotate); //Calculated directions to move tangent to a side of the hexagon
        float YDirection = Mathf.Sin(rotate);
           
        float param = Random.Range(0f, 6f);


            // It is slightly more likey for bees to move straight down than straight up
            if (param <= .5f)
                move = new Vector3(0f, 1f, 0);

            if (.5f < param && param <= 2f)
                move = new Vector3(0f, -1f, 0);

            if (2f < param && param <= 3f)
                move = new Vector3(XDirection, YDirection, 0);

            if (3f < param && param <= 4f)
                move = new Vector3(-XDirection, YDirection, 0);

            if (4f < param && param <= 5f)
                move = new Vector3(XDirection, -YDirection, 0);

            if (5f < param && param <= 6f)
                move = new Vector3(-XDirection, -YDirection, 0);
        }


    // Gives a new random direction and speed for the bee to move
    // Only allows directions that maximise tiling
    void changeDirectionplus()
        {
        startPoint = trans.position;
        

        float param = Random.Range(0f, 6f);

        if (param <= .5f)
            move = sides[0];

        if (.5f < param && param <= 1.5f)
            move = sides[1];

        if (1.5f < param && param <= 2.5f)
            move = sides[2];

        if (2.5f < param && param <= 4f)
            move = sides[3];

        if (4f < param && param <= 5f)
            move = sides[4];

        if (5f < param && param <= 6f)
            move = sides[5];
        }


    // Update is called once per frame
    void Update()
        {
        //Update the bees position
        trans.position += speed * move * Time.deltaTime;

        // Change direction if outside the bounds of the camera
        if (trans.position.x > x_bound || trans.position.x < -x_bound)
            {
            move = new Vector3(0, 0, 0);
            trans.position = startPoint;
            }
        if (trans.position.y > y_bound || trans.position.y < -y_bound)
            {
            move = new Vector3(0, 0, 0);
            trans.position = startPoint;
            }
        }
    }
