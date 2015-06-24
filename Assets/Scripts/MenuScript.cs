using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class MenuScript : MonoBehaviour {

	public Button startGame;

	void Start () {
		startGame = startGame.GetComponent<Button> ();
	}
	

	void Update () {
	
	}

	public void StartGame(){
		Application.LoadLevel (3);
	}

}
