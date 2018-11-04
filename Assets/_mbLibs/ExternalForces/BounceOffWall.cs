using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(BoxCollider2D))]
public class BounceOffWall : MonoBehaviour {

	public float forceMagnitude = 3f;
	public float inactiveTime = 2f;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	/// <summary>
	/// Sent when another object enters a trigger collider attached to this
	/// object (2D physics only).
	/// </summary>
	/// <param name="other">The other Collider2D involved in this collision.</param>
	void OnCollisionEnter2D(Collision2D other)
	{
		
		var t = other.collider.GetComponent<ExtForces>();
		Debug.Log("Getting collision");
		if(t!=null){
			Vector2 temp = new Vector2(transform.position.x, transform.position.y);
			Vector2 p2 = temp - other.GetContact(0).point;			
			Vector2 vel = p2.normalized * forceMagnitude;
			Debug.Log("Doing bouncy:" + p2 + " -- " + vel + " name:"+ other.gameObject.name);
			t.SetExternalForce(vel, inactiveTime);
		}
		
	}
}

