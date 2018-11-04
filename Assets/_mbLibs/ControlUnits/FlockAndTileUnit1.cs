using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using marmaladebacon.movement2d;

[RequireComponent(typeof(Flocking))]
[RequireComponent(typeof(TileInfluence))]
[RequireComponent(typeof(SteeringBasics2D))]
[RequireComponent(typeof(ExtForces))]
public class FlockAndTileUnit1 : MonoBehaviour {
	
	public float flockingWeight = 0.5f;
	public float tileInfluenceWeight = 0.5f;
	private TileInfluence tileInfluence;
	private Flocking flocking;
	private NearSensor nearSensor;
	private ExtForces extForces;
	private WallAvoidance wallAvoidance;
	private SteeringBasics2D steeringBasics2D;
	// Use this for initialization
	void Start () {
		tileInfluence = GetComponent<TileInfluence>();
		flocking = GetComponent<Flocking>();
		nearSensor = transform.Find("Sensor").GetComponent<NearSensor>();
		extForces = GetComponent<ExtForces>();
		wallAvoidance = GetComponent<WallAvoidance>();
		steeringBasics2D = GetComponent<SteeringBasics2D>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	/// <summary>
	/// This function is called every fixed framerate frame, if the MonoBehaviour is enabled.
	/// </summary>
	void FixedUpdate()
	{
		if(extForces.isExternalForcesActive){
			return;
		}

		Vector2 accel = Vector2.zero;
		Vector2 flockAccel = flocking.getSteering(nearSensor.targets) * flockingWeight;
		Vector2 tileAccel = tileInfluence.getSteering() * tileInfluenceWeight;
		Vector2 avoidWallAccel = wallAvoidance.getSteering();		
		accel += flockAccel;
		accel += avoidWallAccel;
		Debug.Log("Avoid wall accel:" + avoidWallAccel);		
		steeringBasics2D.steer(accel);
    
		steeringBasics2D.groundInfluence(tileAccel);
		steeringBasics2D.lookWhereYouAreGoing();
	}
}
