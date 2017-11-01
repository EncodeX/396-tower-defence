using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class NormalEnemy : MonoBehaviour
{
	private int healthpoints = 100;
	private Vector3 speed = new Vector3(-0.1f, -0.1f, 0);
	// Use this for initialization
	void Start()
	{
		healthpoints = 100;
	}

	// Update is called once per frame
	void Update()
	{
        this.transform.position = this.transform.position + speed * Time.deltaTime;
	}

	internal void OnCollisionEnter2D(Collision2D other)
	{
        if (other.gameObject.name.Contains("base"))
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

