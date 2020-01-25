using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public GameObject childPrefab; // Prefab to create new instances of on split
    public float maxSpeed = 25;    // Ceiling for movement speed
    public float moveSpeed = 10;   // Speed multiplier per frame

    private Rigidbody2D rb;     // Reference to GameObjects rigidbody component
    private int splitCount = 0; // Count times the virus has split

    /* A dictionary mapping input keys to movements */
    private Dictionary<KeyCode, Vector2> moveMap = new Dictionary<KeyCode, Vector2>{
	{KeyCode.W, new Vector2( 0,  1)},
	{KeyCode.A, new Vector2(-1,  0)},
	{KeyCode.S, new Vector2( 0, -1)},
	{KeyCode.D, new Vector2( 1,  0)}};


    void Start()
    {
        rb = this.GetComponent<Rigidbody2D>();
    }

    private void CreateChild(int n)
    {
	// Instantiate new virus prefab as child
	//create n new children
	for(int i = 0; i < Mathf.Pow(2, n); i++)
	{
	    GameObject newChild = Instantiate(childPrefab, this.transform.localPosition, this.transform.localRotation); //, this.transform);
	}

	Vector3 newScale = this.transform.localScale * Mathf.Pow(0.75f, splitCount);

	foreach(GameObject child in GameObject.FindGameObjectsWithTag("ChildVirus"))
	{
	    child.transform.localScale = newScale;
	    child.GetComponent<Rigidbody2D>().AddForce(new Vector2(Random.Range(0.1f, 1f), Random.Range(0.1f, 1f)));
	}
	    
	splitCount++;
	// TODO: reduce health on split
    }

    /*
     * Remove half of children, double current scale size
     */
    private void MergeChild()
    {
	// Can't split if there's only one of us
	if(splitCount == 0)
	    return;

	// Pick n children and kill them
	int killed = 0;
	foreach(GameObject child in GameObject.FindGameObjectsWithTag("ChildVirus"))
	{
		if(killed >= (Mathf.Pow(2, splitCount) / 2))
		    break;
		else
		    Destroy(child);
		killed++;
	}
	
	Vector3 newScale = this.transform.localScale * Mathf.Pow(0.75f, splitCount - 1);

	foreach(GameObject child in GameObject.FindGameObjectsWithTag("ChildVirus"))
	    child.transform.localScale = newScale;

	splitCount--;
    }

    void Update()
    {
        //Split children
	if(Input.GetKeyDown(KeyCode.E))
	    CreateChild(splitCount);	    

	if(Input.GetKeyDown(KeyCode.Q))
	    MergeChild();

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
