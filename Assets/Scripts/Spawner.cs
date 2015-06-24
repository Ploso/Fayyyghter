using UnityEngine;
using System.Collections;

public class Spawner : MonoBehaviour {

	public int player1HeroSelect;

	public GameObject[] player1Hero;

	void Start () {
		player1HeroSelect = SelectionScrip.GetPl1Selection ();

		Instantiate (player1Hero [player1HeroSelect], transform.position, Quaternion.identity);
	}
	

	void Update () {
	
	}
}
