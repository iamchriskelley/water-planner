using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class tank : infrastructure
{
    public float latitude, longitude;
    public float elevation, init_level, min_level, max_level, diameter, min_volume;
    public List<float> volume_curve;

    GameObject display_target;
    string display_string;

    void Awake()
    {
        id = "null";
        infrastructure_type = "tank";
        volume_curve = null;
        elevation = -9999f;
        init_level = -9999f;
        min_level = -9999f;
        max_level = -9999f;
        diameter = -1;
        display_target = null;
        display_string = "";
    }

    void OnMouseEnter()
    {
        if (display_target == null)
        {
            Debug.Log("warning: tank #" + id + " has been instantiated without be fully initialized...");
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

    public void set(string tank_id, float tank_init_level, float tank_min_level, float tank_max_level, float tank_diameter, float tank_min_volume, GameObject info_display_target, float tank_elevation = -9999, List<float> tank_pattern = null)
    {
        id = tank_id;
        elevation = tank_elevation;
        init_level = tank_init_level;
        min_level = tank_min_level;
        max_level = tank_max_level;
        diameter = tank_diameter;
        min_volume = tank_min_volume;
        display_target = info_display_target;
        volume_curve = tank_pattern ?? new List<float>();
        //if(tank_elevation == -9999) 
    }

    public void test() { Debug.Log("tested!"); }

    void format_display_string()
    {
        display_string = "tank: " + id + "\nelevation: " + elevation;
    }
}
