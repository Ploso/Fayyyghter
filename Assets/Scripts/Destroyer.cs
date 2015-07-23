using UnityEngine;
using System.Collections;

public class Destroyer : MonoBehaviour {

	private int count;
	private Rigidbody2D rb2d;
	private int direction;
	public GameObject pum;

	void Start () {
		if (SelectionScrip.player1Selection == 6) {
			if (CharacterMove.facingRight == true){
				direction = 1;
			} else if (CharacterMove.facingRight == false){
				direction = -1;
			}
		} else if (SelectionScrip.player2Selection == 6) {
			if (CharacterMove2.facingRight == true){
				direction = 1;
			} else if (CharacterMove2.facingRight == false){
				direction = -1;
			}
		}

		rb2d = GetComponent<Rigidbody2D>();
		count = 0;
		if (gameObject.name.Contains ("FloorProp")){
			rb2d.AddForce(new Vector2(direction * 50000f, 0f));
		}
	}
	

	void Update () {
		count++;
		if (count > 99) {
			count = 0;
			Destroy (gameObject);

		}
	}

	void OnCollisionEnter2D (Collision2D col)
	{
		if (gameObject.name.Contains ("FloorProp")) {
			Instantiate (pum, transform.position, Quaternion.identity);
		}
	}
}
