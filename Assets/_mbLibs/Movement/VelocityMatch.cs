using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace marmaladebacon.movement2d {
	public class VelocityMatch : MonoBehaviour {
		public float facingCosine = 90;
    public float timeToTarget = 0.1f;
    public float maxAcceleration = 4f;

		private float facingCosineVal;

		private Rigidbody2D rb;
		private SteeringBasics2D steeringBasics2D;
		// Use this for initialization
		void Start () {
			facingCosineVal = Mathf.Cos(facingCosine * Mathf.Deg2Rad);
			this.rb = GetComponent<Rigidbody2D>();
			this.steeringBasics2D = GetComponent<SteeringBasics2D>();
		}
		
		public Vector2 getSteering(ICollection<Rigidbody2D> targets){
			Vector2 accel = Vector2.zero;
			int count = 0;
			foreach (Rigidbody2D target in targets)
			{
				if (steeringBasics2D.isFacing(target.position, facingCosineVal))
				{
						/* Calculate the acceleration we want to match this target */
						Vector2 a = target.velocity - rb.velocity;
						/*
							Rather than accelerate the character to the correct speed in 1 second, 
							accelerate so we reach the desired speed in timeToTarget seconds 
							(if we were to actually accelerate for the full timeToTarget seconds).
						*/
						a = a / timeToTarget;
						accel += a;
						count++;
				}
			}

			if (count > 0)
			{
				accel = accel / count;

				/* Make sure we cap at accelerating at max acceleration */
				if (accel.magnitude > maxAcceleration)
				{
					accel = accel.normalized * maxAcceleration;
				}
			}
			return accel;
		}
		// Update is called once per frame
		void Update () {
			
		}
	}
}
