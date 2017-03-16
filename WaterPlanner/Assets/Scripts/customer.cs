using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class customer : infrastructure
{
    public string address;
    public string demographic_note;
    public float latitude, longitude;
    public string connection_type;
    public float population;

    public List<float> demand_pattern;
    public float elevation;
    
    GameObject display_target;
    string display_string;

    void Awake()
    {
        id = "null";
        infrastructure_type = "customer";
        demand_pattern = null;
        elevation = -9999f;
        //display_target = null;
        display_string = "";
    }

    void OnMouseEnter()
    {
        if (display_target == null)
        {
            Debug.Log("warning: " + infrastructure_type + "_" + id + " has been instantiated without be fully initialized...");
        }
        else
        {
            format_display_string();
            display_target.GetComponentInChildren<Text>().text = display_string;
            display_target.GetComponent<CanvasGroup>().alpha = 1;
        }
    }

    void OnMouseExit()
    {
        if (display_target != null)
        {
            display_target.GetComponentInChildren<Text>().text = "";
            display_target.GetComponent<CanvasGroup>().alpha = 0;
        }
    }

    public void set(string customer_id, float customer_elevation, GameObject info_display_target, List<float> customer_demand_pattern = null)
    {
        id = customer_id;
        elevation = customer_elevation;
        display_target = info_display_target;
        demand_pattern = customer_demand_pattern ?? new List<float>();
    }

    public void test() { Debug.Log("tested!"); }

    void format_display_string()
    {
        display_string = "customer: " + id + "\nelevation: " + elevation;
    }
}
