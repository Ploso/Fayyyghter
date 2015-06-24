using UnityEngine;
using System.Collections;

public class Destroyer : MonoBehaviour {

	private int count;

	void Start () {
		count = 0;
	}
	

	void Update () {
		count++;
		if (count > 99) {
			count = 0;
			Destroy (gameObject);

		}
	}
}
