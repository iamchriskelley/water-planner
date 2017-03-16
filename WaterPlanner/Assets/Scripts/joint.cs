using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class joint : infrastructure
{
    public string shape;
    public string material;
    public float latitude, longitude;
    public List<float> demand_pattern;
    public float elevation;
    GameObject display_target;
    string display_string;

    void Awake()
    {
        id = "null";
        infrastructure_type = "joint";
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

    public void set(string joint_id, float joint_elevation, GameObject info_display_target, List<float> joint_demand_pattern = null)
    {
        id = joint_id;
        elevation = joint_elevation;
        display_target = info_display_target;
        demand_pattern = joint_demand_pattern ?? new List<float>();
    }

    public void test() { Debug.Log("tested!"); }
    
    void format_display_string()
    {
        display_string = "joint: " + id + "\nelevation: " + elevation;
    }
}
