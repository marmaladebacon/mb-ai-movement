using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using marmaladebacon.movement2d;
public class WanderUnit1 : MonoBehaviour {
  private SteeringBasics2D steeringBasics;
  private Wander1 wander;

  void Start () {
      steeringBasics = GetComponent<SteeringBasics2D>();
      wander = GetComponent<Wander1>();
	}
	
	// Update is called once per frame
	void Update () {
      Vector2 accel = wander.getSteering();
      Debug.Log("Acceleration:" + accel);
      steeringBasics.steer(accel);
      steeringBasics.lookWhereYouAreGoing();
  }
}