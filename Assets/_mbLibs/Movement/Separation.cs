using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace marmaladebacon.movement2d {
	public class Separation : MonoBehaviour {

		 /* The maximum acceleration for separation */
    public float sepMaxAcceleration = 25;

    /* This should be the maximum separation distance possible between a separation
     * target and the character.
     * So it should be: separation sensor radius + max target radius */
    public float maxSepDist = 1f;

		private float boundingRadius;
		void Start () {
			boundingRadius = SteeringBasics2D.getBoundingRadius(this.transform);
		}
		
		//This function can return a large value for acceleration. This is intended so that
		//combining with other accelerations, it can make up a large portion of the vector
		//and steering basics normalizes the acceleration anyway
		//ToDo: investigate if we want to normalize this value here or in a wrapper function 
		//so that we can control the degree of separation
		public Vector2 getSteering(ICollection<Rigidbody2D> targets){
			Vector2 acceleration = Vector2.zero;
			foreach(Rigidbody2D r in targets){
			/* Get the direction and distance from the target */
				Vector2 direction = SteeringBasics2D.GetTransformV2(transform) - r.position;
				float dist = direction.magnitude;

				if (dist < maxSepDist)
				{
						float targetRadius = SteeringBasics2D.getBoundingRadius(r.transform);

						/* Calculate the separation strength (can be changed to use inverse square law rather than linear) */
						var strength = sepMaxAcceleration * (maxSepDist - dist) / 
							(maxSepDist - boundingRadius - targetRadius);

						/* Added separation acceleration to the existing steering */
						direction.Normalize();
						acceleration += direction * strength;
				}
			}
			return acceleration;
		}

	}
}
