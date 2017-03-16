using UnityEngine;

public class sandbox : MonoBehaviour
{
    GameObject foo;

    void Start()
    {
        Debug.Log("foo started");
        foo = null;
    }

    void OnMouseEnter()
    {
        Debug.Log(foo);
    }

    sandbox(GameObject bar)
    {
        foo = bar;
        Debug.Log("foo set");
    }
}
