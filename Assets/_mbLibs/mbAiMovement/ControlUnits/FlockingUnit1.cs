using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using marmaladebacon.movement2d;
[RequireComponent(typeof(Flocking))]
[RequireComponent(typeof(SteeringBasics2D))]
public class FlockingUnit1 : MonoBehaviour {
	private Flocking flocking;
	private NearSensor nearSensor;
	private SteeringBasics2D steeringBasics2D;
	// Use this for initialization
	void Start () {
		flocking = GetComponent<Flocking>();
		steeringBasics2D = GetComponent<SteeringBasics2D>();
		nearSensor = transform.Find("Sensor").GetComponent<NearSensor>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	
	/// This function is called every fixed framerate frame, if the MonoBehaviour is enabled.
	/// </summary>
	void FixedUpdate()
	{
		Vector2 accel = flocking.getSteering(nearSensor.targets);
		steeringBasics2D.steer(accel);
    steeringBasics2D.lookWhereYouAreGoing();
	}
}
