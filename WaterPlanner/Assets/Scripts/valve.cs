using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class valve : infrastructure
{

    public float latitude1, longitude1, latitude2, longitude2;
    public string node1, node2, valve_type, setting;
    public float diameter, minor_loss;

    GameObject display_target;
    string display_string;

    void Awake()
    {
        id = "null";
        infrastructure_type = "valve";
        node1 = "null";
        node2 = "null";

        display_target = null;
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

    public void set(string valve_id, string valve_node1, string valve_node2, float valve_diameter, float valve_minor_loss, string valvetype, string valve_setting, GameObject info_display_target)
    {
        id = valve_id;
        node1 = valve_node1;
        node2 = valve_node2;
        diameter = valve_diameter;
        minor_loss = valve_minor_loss;
        valve_type = valvetype;
        setting = valve_setting;
        display_target = info_display_target;
    }

    public void test() { Debug.Log("tested!"); }

    public void set(GameObject info_display_target)
    {
        display_target = info_display_target;
    }

    void format_display_string()
    {
        display_string = "valve: " + id;
    }
}