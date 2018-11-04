using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace marmaladebacon.movement2d {
	public class NearSensor : MonoBehaviour {

		public HashSet<Rigidbody2D> targets = new HashSet<Rigidbody2D>();

		void OnTriggerEnter2D(Collider2D other) {
			targets.Add (other.GetComponent<Rigidbody2D>());
		}
		
		void OnTriggerExit2D(Collider2D other) {
			targets.Remove (other.GetComponent<Rigidbody2D>());
		}
	}
}
