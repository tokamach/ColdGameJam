using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public GameObject childPrefab;

    public float maxSpeed = 25;
    public float moveSpeed = 10;

    private Rigidbody2D rb;

    private int splitCount = 1;

    private Dictionary<KeyCode, Vector2> moveMap = new Dictionary<KeyCode, Vector2>{
	{KeyCode.W, new Vector2( 0,  1)},
	{KeyCode.A, new Vector2(-1,  0)},
	{KeyCode.S, new Vector2( 0, -1)},
	{KeyCode.D, new Vector2( 1,  0)}};
    private Dictionary<KeyCode, bool> pressMap = new Dictionary<KeyCode, bool>{
	{KeyCode.W, false},
	{KeyCode.A, false},
	{KeyCode.S, false},
	{KeyCode.D, false}};
    
    void Start()
    {
        rb = this.GetComponent<Rigidbody2D>();
    }

    private void CreateChild()
    {
	// Instantiate new virus prefab as child
	GameObject newChild = Instantiate(childPrefab, this.transform);
	Vector3 newScale = this.transform.localScale * Mathf.Pow(0.75f, splitCount);

	foreach(Transform child in transform)
	    child.transform.localScale = newScale;

	//newChild.GetComponent<Collider2D>().
	newChild.GetComponent<Rigidbody2D>().AddForce(new Vector2(Random.Range(0.1f, 1f), Random.Range(0.1f, 1f)));

	splitCount++;
	// TODO: reduce health on split
    }

    void Update()
    {
        //Split children
	if(Input.GetKeyDown(KeyCode.E))
	{
	    CreateChild();	    
	}

	// Use moveMap to map wasd to vec2's
	foreach(var pair in moveMap)
	{
	    if(Input.GetKey(pair.Key))
	    {
		rb.AddForce(pair.Value * moveSpeed);
	    }
	}

	if(rb.velocity.magnitude > maxSpeed)
	    rb.velocity = rb.velocity.normalized * maxSpeed;
    }
}
