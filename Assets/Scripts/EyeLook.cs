using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EyeLook : MonoBehaviour
{
    public float scaleFactor = 5;
    private GameObject parent;
    private Camera cam;

    void Start()
    {
	parent = GameObject.FindGameObjectWithTag("Player");
	cam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
    }

    void OnGUI()
    {
	/* Set current position to parent transform (root) moved towards mouse pos */
	Vector2 mousePos = new Vector2();
	mousePos.x = Event.current.mousePosition.x;
	mousePos.y = cam.pixelHeight - Event.current.mousePosition.y;

	this.transform.localPosition = (cam.ScreenToWorldPoint(mousePos) -
	    parent.transform.localPosition).normalized * scaleFactor;

	//if(this.transform.localPosition.magnitude > 0.5f)
	    //this.transform.localPosition.normalized ;
    }
}
