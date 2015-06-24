using UnityEngine;
using System.Collections;

public class Spawner : MonoBehaviour {

	public int player1HeroSelect;

	public GameObject[] player1Hero;

	public static int win1;

	void Start () {
		player1HeroSelect = SelectionScrip.GetPl1Selection ();

		Instantiate (player1Hero [player1HeroSelect], transform.position, Quaternion.identity);
	}
	

	void Update () {
	
	}

	public static int GetWin1 ()
	{
		return win1;
	}

	public static void SetWin1(int tempWin)
	{
		win1 = tempWin;
	}
}
