using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bee_merge_move : MonoBehaviour
{
    public bool inPosition = false; //Is this bee in between the combs and ready to merge
    private bool didThis = false; // Dont worry about it

    private LayerMask VERTEX_LAYER; // Layer for all vertices
    

    private Transform trans; // Transform of the bee object

    public float speed; // Speed this bee moves at
    private Vector3 move; // Direction this bee will move in, always normalized

    public float x_bound; //The xbound for movement
    public float y_bound; // The ybound for movment

    private GameObject SetupManager;
    private VoronoiDiagram Voronoi;
    private WaveSpawn waveSpawn;

    public GameObject vertex; // The vertex object for this bee to lay
    private hex_Object hexobj; //The script for managing hexagons

    // Start is called before the first frame update
    void Start()
    {
        trans = GetComponent<Transform>();

        SetupManager = GameObject.Find("SetUp Manager");
        Voronoi = GameObject.Find("Voronoi Manager").GetComponent<VoronoiDiagram>();
        waveSpawn = GameObject.Find("Wave Spawner").GetComponent<WaveSpawn>();
        

        speed = SetupManager.GetComponent<SetUp>().speed;
        x_bound = SetupManager.GetComponent<SetUp>().x_bound;
        y_bound = SetupManager.GetComponent<SetUp>().y_bound;

        hexobj = GameObject.Find("Hex Manager").GetComponent<hex_Object>();

        VERTEX_LAYER = LayerMask.GetMask("vertex"); //The vertex layer


        // The bee should begin by moving to the right
        move = new Vector3(1, 0, 0);

        
        }

    // Spawns a vertex object and returns a refernece to that object
    GameObject spawnVertex()
        {
        GameObject vert = GameObject.Instantiate(vertex);
        vert.transform.position = trans.position;
        return vert;
        }

    // Changes the direction of the bee, greater range for y value will encourage bees to move more vertically
    void changeDirection()
        {
        move = new Vector3(Random.Range(-1, 1),Random.Range(-3, 3), 0);
        move = move.normalized;
        if (move == Vector3.zero) changeDirection();
        }
    // The bee searches for the space inbetween the combs
    void search ()
        {
        
        // Handle bonunds
        if (trans.position.x >= x_bound || trans.position.x <= -x_bound)
            {
            move.x = -move.x;
            trans.position += 3 * move * speed * Time.deltaTime;
            }
        if (trans.position.x >= y_bound || trans.position.y <= -y_bound)
            {
            move.y = -move.y;
            trans.position += 3 * move * speed * Time.deltaTime;
            }

        // Move the bee
        trans.position += move * speed * Time.deltaTime;

        // If the bee is now in position to merge, switch to merge mode and begin the changeDirection 
        if (hexobj.hex_isTouchingHex(this.gameObject) == "") { 
            inPosition = true;
            // Invokes changeDirection+ every 2/3 *speed seconds
            InvokeRepeating("changeDirection", 0f, 2.9f / speed);
            }
        }


    void placeVertex() {

        GameObject vert = null; 
        //If there are no verticies in the sensor collider of this bee lay a vertex
        CircleCollider2D circleCollider = this.gameObject.GetComponentInChildren<CircleCollider2D>();
        if (!circleCollider.IsTouchingLayers(VERTEX_LAYER) && hexobj.hex_isTouchingHex(circleCollider.gameObject) == "")
            {
            vert = spawnVertex();

            Vector3 pos = vert.transform.position;

            bool inBounds = true;
            if (this.gameObject.transform.position.x > x_bound || vert.transform.position.x < -x_bound) inBounds = false;
            if (this.gameObject.transform.position.y > y_bound || vert.transform.position.y < -y_bound) inBounds = false;
            if (!Voronoi.points3D.Contains(pos) && inBounds) Voronoi.points3D.Add(pos);

            }
        if (vert != null)
            {
            if (vert.transform.position.x > x_bound || vert.transform.position.x < -x_bound) Destroy(vert);
            else if(vert.transform.position.y > y_bound || vert.transform.position.y < -y_bound) Destroy(vert);

            else waveSpawn.vertex_laid = true;
            }
        }

    // The bee goes about merging the combs
    void merge()
        {
        GameObject vert = null;
        // Handle bonunds
        if (trans.position.x >= x_bound || trans.position.x <= -x_bound) {
            move.x = -move.x;
            trans.position += 3 * move * speed * Time.deltaTime;
            }
        if (trans.position.x >= y_bound || trans.position.y <= -y_bound) { 
            move.y = -move.y;
            trans.position += 3 * move * speed * Time.deltaTime;
            }



        

        

        // move the bee
        trans.position += move * speed * Time.deltaTime;
        }

    // Update is called once per frame
    void Update()
    {
 
        if (!inPosition) search();
        else merge();

        if (inPosition && !didThis)
            {
            InvokeRepeating("placeVertex", .3f, .3f);
            didThis = true;
            }


    }

    private void OnTriggerEnter2D(Collider2D collision)
        {
        if (hexobj.hex_isTouchingHex(this.gameObject) != "" && inPosition)
            {
            move.x = -move.x;
            move.y = -move.y;

            trans.position += 5 * move * speed * Time.deltaTime;
            }
        }
    }
