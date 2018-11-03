using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using marmaladebacon.movement2d;

[RequireComponent(typeof(Flocking))]
[RequireComponent(typeof(TileInfluence))]
[RequireComponent(typeof(SteeringBasics2D))]
public class FlockAndTileUnit1 : MonoBehaviour {

	/*
	public float defaultMaxVelocity = 3.5f;
	public float defaultMaxAcceleration = 10f;
	public float onTileMaxVelocity = 5f;
	public float onTileMaxAcceleration = 30f;
	*/
	
	public float flockingWeight = 0.5f;
	public float tileInfluenceWeight = 0.5f;
	private TileInfluence tileInfluence;
	private Flocking flocking;
	private NearSensor nearSensor;
	private SteeringBasics2D steeringBasics2D;
	// Use this for initialization
	void Start () {
		tileInfluence = GetComponent<TileInfluence>();
		flocking = GetComponent<Flocking>();
		nearSensor = transform.Find("Sensor").GetComponent<NearSensor>();
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
		Vector2 accel = Vector2.zero;
		Vector2 flockAccel = flocking.getSteering(nearSensor.targets) * flockingWeight;
		Vector2 tileAccel = tileInfluence.getSteering() * tileInfluenceWeight;
		/*
		if(tileAccel == Vector2.zero){
			Debug.Log("Setting NO TILE");
			steeringBasics2D.maxAcceleration = this.defaultMaxAcceleration;
			steeringBasics2D.maxVelocity = this.defaultMaxVelocity;
		}else{
			Debug.Log("Setting OnTile");
			steeringBasics2D.maxAcceleration = this.onTileMaxAcceleration;
			steeringBasics2D.maxVelocity = this.onTileMaxVelocity;
		}
		*/
		accel += flockAccel;		
		steeringBasics2D.steer(accel);
    steeringBasics2D.lookWhereYouAreGoing();
		steeringBasics2D.externalForce(tileAccel);
	}
}
