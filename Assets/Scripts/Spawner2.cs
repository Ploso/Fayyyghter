using UnityEngine;
using System.Collections;

public class Spawner2 : MonoBehaviour {

	public int player2HeroSelect;

	public GameObject[] player2Hero;

	public static int win2;
	
	void Start () {
		player2HeroSelect = SelectionScrip.GetPl2Selection ();
		
		Instantiate (player2Hero [player2HeroSelect], transform.position, Quaternion.identity);
	}
	
	
	void Update () {
		
	}

	public static int GetWin2 ()
	{
		return win2;
	}
	
	public static void SetWin2(int tempWin)
	{
		win2 = tempWin;
	}
}
