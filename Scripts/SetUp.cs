using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetUp : MonoBehaviour
{
    public GameObject beetype1;
    public GameObject beetype2;
    public GameObject beemerge;

    public GameObject sensor;
    public Camera cam;

    public int num_bee;
    public int numbee_1;
    public int numbee_2;
    public int numbee_merge;

    public Vector3 positionbee_1;
    public Vector3 positionbee_2;

    public float speed;
    public float x_bound;
    public float y_bound;
    public float cameraSize;
    public bool Sensors_On;

    public bee_moveTile moveScript;
    public bee_noOverlap hexLayScript;

    public float startTime;

    private bool allspawned = false;
    // Start is called before the first frame update
    void Start()
    {
        //Get information from PlayerPrefs to set values for Setup
        speed = PlayerPrefs.GetFloat("speed");
        Sensors_On = PlayerPrefs.GetInt("naive") == 0;
        num_bee = PlayerPrefs.GetInt("bee");
        numbee_1 = num_bee / 2;
        numbee_2 = num_bee - numbee_1;


        GameObject hexManager = GameObject.Find("Hex Manager");
        // Spawn the bees
        for (int i = 0; i < numbee_1; i++) {
            GameObject bee = Instantiate(beetype1);

            if (Sensors_On)
                {
                GameObject sensorX = Instantiate(sensor, bee.transform);
                sensorX.transform.position = bee.transform.position;
                }

            bee.transform.position = positionbee_1;
            bee.GetComponent<bee_noOverlap>().hexobj = hexManager.GetComponent<hex_Object>();
            }

        startTime = Time.time;

        
    

        //Set bounds for each bee and camera size
        // Set Sensor_On or Off for each bee

        GameObject[] bees = GameObject.FindGameObjectsWithTag("bee");
        for (int i = 0; i < bees.Length; i++)
            {
            moveScript = bees[i].GetComponent<bee_moveTile>();
            moveScript.x_bound = x_bound;
            moveScript.y_bound = y_bound;

            hexLayScript = bees[i].GetComponent<bee_noOverlap>();
            hexLayScript.Sensors_On = Sensors_On;
            }
        cam.orthographicSize = cameraSize;
     }



    // Update is called once per frame
    void Update()
    {
        if(Time.time > startTime + .1 && !allspawned)
            {
            // Wait for a moment then spawn the next bees

            GameObject hexManager = GameObject.Find("Hex Manager");

            for (int i = 0; i < numbee_2; i++)
                {
                GameObject bee = Instantiate(beetype2);

                if (Sensors_On)
                    {
                    GameObject sensorX = Instantiate(sensor, bee.transform);
                    sensorX.transform.position = bee.transform.position;
                    }

                bee.transform.position = positionbee_2;
                bee.GetComponent<bee_noOverlap>().hexobj = hexManager.GetComponent<hex_Object>();
                }

            //Set bounds for each bee and camera size
            // Set Sensor_On or Off for each bee

            GameObject[] bees = GameObject.FindGameObjectsWithTag("bee");
            for (int i = 0; i < bees.Length; i++)
                {
                moveScript = bees[i].GetComponent<bee_moveTile>();
                moveScript.x_bound = x_bound;
                moveScript.y_bound = y_bound;

                hexLayScript = bees[i].GetComponent<bee_noOverlap>();
                hexLayScript.Sensors_On = Sensors_On;
                }

            allspawned = true;
            }
    }
}
