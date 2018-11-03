using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace marmaladebacon.movement2d{

	[RequireComponent(typeof(Cohesion))]
	[RequireComponent(typeof(Separation))]
	[RequireComponent(typeof(VelocityMatch))]
	public class Flocking : MonoBehaviour {
		public float cohesionWeight = 1.5f;
		public float separationWeight = 2f;
		public float velocityMatchWeight = 1f;
		private Wander2 wander;

		private Cohesion cohesion;
		private Separation separation;
		private VelocityMatch velocityMatch;

		// Use this for initialization
		void Start () {
			wander = GetComponent<Wander2>();
			cohesion = GetComponent<Cohesion>();
			separation = GetComponent<Separation>();
			velocityMatch = GetComponent<VelocityMatch>();
		}

		public Vector2 getSteering(ICollection<Rigidbody2D> targets){
			Vector2 accel = Vector2.zero;

			accel += cohesion.getSteering(targets) * cohesionWeight;
			accel += separation.getSteering(targets) * separationWeight;
			accel += velocityMatch.getSteering(targets) * velocityMatchWeight;

			if(accel.magnitude < 0.005f){
				accel = wander.getSteering();
			}

			return accel;
		}
	
	}
}
