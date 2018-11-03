using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace marmaladebacon.movement2d{

	public class DirectionToggle : MonoBehaviour {

		private DirectionalTile directionalTile;
		// Use this for initialization
		void Start () {
			directionalTile = this.GetComponent<DirectionalTile>();
		}
		
		// Update is called once per frame
		void Update () {
			if(Input.GetMouseButtonDown(0)){
				ToggleOppositeDirection();
			}
		}

		public void ToggleOppositeDirection(){
			directionalTile.direction = new Vector2(directionalTile.direction.x * -1f, directionalTile.direction.y * -1f);
			directionalTile.FlipSpriteDirection();
		}
	}
}