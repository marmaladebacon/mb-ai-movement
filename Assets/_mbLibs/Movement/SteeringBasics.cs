using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class SteeringBasics : MonoBehaviour {

	public float maxVelocity = 3.5f;
	public float maxAcceleration = 10f;
	public float targetRadius = 0.0005f;
	public float slowRadius = 1f;
	public float timeToTarget = 0.1f;
	public float turnSpeed = 20f;
	private Rigidbody2D rb;

	public bool smoothing = true;
	public int numSamplesForSmoothing = 5;
	private Queue<Vector2> velocitySamples = new Queue<Vector2>();

	// Use this for initialization
	void Start () {
		rb = GetComponent<Rigidbody2D>();	
	}
	
	// Note: Updates the velocity of the current game object by the given linear acceleration
	public void steer(Vector2 linearAcceleration){
		rb.velocity += linearAcceleration * Time.deltaTime;
		if(rb.velocity.magnitude > maxVelocity){
			rb.velocity = rb.velocity.normalized * maxVelocity;
		}
	}
	// Note: A seek steering behavior. Wiill return the steering for the current game object to seek a given position
	public Vector2 seek(Vector2 targetPosition, float maxSeekAccel){
		Vector2 transformPos = new Vector2(transform.position.x, transform.position.y);
		// Note: Get the direction
		Vector2 acceleration = targetPosition - transformPos;
		
		acceleration.Normalize();

		//accelerate to target
		acceleration *= maxSeekAccel;
		return acceleration;
	}

	public Vector2 seek(Vector2 targetPosition){
		return seek(targetPosition, maxAcceleration);
	}

	public void lookWhereYouAreGoing(){
		Vector2 direction = rb.velocity;
		if(smoothing){
			if(velocitySamples.Count == numSamplesForSmoothing){
				velocitySamples.Dequeue();
			}
			velocitySamples.Enqueue(rb.velocity);
			direction = Vector2.zero;
			foreach(Vector2 v in velocitySamples){
				direction+=v;
			}
			direction /= velocitySamples.Count;
		}
		lookAtDirection(direction);
	}

	public void lookAtDirection(Vector2 direction){
		direction.Normalize();

		if(direction.sqrMagnitude > 0.001f){
			float toRotation = (Mathf.Atan2(direction.y, direction.x)*Mathf.Rad2Deg);
			float rotation = Mathf.LerpAngle(transform.rotation.eulerAngles.z, toRotation, Time.deltaTime*turnSpeed);
			transform.rotation = Quaternion.Euler(0,0,rotation);
		}
	}

	public void lookAtDirection(Quaternion toRotation){
		lookAtDirection(toRotation.eulerAngles.z);
	}

	public void lookAtDirection(float toRotation){
		float rotation = Mathf.LerpAngle(transform.rotation.eulerAngles.z, toRotation, Time.deltaTime * turnSpeed);
		transform.rotation = Quaternion.Euler(0,0,rotation);
	}

	// Update is called once per frame
	void Update () {
		
	}
}
