using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ScoreScript : MonoBehaviour {

	public Text player1Wins;
	public Text player2Wins;

	private string player1;
	private string player2;


	void Start () {

	}
	

	void Update () {
		player1 = Spawner.win1.ToString ();
		player1Wins.text = player1;

		player2 = Spawner2.win2.ToString ();
		player2Wins.text = player2;

	}
}
