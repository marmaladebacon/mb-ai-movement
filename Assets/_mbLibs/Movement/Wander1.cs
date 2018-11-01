using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace marmaladebacon.movement2d {
	public class Wander1 : MonoBehaviour {
		//Forward offset of the wander square
		public float wanderOffset = 1.5f;
		//radius of the wander area
		public float wanderRadius = 4;
		//Rate at which the wander orientation changes
		public float wanderRate = 0.4f;
		//rotation in radians
		private float wanderOrientation =0f;
		private SteeringBasics2D steeringBasics; 
		// Use this for initialization
		void Start () {
			steeringBasics = GetComponent<SteeringBasics2D>();
		}

		public Vector3 getSteering(){
			float characterOrientation = transform.rotation.eulerAngles.z * Mathf.Deg2Rad;
			wanderOrientation += randomBinomial() * wanderRate;
			float targetOrientation = wanderOrientation + characterOrientation;

			/* Calculate the center of the wander circle */
			Vector2 targetPosition = steeringBasics.GetTransformV2(this.transform) + (orientationToVector(characterOrientation) * wanderOffset);

			/* Calculate the target position */
			targetPosition = targetPosition + (orientationToVector(targetOrientation) * wanderRadius);
			
			//Debug.DrawLine (transform.position, targetPosition);
			
			return steeringBasics.seek (targetPosition);
		}

		// Returns a random number between -1 to 1. Values around zero are more likely
		float randomBinomial(){
			return Random.value - Random.value;
		}

		Vector2 orientationToVector(float orientation){
			return new Vector2(Mathf.Cos(orientation * Mathf.Deg2Rad), Mathf.Sin(orientation * Mathf.Deg2Rad));
		}
		
		// Update is called once per frame
		void Update () {
			
		}
	}
}
