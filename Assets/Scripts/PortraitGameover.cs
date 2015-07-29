using UnityEngine;
using System.Collections;

public class PortraitGameover : MonoBehaviour {

	public GameObject[] obj;

	public int character;

	void Start () {
		if (Victor.pl1Win == true) {
			character = SelectionScrip.GetPl1Selection();
		} else {
			character = SelectionScrip.GetPl2Selection();
		}

		Instantiate (obj [character], transform.position, Quaternion.identity);
	}
	

	void Update () {
	
	}
}
