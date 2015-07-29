using UnityEngine;
using System.Collections;

public class Music : MonoBehaviour {
	
	public bool bumaus;

	void Start () {
		bumaus = MenuScript.GetBum ();
		if (bumaus == true) {
			Destroy(gameObject);
		}
	}
	

	void Update () {
		MenuScript.SetBum(true);
		DontDestroyOnLoad (transform.gameObject);
		if (Application.loadedLevel == 1 || Application.loadedLevel == 5) {
			Destroy(gameObject);
		}
	}
}
