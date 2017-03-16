using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;

public class InGameMenu : MonoBehaviour {

    public string epanet_units = "metric"; //"imperial";


    public Ray ray;
    RaycastHit hit;
    string pipe_status = "null";
    bool adding_pump = false, adding_valve = false;
    Vector3 point1, point2;
    List<Vector3> pipe_points = new List<Vector3>();
    public float default_pipe_section_width = 1f;
    public float default_pipe_joint_width = 2f;

    public GameObject raycast_highlight;
    public GameObject notes_panel;
    public GameObject infrastructure_panel;
    public string launch_name;

    int infra_layer;

    void Start() {
        hit = new RaycastHit();
        launch_name = "null";
        infrastructure_panel.GetComponent<CanvasGroup>().alpha = 0;
        infra_layer = LayerMask.NameToLayer("infrastructure");
    }

    void Update() {
        ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out hit))
        {
            if (hit.collider != null)
            {
                var hp = hit.point;
                GameObject hit_object = hit.collider.transform.gameObject;
                raycast_highlight.GetComponent<Transform>().position = hp - ray.direction;
                //if (hit_object.layer == LayerMask.NameToLayer("infrastructure"))
                //{
                //    Debug.Log(hit_object.name);
                //}
                if (Input.GetMouseButtonDown(0))
                {
                    HandleLeftMouseClick(hit);
                }
                //FOR TOUCH INPUT, SEE: http://answers.unity3d.com/questions/1115464/ispointerovergameobject-not-working-with-touch-inp.html

                if (Input.GetMouseButtonDown(1)) //right button resets
                {
                    HandleRightMouseClick(hit);
                }
            }
            Debug.DrawLine(ray.origin, hit.point, Color.red);
        }
    }

    public void RedButtonPressed()          {launch_name = "red_ball";}
    public void GreenButtonPressed()        {launch_name = "green_cube";}
    public void BlueButtonPressed()         {launch_name = "blue_pill";}

    public void pipe_button_press()         {launch_name = "pipe";}
    public void reservoir_button_press()    {launch_name = "reservoir";}
    public void customer_button_press()     {launch_name = "customer";}
    public void tank_button_press()         {launch_name = "tank";}
    public void pump_button_press()         {launch_name = "pump";}
    public void valve_button_press()        {launch_name = "valve";}

    public void ButtonReset()
    {
        EventSystem.current.SetSelectedGameObject(null);
    }

    void LaunchObject(string object_name)
    {
        if (object_name == "null") return;
        update_notes_panel(object_name);
        Debug.Log("I will try to launch " + object_name + " at " + hit.point);
        GameObject launch = Instantiate(Resources.Load(object_name), hit.point + new Vector3(0, 4, 0), Quaternion.identity) as GameObject;
        update_notes_panel("");
    }

    /*IEnumerator InstantiateObject(string object_name)    {        yield return new WaitForSeconds(1);        update_notes_panel(object_name);        yield return null;    }*/

    private bool IsPointerOverUIObject()
    {
        PointerEventData eventDataCurrentPosition = new PointerEventData(EventSystem.current);
        eventDataCurrentPosition.position = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
        List<RaycastResult> results = new List<RaycastResult>();
        EventSystem.current.RaycastAll(eventDataCurrentPosition, results);
        return results.Count > 0;
    }

    void update_notes_panel(string text)
    {
        notes_panel.GetComponentInChildren<Text>().text = text;
    }

    void PlacePipeSection(string object_name, float width_scalar, Vector3 start, Vector3 end)
    {
        Vector3 offset = end - start;
        Vector3 scale = new Vector3(width_scalar, offset.magnitude / 2.0f, width_scalar);
        GameObject pipe = Instantiate(Resources.Load(object_name), (start+end) / 2, Quaternion.identity) as GameObject;
        pipe.transform.up = offset;
        pipe.transform.localScale = scale;
        //Debug.Log(pipe.transform.localScale);
        var size = pipe.GetComponent<Transform>().localScale;
        pipe.GetComponent<pipe>().set("pipe1", "-2", "-1", "debug", size[1], size[2], -1, -1, infrastructure_panel);

    }

    void PlacePipeJoint(string object_name, float width_scalar, Vector3 where)
    {
        GameObject joint = Instantiate(Resources.Load(object_name), where, Quaternion.identity) as GameObject;
        joint.GetComponent<Transform>().localScale *= width_scalar;
        var pos = joint.GetComponent<Transform>().position;
        joint.GetComponent<joint>().set("joint1", pos[1], infrastructure_panel);
    }

    void PlaceReservoir(string object_name, float scalar, Vector3 where)
    {
        GameObject reservoir = Instantiate(Resources.Load(object_name), where, Quaternion.identity) as GameObject;
        reservoir.GetComponent<Transform>().localScale *= scalar;
        reservoir.GetComponent<Transform>().Translate(0, reservoir.transform.localScale.y * .9f, 0);
        var pos = reservoir.GetComponent<Transform>().position;
        reservoir.GetComponent<reservoir>().set("reservoir1", 100, infrastructure_panel, pos[1]);
    }

    void PlaceTank(string object_name, float scalar, Vector3 where)
    {
        GameObject tank = Instantiate(Resources.Load(object_name), where, Quaternion.identity) as GameObject;
        tank.GetComponent<Transform>().localScale *= scalar;
        tank.GetComponent<Transform>().Translate(0, tank.transform.localScale.y * .9f, 0);
        var pos = tank.GetComponent<Transform>().position;
        tank.GetComponent<tank>().set("tank1", 10, 15, 20, 20, 100, infrastructure_panel, pos[1]);
    }

    void PlaceCustomer(string object_name, float scalar, Vector3 where)
    {
        GameObject customer = Instantiate(Resources.Load(object_name), where, Quaternion.identity) as GameObject;
        customer.GetComponent<Transform>().localScale *= scalar;
        customer.GetComponent<Transform>().Translate(0, customer.transform.localScale.y * .9f, 0);
        var pos = customer.GetComponent<Transform>().position;
        customer.GetComponent<customer>().set("customer1", pos[1], infrastructure_panel);
    }

    void PlacePump(string object_name, float width_scalar, Vector3 start, Vector3 end)
    {
        Vector3 offset = end - start;
        Vector3 scale = new Vector3(width_scalar, offset.magnitude / 2.0f, width_scalar);
        GameObject pump = Instantiate(Resources.Load(object_name), (start + end) / 2, Quaternion.identity) as GameObject;
        pump.transform.up = offset;
        pump.transform.localScale = scale;
        var size = pump.GetComponent<Transform>().localScale;
        pump.GetComponent<pump>().set("pump1", "-2", "-1", infrastructure_panel);
    }

    void PlaceValve(string object_name, float width_scalar, Vector3 start, Vector3 end)
    {
        Vector3 offset = end - start;
        Vector3 scale = new Vector3(width_scalar, offset.magnitude / 2.0f, width_scalar);
        GameObject valve = Instantiate(Resources.Load(object_name), (start + end) / 2, Quaternion.identity) as GameObject;
        valve.transform.up = offset;
        valve.transform.localScale = scale;
        var size = valve.GetComponent<Transform>().localScale;
        valve.GetComponent<valve>().set("valve1", "-2", "-1", 4f, -1, "default", "okay", infrastructure_panel);
    }


    void HandleLeftMouseClick(RaycastHit hit)
    {
        var hp = hit.point;
        if (!EventSystem.current.IsPointerOverGameObject())
        {
            GameObject hit_object = hit.collider.transform.gameObject;
            Vector3 hit_point_center = hit_object.GetComponent<Transform>().position;
            if (launch_name == "pipe")
            {
                //move the hit point to the centroid of the object raycasted if that object is a piece of infrastructure
                //obvious issues with two-point objects (pipes, valves, pumps)
                if (hit_object.layer == infra_layer)
                {
                    Debug.Log("Attach to infrastructure: move object from " + hp + " to " + hit_point_center);
                    hp = hit_point_center;
                }
                if (pipe_status == "null")
                {
                    update_notes_panel("start pipe!");
                    point1 = hp;
                    pipe_points = new List<Vector3>();
                    pipe_points.Add(point1);
                    pipe_status = "piping";
                    PlacePipeJoint("default_pipe_joint", default_pipe_joint_width, point1);
                }
                else if (pipe_status == "piping")
                {
                    update_notes_panel("piping...");
                    pipe_points.Add(hp);
                    PlacePipeSection("default_pipe_section", default_pipe_section_width, pipe_points[pipe_points.Count - 2], pipe_points[pipe_points.Count - 1]);
                    PlacePipeJoint("default_pipe_joint", default_pipe_joint_width, pipe_points[pipe_points.Count - 1]);
                }

            }
            else if(launch_name == "pump")
            {
                if(!adding_pump)
                {
                    update_notes_panel("start pump!");
                    point1 = hp;
                    adding_pump = true;
                }
                else
                {
                    point2 = hp;
                    adding_pump = false;
                    PlacePump(launch_name, 3f, point1, point2);
                    update_notes_panel("done with pump");
                }
            }
            else if (launch_name == "valve")
            {
                if (!adding_valve)
                {
                    update_notes_panel("start valve!");
                    point1 = hp;
                    adding_valve = true;
                }
                else
                {
                    point2 = hp;
                    adding_valve = false;
                    PlaceValve(launch_name, 3f, point1, point2);
                    update_notes_panel("done with valve");
                }
            }
            else if (launch_name == "reservoir")
            {
                update_notes_panel(launch_name);
                PlaceReservoir(launch_name, 6f, hp);
            }
            else if (launch_name == "customer")
            {
                update_notes_panel(launch_name);
                PlaceCustomer(launch_name, 4f, hp);
            }
            else if (launch_name == "tank")
            {
                update_notes_panel(launch_name);
                PlaceTank(launch_name, 4f, hp);
            }
            else
            {
                LaunchObject(launch_name);
            }
        }
    }

    void HandleRightMouseClick(RaycastHit hit)
    {
        GameObject hit_object = hit.collider.transform.gameObject;
        Vector3 hit_point_center = hit_object.GetComponent<Transform>().position;
        var hp = hit.point;
        if (pipe_status == "piping")
        {
            point2 = hp;
            //move the hit point to the centroid of the object raycasted if that object is a piece of infrastructure
            //obvious issues with two-point objects (pipes, valves, pumps)
            if (hit_object.layer == infra_layer)
            {
                Debug.Log("Attach to infrastructure: move object from " + hp + " to " + hit_point_center);
                hp = hit_point_center;
            }

            pipe_points.Add(point2);
            PlacePipeSection("default_pipe_section", default_pipe_section_width, pipe_points[pipe_points.Count - 2], point2);
            PlacePipeJoint("default_pipe_joint", default_pipe_joint_width, point2);
            Debug.Log("start pipe at " + pipe_points[0]);
            for (int i = 1; i < pipe_points.Count; i++)
            {
                Debug.Log("draw pipe between " + pipe_points[i - 1] + " and " + pipe_points[i]);
                if (i < pipe_points.Count - 1) Debug.Log("place joint at " + pipe_points[i]);
            }
            Debug.Log("end pipe at " + pipe_points[pipe_points.Count - 1]);
            update_notes_panel("piped!");
            pipe_status = "null";
        }
        launch_name = "null";
    }
}
