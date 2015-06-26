using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class InfoScript : MonoBehaviour {
	
	public Button back;

	
	void Start () {
		back = back.GetComponent<Button> ();
	}
	
	public void Back(){
		Application.LoadLevel (0);
	}
	
}