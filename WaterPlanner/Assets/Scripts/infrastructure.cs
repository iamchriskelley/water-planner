using UnityEngine;
using System.Collections;

public class infrastructure : MonoBehaviour {

    public string id;
    public string infrastructure_type;
    Material highlight_material;
    Renderer rend;
    Material mat;
    Color color;
    string type;

    bool verbose = false;

    void Start()
    {
        highlight_material = Resources.Load("Materials/infrastructure_highlight", typeof(Material)) as Material;
        rend = gameObject.GetComponent<Renderer>();
        mat = rend.material;
        color = mat.color;
    }
    void OnMouseEnter()
    {
        if(verbose) Debug.Log("mousein");
        //rend.material.color = Color.green;
        rend.material = highlight_material;
    }

    void OnMouseExit()
    {
        if (verbose) Debug.Log("mouseout");
        //rend.material.color = color;
        rend.material = mat;
    }
}
