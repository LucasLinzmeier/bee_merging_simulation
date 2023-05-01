using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveSpawn : MonoBehaviour
    {
    // Counts for how many updates(Not Physics updates!!!) without an object being laid
    private int updates_without_vertex;
    private int updates_without_hex;

    // bools to be manipulated by other functions that encourage state transition
    public bool vertex_laid;
    public bool hex_laid;


    private bool waveSpawned = false;
    private bool simEnded = false;

    public GameObject sensor;

    public GameObject SetupManager;
    private float y_bound;
    private int num_bee_merge;

    public voronoi_diagram Voronoi;

    private float speed;
    // Start is called before the first frame update
    void Start()
        {
        SetupManager = GameObject.Find("SetUp Manager");
        Voronoi = GameObject.Find("Voronoi Manager").GetComponent<voronoi_diagram>();
        speed = SetupManager.GetComponent<SetUp>().speed;
        y_bound = SetupManager.GetComponent<SetUp>().y_bound;
        num_bee_merge = SetupManager.GetComponent<SetUp>().numbee_merge;

        updates_without_hex = 0;

        if (PlayerPrefs.GetInt("naive") == 0)
            InvokeRepeating("checkSpawn", 0f, 2.9f / speed);
        else InvokeRepeating("naiveFinish", 0f, 2.9f / speed);

        Debug.Log(SetupManager.GetComponent<SetUp>().Sensors_On);
        }

    void naiveFinish()
        {

        Debug.Log("HERE!");

        if (hex_laid) updates_without_hex = 0;
        else updates_without_hex++;

        if (updates_without_hex >= 5)
            {
            Voronoi.drawTime = true;
            }
        hex_laid = false;
        }






    void checkEndSim()
        {
        if (!simEnded)
            {
            if (vertex_laid) updates_without_vertex = 0;
            else updates_without_vertex++;

            if (updates_without_vertex >= 5 || num_bee_merge == 0)
                {
                Voronoi.drawTime = true;
                SetUp setup = SetupManager.GetComponent<SetUp>();

                setup.cam.transform.position = new Vector3(Voronoi.gameObject.transform.position.x, Voronoi.gameObject.transform.position.y, 0);
                
                setup.cam.orthographicSize = 4;

                simEnded = true;
                }

            vertex_laid = false;
            }
        }


    void checkSpawn()
        {
        if (!waveSpawned)
            {
            if (hex_laid) updates_without_hex = 0;
            else updates_without_hex++;

            if (updates_without_hex >= 5)
                {
                waveSpawned = true;
                

                // Spawn the merge bees
                GameObject beemerge = SetupManager.GetComponent<SetUp>().beemerge;
                int numbee = SetupManager.GetComponent<SetUp>().numbee_merge;

                for (int i = 0; i < numbee; i++)
                    {
                    GameObject bee = Instantiate(beemerge);
                    bee.transform.position = new Vector3(0, (-y_bound + ((2 * y_bound) / numbee) * i), 0);

                    GameObject sensorX = Instantiate(sensor, bee.transform);
                    sensorX.transform.position = bee.transform.position;

                    }

                //Start Checking for the end of simulation
                InvokeRepeating("checkEndSim", 0f, 2.9f / speed);
                }
            hex_laid = false;

            
            }

        
        // Update is called once per frame
        void Update()
            {

            }
        }
    }
