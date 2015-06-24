using UnityEngine;
using System.Collections;

public class IconDestroyer : MonoBehaviour {

	public static bool bam;

	void Start () {
		bam = false;
	}
	

	void Update () {
		if (bam == true) {
			Destroy (gameObject);
		}
	}

	public static void Boom()
	{
		bam = true;
	}
}
