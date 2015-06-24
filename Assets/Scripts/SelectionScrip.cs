using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class SelectionScrip : MonoBehaviour {

	public Button hero1;
	public Button hero2;
	public Button hero3;
	public Button hero4;
	public Button hero5;
	public Button hero6;

	public Text selectingPlayer;

	public bool hero1Selected;
	public bool hero2Selected;
	public bool hero3Selected;
	public bool hero4Selected;
	public bool hero5Selected;
	public bool hero6Selected;

	public static int player1Selection;
	public static int player2Selection;

	private bool player1Selects = true;

	void Start () {
		hero1Selected = false;
		hero2Selected = false;
		hero3Selected = false;
		hero4Selected = false;
		hero5Selected = false;
		hero6Selected = false;
	}
	

	void Update () {

		if (hero1Selected == true) {
			hero1.interactable = false;
		} else if (hero1Selected == false) {
			hero1.interactable = true;
		}
		if (hero2Selected == true) {
			hero2.interactable = false;
		} else if (hero2Selected == false) {
			hero2.interactable = true;
		}
		if (hero3Selected == true) {
			hero3.interactable = false;
		} else if (hero3Selected == false) {
			hero3.interactable = true;
		}
		if (hero4Selected == true) {
			hero4.interactable = false;
		} else if (hero4Selected == false) {
			hero4.interactable = true;
		}
		if (hero5Selected == true) {
			hero5.interactable = false;
		} else if (hero5Selected == false) {
			hero5.interactable = true;
		}
		if (hero6Selected == true) {
			hero6.interactable = false;
		} else if (hero6Selected == false) {
			hero6.interactable = true;
		}

		if (player1Selects == true) {
			selectingPlayer.text = "Player 1, select your hero";
		} else if (player1Selects == false) {
			selectingPlayer.text = "Player 2, select your hero";
		}

	}

	public void SelectHero1 ()
	{
		if (player1Selects == true) {
			hero1Selected = true;
			player1Selection = 1;
			player1Selects = false;
		} else if (player1Selects == false) {
			hero1Selected = true;
			player2Selection = 1;
			Application.LoadLevel(1);
		}
	}

	public void SelectHero2 ()
	{
		if (player1Selects == true) {
			hero2Selected = true;
			player1Selection = 2;
			player1Selects = false;
		} else if (player1Selects == false) {
			hero2Selected = true;
			player2Selection = 2;
			Application.LoadLevel(1);
		}
	}

	public void SelectHero3 ()
	{
		if (player1Selects == true) {
			hero3Selected = true;
			player1Selection = 3;
			player1Selects = false;
		} else if (player1Selects == false) {
			hero3Selected = true;
			player2Selection = 3;
			Application.LoadLevel(1);
		}
	}

	public void SelectHero4 ()
	{
		if (player1Selects == true) {
			hero4Selected = true;
			player1Selection = 4;
			player1Selects = false;
		} else if (player1Selects == false) {
			hero4Selected = true;
			player2Selection = 4;
			Application.LoadLevel(1);
		}
	}

	public void SelectHero5 ()
	{
		if (player1Selects == true) {
			hero5Selected = true;
			player1Selection = 5;
			player1Selects = false;
		} else if (player1Selects == false) {
			hero5Selected = true;
			player2Selection = 5;
			Application.LoadLevel(1);
		}
	}

	public void SelectHero6 ()
	{
		if (player1Selects == true) {
			hero6Selected = true;
			player1Selection = 6;
			player1Selects = false;
		} else if (player1Selects == false) {
			hero6Selected = true;
			player2Selection = 6;
			Application.LoadLevel(1);
		}
	}

	public static int GetPl1Selection()
	{
		return player1Selection;
	}
	public static int GetPl2Selection()
	{
		return player2Selection;
	}
}
