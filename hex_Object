using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// This class implements the hexagon object
public class hex_Object : MonoBehaviour
    {
    public GameObject hex_BLUETemplate; //This is the template for the hexagon blue object
    public GameObject hex_REDTemplate; //This is the template for the hexagon blue object
    private VoronoiDiagram Voronoi;

    private LayerMask HEX_LAYER_BLUE; //The blue hex layer
    private LayerMask HEX_LAYER_RED; //The blue hex layer
    private LayerMask HEX_LAYER; //Layers for all hexes

    void Start()
        {
        HEX_LAYER_BLUE = LayerMask.GetMask("hex_blue"); //The blue hex layer
        HEX_LAYER_RED = LayerMask.GetMask("hex_red"); //The blue hex layer
        HEX_LAYER = LayerMask.GetMask("hex_red","hex_blue"); // The layers for all hexes

        Voronoi = GameObject.Find("Voronoi Manager").GetComponent<VoronoiDiagram>();
        }

    // Instatiates a hexagon object at the position specified by trans
    // and with scale of size, and with color specifeid by color returns a reference to the object
    public GameObject hex_createHex(Transform trans, float size, string color)
        {
        GameObject hex = null;
        if (color == "red")
        hex= Instantiate(hex_REDTemplate);

        if (color == "blue")
        hex = Instantiate(hex_BLUETemplate);

        

        hex.transform.position = trans.position;
        hex.transform.localScale = new Vector3(size, size, 1);

        

        return hex;
        }

    public void hex_destroyHex(GameObject hex)
        {
        
        Destroy(hex);
        }

    // This function returns true if this hex is touching another
    // Defunct due to physics sytem not updateing every frame
    // Will most likely be useless.  Update need to fix
   /* public bool hex_areTouch(GameObject hex)
        {
        PolygonCollider2D poly1 = hex.GetComponent<PolygonCollider2D>();
        bool touch = poly1.IsTouchingLayers(HEX_LAYER_BLUE) || poly1.IsTouchingLayers(HEX_LAYER_RED);
        return touch;
        }
   */

    public bool hex_areTouch(GameObject hex)
        {
        Collider2D[] colliders = new Collider2D[20];  //This is awful code.  If this function breaks check this
        ContactFilter2D filter = new ContactFilter2D();
        filter.SetLayerMask(HEX_LAYER);
        PolygonCollider2D poly1 = hex.GetComponent<PolygonCollider2D>();
        Physics2D.OverlapCollider(poly1, filter, colliders);
        foreach (Collider2D collider in colliders)
            {
            if (collider.gameObject != hex)
                {
                return true;
                }
            }
        return false;
        }


    // Has an object check whether it is currently in a hexagon, returns "red" if it is red, "blue if blue" and
    //"redblue" if both and "" if neither
    public string hex_isTouchingHex(GameObject obj)
        {
            string str = "";
            if (obj.GetComponent<Collider2D>().IsTouchingLayers(HEX_LAYER_RED)) str += "red"; 
            
            if (obj.GetComponent<Collider2D>().IsTouchingLayers(HEX_LAYER_BLUE)) str += "blue"; 

            return str;
                        
        }
