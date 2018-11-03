using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CircleCollider2D))]
public class CircleForceOut : MonoBehaviour {
	bool isActive = false;
	public float circleIncreaseRate = 0.5f;
	public float startRadius = 0.01f;
	public float maxCircleRadius = 3f;
	public float forceMagnitude = 12f;
	public Transform explosionPerimeterSprite;
	CircleCollider2D circleCollider;
	/// <summary>
	/// Awake is called when the script instance is being loaded.
	/// </summary>
	void Awake()
	{
		circleCollider = GetComponent<CircleCollider2D>();	
		circleCollider.radius = startRadius;
		SetScale(circleCollider.radius);
	}

	public void Activate(){
		isActive = true;
	}

	/// <summary>
	/// Sent when another object enters a trigger collider attached to this
	/// object (2D physics only).
	/// </summary>
	/// <param name="other">The other Collider2D involved in this collision.</param>
	void OnTriggerEnter2D(Collider2D other)
	{
		if(!isActive){
			return;
		}
		var ext = other.GetComponent<ExtForces>();
		if(ext!=null){
			Vector3 p3 = other.transform.position - this.transform.position;
			Vector2 p2 = new Vector2(p3.x, p3.y);
			Vector2 vel = p2.normalized * forceMagnitude;
			ext.SetExternalForce(vel, 3f);
		}
	}

	/// <summary>
	/// This function is called every fixed framerate frame, if the MonoBehaviour is enabled.
	/// </summary>
	void FixedUpdate()
	{
		if(!isActive){
			return;
		}
		circleCollider.radius += circleIncreaseRate * Time.deltaTime;
		SetScale(circleCollider.radius);
		if(circleCollider.radius >= maxCircleRadius){
			GameObject.Destroy(this.gameObject);
		}
	}

	private void SetScale(float r){
		var s = explosionPerimeterSprite.localScale;
		s.x = r * 2f;
		s.y = r * 2f;
		explosionPerimeterSprite.localScale = s;
	}
}
