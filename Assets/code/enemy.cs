using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class enemy : MonoBehaviour
{
	private int healthpoints = 100;
	private vector3 speed = new vector3(0.5, 0.5, 0);
	// Use this for initialization
	void Start()
	{
		healthpoints = 100;
	}

	// Update is called once per frame
	void Update()
	{
		this.transform.position = this.transform.position + speed * time.delta;
	}

	internal void OnCollisionEnter2D(Collision2D other)
	{
		if (other.gameObject.name.contains("base"))
		{
			//other.gameObject.hitBase(healthpoints);
			healthpoints = 0;
		}
		Die();
	}

	private void Die()
	{
		Destroy(gameObject);
	}


}

