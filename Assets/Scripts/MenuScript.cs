using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class MenuScript : MonoBehaviour {

	public Button startGame;
	public Button quitGame;
	public Button info;
	public static bool bumaus = false;

	void Start () {
		startGame = startGame.GetComponent<Button> ();
		quitGame = quitGame.GetComponent<Button> ();
		info = info.GetComponent<Button> ();
	}
	

	void Update () {
	
	}

	public void StartGame(){
		Application.LoadLevel (3);
	}

	public void QuitGame(){
		Application.Quit ();

	}

	public void InfoScreen()
	{
		Application.LoadLevel (4);
		
	}

	public static bool GetBum ()
	{
		return bumaus;
	}

	public static void SetBum(bool temp)
	{
		bumaus = temp;
	}

}
