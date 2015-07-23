using UnityEngine;
using System.Collections;

public class Hitboxes : MonoBehaviour {
		
	public GameObject pum;

	void Start () {

	}
	

	void Update () {
	
	}

	void OnCollisionEnter2D (Collision2D col)
	{
		Instantiate (pum, transform.position, Quaternion.identity);
	}
}
