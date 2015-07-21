using UnityEngine;
using System.Collections;

public class Pauser : MonoBehaviour {

	private float count;
	public static bool pause;

	public GameObject ready;
	public GameObject go;

	void Start () {
		StartCoroutine (Buffer ());
		Instantiate (ready, transform.position, Quaternion.identity);
		Time.timeScale = 0;
		count = 0;
		pause = true;
	}
	

	void Update () {
		if (count < 100) {
			count++;
		} else {
			StartAgain();
		}
	}

	void StartAgain ()
	{
		Instantiate (go, transform.position, Quaternion.identity);
		Time.timeScale = 1;
		pause = false;
		Destroy (gameObject);
	}

	public IEnumerator Buffer()
	{
		yield return new WaitForSeconds (0.01f);
	}
}
