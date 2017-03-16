using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class pipe : infrastructure {

    public string material;
    public float latitude1, longitude1, latitude2, longitude2;
    public string node1, node2, status;
    public float length, diameter, roughness, minor_loss;
    GameObject display_target;
    string display_string;

    void Awake () {
        id = "null";
        infrastructure_type = "pipe";
        node1 = "null";
        node2 = "null";
        status = "uninitialized";
        roughness = -1;
        minor_loss = -1;

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

    public void set(string pipe_id, string pipe_node1, string pipe_node2, string pipe_status, float pipe_length, float pipe_diameter, float pipe_roughness, float pipe_minor_loss, GameObject info_display_target)
    {
        id = pipe_id;
        node1 = pipe_node1;
        node2 = pipe_node2;
        status = pipe_status;
        length = pipe_length;
        diameter = pipe_diameter;
        display_target = info_display_target;
    }

    public void test() { Debug.Log("tested!"); }

    public void set(GameObject info_display_target)
    {
        display_target = info_display_target;
    }

    void format_display_string()
    {
        display_string = "pipe: " + id + "\nlength: " + length;
    }
}
