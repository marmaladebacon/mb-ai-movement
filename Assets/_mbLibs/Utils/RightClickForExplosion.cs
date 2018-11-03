using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RightClickForExplosion : MonoBehaviour {
	public Transform Explosion;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetMouseButtonDown(1)){
			var worldPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
			var t = Instantiate<Transform>(Explosion);
			t.position = new Vector3(worldPoint.x, worldPoint.y, 0f);
			t.GetComponent<CircleForceOut>().Activate();
		}
	}
}
