using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float lifeTime = 5; // Lifetime of bullet in seconds
    
    void Start()
    {

    }

    void Update()
    {
	if(lifeTime <= 0)
	{
	    Object.Destroy(this.gameObject);
	    return;
	}
	else
	{
	    lifeTime -= Time.deltaTime;
	    Debug.Log(lifeTime);
	}
    }
}
