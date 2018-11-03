using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class MoveTile : MonoBehaviour {
	public float effectIncreaseRate = 0.2f;
	public float startRate = 0.1f;

	// Use this for initialization
		void OnTriggerEnter2D(Collider2D other) {
			TileInfluence tf = null;
			if(GetTileInfluence(other, out tf)){
				Debug.Log("Setting tile influence");
				tf.SetMoveTile(this);
			}
		}
		
		void OnTriggerExit2D(Collider2D other) {
			TileInfluence tf = null;
			if(GetTileInfluence(other, out tf)){
				tf.RemoveMoveTile(this);
			}
		}

		bool GetTileInfluence(Collider2D other, out TileInfluence t){
			t = other.GetComponent<TileInfluence>();
			if(t!=null){				
				return true;
			}
			return false;
		}

		public abstract Vector2 getMoveInfluence();
}
