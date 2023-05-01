using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bee_noOverlap : MonoBehaviour
{
    private bool isTouchingHex = false;
    public float size; //Size of the hexagon ot place
    public hex_Object hexobj; //The script for managing hexagons
    public string color; //The color of hexagon this bee should place
    public bool Sensors_On; //Should this bee have a sensor

    // Relevent managers
    public GameObject SetupManager;
    public GameObject WaveSpawner;

    // Relevant Scripts
    public WaveSpawn wavespawn;
    public VoronoiDiagram Voronoi;

    // Start is called before the first frame update
    void Start()
        {
        float speed;
        SetupManager = GameObject.Find("SetUp Manager");
        WaveSpawner = GameObject.Find("Wave Spawner");

        speed = SetupManager.GetComponent<SetUp>().speed;
        wavespawn = WaveSpawner.GetComponent<WaveSpawn>();
        Voronoi = GameObject.Find("Voronoi Manager").GetComponent<VoronoiDiagram>();

        InvokeRepeating("place", 0f, 2.9f / speed);
        }

    // places a hexagon if this bee is not in another hexagon and this bee cannot see both colors of hex
    void place()
        {
        bool touchingBothColors = false;
        if (Sensors_On)
            {
            // This is the most backwards way to possibly achieve this but it works + its funny
            CircleCollider2D circleCollider = this.gameObject.GetComponentInChildren<CircleCollider2D>();
            if (hexobj.hex_isTouchingHex(circleCollider.gameObject) != color && hexobj.hex_isTouchingHex(circleCollider.gameObject) != "") touchingBothColors = true;
            }


        if (!isTouchingHex && !touchingBothColors)
            {
            GameObject hex = hexobj.hex_createHex(GetComponent<Transform>(), size, color);
            Vector3 position = hex.transform.position;

            if (!Voronoi.points3D.Contains(position))
                {
                Voronoi.points3D.Add(position);

                }
            


            if (hexobj.hex_isTouchingHex(hex) != "")
                {
                Voronoi.points3D.Remove(position);
                
                hexobj.hex_destroyHex(hex);
                }

            else if (hexobj.hex_isTouchingHex(this.gameObject) == "redblue")
                {
                Voronoi.points3D.Remove(position);
                
                hexobj.hex_destroyHex(hex);
                }
            else
                {
                // Lets the wave manager know a vertex was placed this update
                wavespawn.hex_laid = true;


                }
            }
        }


    // These functions let the bee know whether it is inside a hex
    private void OnTriggerEnter2D(Collider2D collision)
        {
        if (hexobj.hex_isTouchingHex(this.gameObject) != "") isTouchingHex = true;
        }



    private void OnTriggerExit2D(Collider2D collision)
        {
        if (hexobj.hex_isTouchingHex(this.gameObject) == "") isTouchingHex = false;
        }
    }
