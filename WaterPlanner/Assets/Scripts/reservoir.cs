using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class reservoir : infrastructure
{
    public string address;
    public string demographic_note;
    public string connection_type;
    public float population;

    public List<float> pattern;
    public float elevation;
    public float head;

    GameObject display_target;
    string display_string;

    void Awake()
    {
        id = "null";
        infrastructure_type = "reservoir";
        pattern = null;
        elevation = -9999f;
        head = -9999f;
        display_target = null;
        display_string = "";
    }

    void OnMouseEnter()
    {
        if (display_target == null)
        {
            Debug.Log("warning: reservoir #" + id + " has been instantiated without be fully initialized...");
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

    public void set(string reservoir_id, float reservoir_head, GameObject info_display_target, float reservoir_elevation = -9999, List<float> reservoir_pattern = null)
    {
        id = reservoir_id;
        head = reservoir_head;
        elevation = reservoir_elevation;
        display_target = info_display_target;
        pattern = reservoir_pattern ?? new List<float>();
        //if(reservoir_elevation == -9999) 
    }

    public void test() { Debug.Log("tested!"); }

    void format_display_string()
    {
        display_string = "reservoir: " + id + "\nelevation: " + elevation;
    }
}
