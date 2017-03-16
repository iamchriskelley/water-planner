using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class pump : infrastructure
{

    public float latitude1, longitude1, latitude2, longitude2;
    public string node1, node2;
    public List<string> parameters;
    
    GameObject display_target;
    string display_string;

    void Awake()
    {
        id = "null";
        infrastructure_type = "pump";
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

    public void set(string pump_id, string pump_node1, string pump_node2, GameObject info_display_target, List<string> pump_parameters = null)
    {
        id = pump_id;
        node1 = pump_node1;
        node2 = pump_node2;
        parameters = pump_parameters ?? new List<string>();
        display_target = info_display_target;
    }

    public void test() { Debug.Log("tested!"); }

    public void set(GameObject info_display_target)
    {
        display_target = info_display_target;
    }

    void format_display_string()
    {
        display_string = "pump: " + id;
    }
}
