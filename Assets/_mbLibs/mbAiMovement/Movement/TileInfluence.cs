using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileInfluence : MonoBehaviour {
	public float currRate = 0.2f;
	public float maxRate = 1f;
	MoveTile currMoveTile;	

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	/// <summary>
	/// This function is called every fixed framerate frame, if the MonoBehaviour is enabled.
	/// </summary>
	void FixedUpdate()
	{
		if(currMoveTile!=null){
			currRate += currMoveTile.effectIncreaseRate * Time.deltaTime;
			currRate = currRate > 1f ? 1f : currRate;
		}
	}

	public Vector2 getSteering(){
		if(currMoveTile!=null){
			return currMoveTile.getMoveInfluence() * currRate;
		}
		return Vector2.zero;		
	}

	public void SetMoveTile(MoveTile m){
		if(currMoveTile != m){			
			currMoveTile = m;
			currRate = m.startRate;
		}
	}

	public void RemoveMoveTile(MoveTile m){
		if(currMoveTile == m){
			currMoveTile = null;
			currRate = 0f;
		}
	}
}
