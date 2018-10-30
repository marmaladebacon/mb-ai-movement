using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class SteeringBasics2D : MonoBehaviour {

	public float maxVelocity = 3.5f;
	public float maxAcceleration = 10f;
	public float targetRadius = 0.0005f;
	public float slowRadius = 1f;
	//The time in which we want to achieve the targetSpeed 
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
	public Vector2 GetTransformV2(){
		return GetTransformV2(this.transform);
	}
	public Vector2 GetTransformV2(Transform t){
		return new Vector2(t.position.x, t.position.y);
	}
	// Note: A seek steering behavior. Wiill return the steering for the current game object to seek a given position
	public Vector2 seek(Vector2 targetPosition, float maxSeekAccel){
		Vector2 transformPos = GetTransformV2();
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

	public Vector2 arrive(Vector2 targetPosition){
		//Note: Get the right direction for the linear acceleration
		Vector2 targetVelocity = targetPosition - GetTransformV2();

		//Note:Get the distance to the target
		float dist = targetVelocity.magnitude;

		//Note:if we are within stopping radius stop
		if(dist < targetRadius){
			rb.velocity = Vector2.zero;
			return Vector2.zero;
		}

		//Note:Calculate the target speed, full speed at slowRadius and 0 speed at 0 distance
		float targetSpeed;
		if(dist > slowRadius){
			targetSpeed = maxVelocity;
		} else {
			targetSpeed = maxVelocity * (dist/slowRadius);
		}

		//Note: Give target velocity the correct speed
		targetVelocity.Normalize();
		targetVelocity *= targetSpeed;

		//Calculate the linear acceleration we want
		Vector2 acceleration = targetVelocity - rb.velocity;

		/*
		 Rather than accelerate the character to the correct speed in 1 second, 
		 accelerate so we reach the desired speed in timeToTarget seconds 
		 (if we were to actually accelerate for the full timeToTarget seconds).
		*/
		acceleration *= 1/timeToTarget;

		//Make sure we are accelerating at max acceleration
		if(acceleration.magnitude > maxAcceleration){
			acceleration.Normalize();
			acceleration *= maxAcceleration;
		}

		return acceleration;
	}

	public Vector2 interpose(Rigidbody2D target1, Rigidbody2D target2){
		Vector2 midPoint = (target1.position + target2.position) / 2f;
		float timeToReachMidPoint = Vector2.Distance(midPoint, GetTransformV2()) / maxVelocity; 
		Vector2 futureTarget1Pos = target1.position + target1.velocity * timeToReachMidPoint;
		Vector2 futureTarget2Pos = target2.position + target2.velocity * timeToReachMidPoint;

		midPoint = (futureTarget1Pos + futureTarget2Pos) / 2;

		return arrive(midPoint);
	}

	/* Checks to see if the target is in front of the character */
	public bool isInFront(Vector2 targetPos)
	{
			return isFacing(targetPos, 0);
	}

	//cosineValue is in radians, also \|/  if the middle line is the direction of facing, the cosineVal is just one side eg \|
	public bool isFacing(Vector2 targetPos, float cosineValue) { 
			float degree = rb.rotation;
			Vector2 facing = new Vector2(Mathf.Cos(degree * Mathf.Deg2Rad), Mathf.Sin(degree * Mathf.Deg2Rad));
			
			Vector2 directionToTarget = (targetPos - GetTransformV2());
			
			directionToTarget.Normalize();
			float dot = Vector2.Dot(facing, directionToTarget);
			//Debug.Log(facing +" vs " + directionToTarget + " = " + dot + " to " + cosineValue);

			return  dot >= cosineValue;
	}

	public static float getBoundingRadius(Transform t)
	{
			CircleCollider2D col = t.GetComponent<CircleCollider2D>();
			return Mathf.Max(t.localScale.x, t.localScale.y, t.localScale.z) * col.radius;
	}
	// Update is called once per frame
	void Update () {
		
	}
}
