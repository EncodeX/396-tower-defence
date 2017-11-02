using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class NormalEnemy : MonoBehaviour
{
	private float _healthpoints;
    private string _type;

	public void Initialize(Vector3 speed, string type, float healthpoints)
	{
        var rb = GetComponent<Rigidbody>();
        rb.velocity = speed;
        _healthpoints = healthpoints;
        _type = type;
	}


	// Update is called once per frame
	void Update()
	{
		var rb = GetComponent<Rigidbody>();
        this.transform.position = this.transform.position + rb.velocity * Time.deltaTime;
	}

	internal void OnCollisionEnter(Collision other)
	{
        if (other.gameObject.name.Contains("Base"))
		{
            //other.gameObject.hitBase(healthpoints);
            Game.Ctx.BaseHealthBar.PerformDamage(_healthpoints / 10f);
            _healthpoints = 0;
            Die();
		}
		
	}

	private void Die()
	{
		Destroy(gameObject);
	}

	public void PerformDamage(float points)
	{
		_healthpoints -= points;
        if (_healthpoints < 0.01f){
            Die();
        }
	}


}

