using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shoot : MonoBehaviour
{
    public float velocity = 1;
    public GameObject bullet;

    private Camera cam;

    void Start()
    {
	cam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
    }

    void Update()
    {
        //if click, instantiate bullet prefab with velocity and direction towards mouse
	if(Input.GetMouseButtonDown(0))
	{
	    Vector2 mousePos = new Vector2();
	    mousePos.x = Input.mousePosition.x;
	    mousePos.y = Input.mousePosition.y;

	    Vector2 dir = (cam.ScreenToWorldPoint(mousePos) - this.transform.localPosition);
	    //dir.y = -dir.y;

	    //dir = Vector2.ClampMagnitude(dir, velocity);

	    GameObject newBullet = GameObject.Instantiate(bullet, this.transform.localPosition, Quaternion.identity);
	    newBullet.GetComponent<Rigidbody2D>().AddForce(dir * velocity);

	    Debug.DrawLine(this.transform.localPosition, dir, Color.red, 2, false);
	    Debug.DrawLine(this.transform.localPosition, cam.ScreenToWorldPoint(mousePos), Color.yellow, 2, false);
	}
    }
}
