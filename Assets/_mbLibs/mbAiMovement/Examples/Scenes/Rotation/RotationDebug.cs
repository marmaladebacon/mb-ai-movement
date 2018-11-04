using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using marmaladebacon.movement2d;
public class RotationDebug : MonoBehaviour {
	[SerializeField]
	SteeringBasics2D steeringBasics;
	[SerializeField]
	Transform targetToLookAt;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		Debug.Log("facing:"+ steeringBasics.isFacing(SteeringBasics2D.GetTransformV2(targetToLookAt),Mathf.Cos( 45f * Mathf.Deg2Rad)) );
	}
}
