using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Victor : MonoBehaviour {

	public static bool pl1Win;

	public Button menu;
	public Button rematch;

	public Text victor;

	void Start () {

		menu = menu.GetComponent<Button>();
		rematch = rematch.GetComponent<Button> ();

		if (pl1Win == true) {
			victor.text = "Player 1 wins!";
		} else if (pl1Win == false) {
			victor.text = "Player 2 wins!";
		}
	}
	

	void Update () {
		
	}

	public void toMenu()
	{
		Application.LoadLevel (0);
		Spawner.SetWin1(0);
		Spawner2.SetWin2(0);
	}

	public void toReMatch()
	{
		Application.LoadLevel (1);
		Spawner.SetWin1(0);
		Spawner2.SetWin2(0);
	}
}
