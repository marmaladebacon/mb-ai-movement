using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class ExtForces : MonoBehaviour {
	public bool isExternalForcesActive = false;
	public float timeActive;
	Rigidbody2D rb;
	// Use this for initialization
	void Start () {
		rb = GetComponent<Rigidbody2D>();
	}
	
	/// <summary>
	/// Update is called every frame, if the MonoBehaviour is enabled.
	/// </summary>
	void Update()
	{
		if(isExternalForcesActive){
			timeActive -= Time.deltaTime;
			if(timeActive<=0f){
				isExternalForcesActive = false;
			}
		}				
	}

	public Vector2 getSteering(){
		return Vector2.zero;
	}

	public void SetExternalForce(Vector2 immediateVelocity, float time){
		rb.velocity = immediateVelocity;
		this.timeActive = time;
		isExternalForcesActive = true;
	}
}
