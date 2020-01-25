using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EyeLook : MonoBehaviour
{
    public float scaleFactor = 1;
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
	Vector2 newPos    = new Vector2();
	Quaternion newRot = new Quaternion();
	Vector2 mousePos  = new Vector2();
	mousePos.x = Event.current.mousePosition.x;
	mousePos.y = cam.pixelHeight - Event.current.mousePosition.y;

	newPos = (cam.ScreenToWorldPoint(mousePos) -
	    parent.transform.localPosition).normalized * scaleFactor;

	newPos = Vector2.ClampMagnitude(newPos, scaleFactor);

	//this.transform.localPosition = newPos;

	newRot = Quaternion.LookRotation(newPos, Vector3.left);
	newRot.x = 0;
	newRot.y = 0;
	transform.rotation = newRot;
    }
}
