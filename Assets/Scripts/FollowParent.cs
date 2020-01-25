using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowParent : MonoBehaviour
{
    GameObject parent;
    Rigidbody2D rb;

    public float followScale = 1.5f;

    public float maxSpeed = 15f;
    public float moveSpeed = 5f;
    
    void Start()
    {
	parent = GameObject.FindGameObjectWithTag("Player");
	rb = this.GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
	Vector2 dir = (parent.transform.localPosition - this.transform.localPosition).normalized * moveSpeed;
	this.rb.AddForce(dir);

	rb.velocity = Vector2.ClampMagnitude(rb.velocity, maxSpeed);
    }
}
