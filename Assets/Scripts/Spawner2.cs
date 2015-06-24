using UnityEngine;
using System.Collections;

public class Spawner2 : MonoBehaviour {

	public int player2HeroSelect;

	public GameObject[] player2Hero;
	
	void Start () {
		player2HeroSelect = SelectionScrip.GetPl2Selection ();
		
		Instantiate (player2Hero [player2HeroSelect], transform.position, Quaternion.identity);
	}
	
	
	void Update () {
		
	}
}
