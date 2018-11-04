using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DirectionalTile : MoveTile {
	public float magnitude = 1f;
	public Vector2 direction;
	public Transform SpriteTransform;
	void Start(){
		SpriteTransform = transform.Find("Sprite").transform;
	}
	
	public override Vector2 getMoveInfluence(){
		return direction * magnitude;
	}

	public void FlipSpriteDirection(){
		SetSpriteDirection(transform.eulerAngles.z + 180f);
	}
	public void SetSpriteDirection(float degreeDir){
		transform.eulerAngles = new Vector3(0f,0f,degreeDir);
	}
}
