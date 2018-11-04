using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace marmaladebacon.movement2d {
	public class Cohesion : MonoBehaviour {
 		public float facingCosine = 120f;

    private float facingCosineVal;

    private SteeringBasics2D steeringBasics2D;
		// Use this for initialization
		void Start () {
			facingCosineVal = Mathf.Cos(facingCosine * Mathf.Deg2Rad);
			steeringBasics2D = GetComponent<SteeringBasics2D>();
		}
		
		public Vector2 getSteering(ICollection<Rigidbody2D> targets){
			Vector2 centerOfMass = Vector2.zero;
			int count = 0;

			/* Sums up everyone's position who is close enough and in front of the character */
			foreach (Rigidbody2D r in targets)
			{
				if (steeringBasics2D.isFacing(r.position, facingCosineVal))
				{
						centerOfMass += r.position;
						count++;
				}
			}

			if (count == 0)
			{
					return Vector3.zero;
			}
			else
			{
					centerOfMass = centerOfMass / count;

					return steeringBasics2D.arrive(centerOfMass);
			}
		}

		// Update is called once per frame
		void Update () {
			
		}
	}
}