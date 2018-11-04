using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace marmaladebacon.movement2d{
	[RequireComponent(typeof(SteeringBasics2D))]
	public class WallAvoidance : MonoBehaviour {
		// Since we're 2d, we need to avoid the ground tiles and sensors 
		public LayerMask layerMaskToUse;
		/* How far ahead the ray should extend */
    public float mainWhiskerLen = 3f;

    /* The distance away from the collision that we wish go */
    public float wallAvoidDistance = 2f;

    public float sideWhiskerLen = 2f;

    public float sideWhiskerAngle = 45f;

    public float maxAcceleration = 40f;

    private Rigidbody2D rb;
    private SteeringBasics2D steeringBasics2D;
		// Use this for initialization
		void Start () {
			rb = GetComponent<Rigidbody2D>();
			steeringBasics2D = GetComponent<SteeringBasics2D>();

		}
		
		// Update is called once per frame
		void Update () {
			
		}

		public Vector2 getSteering()
    {
        return getSteering(rb.velocity);
    }

    public Vector2 getSteering(Vector2 facingDir)
    {
			Vector2 acceleration = Vector3.zero;

        /* Creates the ray direction vector */
			Vector2[] rayDirs = new Vector2[3];
			rayDirs[0] = facingDir.normalized;

			float orientation = Mathf.Atan2(rb.velocity.y, rb.velocity.x);

			rayDirs[1] = orientationToVector(orientation + sideWhiskerAngle * Mathf.Deg2Rad);
			rayDirs[2] = orientationToVector(orientation - sideWhiskerAngle * Mathf.Deg2Rad);

			RaycastHit2D hit;

        /* If no collision do nothing */
			if (!findObstacle(rayDirs, out hit))
			{
				return acceleration;
			}

			/* Create a target away from the wall to seek */
			Vector2 targetPos = hit.point + hit.normal * wallAvoidDistance;

			/* If velocity and the collision normal are parallel then move the target a bit to
				the left or right of the normal */
			Vector3 cross = Vector3.Cross(rb.velocity, hit.normal);
			if (cross.magnitude < 0.005f)
			{
					targetPos = targetPos + new Vector2(-hit.normal.y, hit.normal.x);
			}

			return steeringBasics2D.seek(targetPos, maxAcceleration);
    }

    /* Returns the orientation as a unit vector */
    private Vector2 orientationToVector(float orientation)
    {
        return new Vector2(Mathf.Cos(orientation), Mathf.Sin(orientation)).normalized;
    }

    private bool findObstacle(Vector2[] rayDirs, out RaycastHit2D firstHit)
    {
        firstHit = new RaycastHit2D();
        bool foundObs = false;

        for (int i = 0; i < rayDirs.Length; i++)
        {
            float rayDist = (i == 0) ? mainWhiskerLen : sideWhiskerLen;
            Vector2 rCurr = rayDirs[i] * rayDist;

						RaycastHit2D hit = Physics2D.Raycast(SteeringBasics2D.GetTransformV2(transform), rayDirs[i], rayDist, layerMaskToUse);
						Debug.DrawLine(this.transform.position, new Vector3(this.transform.position.x + rCurr.x, 
							this.transform.position.y + rCurr.y, 0f), Color.red);
            if (hit.collider!=null)
            {
                foundObs = true;
                firstHit = hit;
                break;
            }

            //Debug.DrawLine(transform.position, transform.position + rayDirs[i] * rayDist);
        }

        return foundObs;
    }
	}
}