using UnityEngine;
using System.Collections;

public class Hitboxes : MonoBehaviour {
		
	public static BoxCollider2D hitbox;

	void Start () {
		hitbox = gameObject.GetComponent<BoxCollider2D> ();
	}
	

	void Update () {
	
	}

	public static void SwitchCollider ()
	{
		if (hitbox.enabled == false) {
			hitbox.enabled = true;
		} else {
			hitbox.enabled = false;
		}
	}
}
