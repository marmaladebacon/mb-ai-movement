using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using marmaladebacon.movement2d;
public class WanderUnit2 : MonoBehaviour {

    private SteeringBasics2D steeringBasics;
    private Wander2 wander;

    // Use this for initialization
    void Start()
    {
			steeringBasics = GetComponent<SteeringBasics2D>();
			wander = GetComponent<Wander2>();
    }

    // Update is called once per frame
    void Update()
    {
			Vector2 accel = wander.getSteering();

			steeringBasics.steer(accel);
			steeringBasics.lookWhereYouAreGoing();
    }
}
