using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class CharacterMove : MonoBehaviour {
	
	public static bool facingRight;
	[HideInInspector] public bool jump = false;
	public float movex;
	public float maxSpeed;
	public float jumpForce;
	public static float player1Charge;
	public int player1Selection;

	public Slider energy1;
	
	public Transform groundCheck;
	
	
	private bool grounded = true;
	private bool doubleJump = false;
	private bool infinijump = false;
	private Rigidbody2D rb2d;

	public float jumpCost;
	public float boostCost;
	public float blockCost;

	public PhysicsMaterial2D phMt;

	public bool shield;
	
	public GameObject shieldIcon;
	public GameObject floorProp;
	public GameObject pum;
	public GameObject pum2;

	public static bool dieded1;

	private Animator anim;

	public bool pause;

	public bool ultraPause;

	public bool isHitting;

	public static int hitforce1;
	private int hitforce2;

//-----------------------------------------------------------------------

	void Awake () 
	{
		ultraPause = true;

		movex = 0;

		isHitting = false;

		StartCoroutine (Buffer ());

		facingRight = false;
/**
		Vector3 theScale = transform.localScale;
		if (player1Selection == 3) {
			theScale.x *= -1;
		} else {
			if (theScale.x > 0) {
				theScale.x *= -1;
			}
		}
		transform.localScale = theScale;
**/

		movex = 0f;

		dieded1 = false;

		rb2d = GetComponent<Rigidbody2D> ();
		energy1 = GameObject.Find ("Slider1").GetComponent<Slider> ();
		anim = GetComponent<Animator> ();

		player1Charge = 100f;
		energy1.value = 100f;

		player1Selection = SelectionScrip.GetPl1Selection ();

		if (player1Selection == 1) {
			infinijump = true;
		}

		if (player1Selection == 2) {
			boostCost = 15f;
		} else if (player1Selection == 6) {
			boostCost = 50f;
		} else {
			boostCost = 20f;
		} 
		if (player1Selection == 1){
			jumpCost = 5f;
		} else{
			jumpCost = 10f;
		} 

		if (player1Selection == 3) {
			rb2d.mass = 35;
			phMt.bounciness = 0.80f;
			jumpForce = 8000f;
			rb2d.gravityScale = 1.0f;
			phMt.friction = 0.01f;
			maxSpeed = 80000f;
		} else if (player1Selection == 4) {
			rb2d.gravityScale = 0.3f;
			phMt.friction = 0.1f;
			phMt.bounciness = 0.6f;
			jumpForce = 4000f;
			maxSpeed = 50000f;
		} else if (player1Selection == 5) {
			jumpForce = 8000f;
			phMt.friction = 1f;
			phMt.bounciness = 0.6f;
			maxSpeed = 50000f;
		} else if (player1Selection == 6) {
			rb2d.mass = 5;
			jumpForce = 2000f;
			phMt.friction = 0.1f;
			phMt.bounciness = 0.3f;
			maxSpeed = 10000f;
		} else {
			phMt.friction = 0.1f;
			phMt.bounciness = 0.6f;
			jumpForce = 6000f;
			maxSpeed = 50000f;
		}

		if (player1Selection == 4) {
			Instantiate (shieldIcon, new Vector2(7.6f, 3.2f), Quaternion.identity);
			shield = true;
		} else {
			shield = false;
		}

		if (player1Selection == 1) {
			hitforce1 = 20000;
		} else if (player1Selection == 2) {
			hitforce1 = 8000;
		} else if (player1Selection == 3) {
			hitforce1 = 15000;
		} else if (player1Selection == 5) {
			hitforce1 = 30000;
		} else {
			hitforce1 = 10000;
		}

		blockCost = 50f;

		ultraPause = false;
	}


//------------------------------------------------------------------------------------


	void Update () 
	{


		if (pause == true) {
			anim.SetBool ("Block", true);
			rb2d.isKinematic = true;
		} else {
			anim.SetBool ("Block", false);
			rb2d.isKinematic = false;
		}

		if (Input.GetButtonDown("Jump") && grounded && player1Charge >= jumpCost && Pauser.pause == false && pause == false)
		{
			grounded = false;
			doubleJump = true;
			rb2d.AddForce(new Vector2(0f, jumpForce));
			player1Charge = player1Charge - jumpCost;
			energy1.value -= jumpCost;
			anim.SetTrigger ("IsJumping");

		} else if (Input.GetButtonDown("Jump") && !grounded && doubleJump == true && player1Charge >= jumpCost && Pauser.pause == false && pause == false){
			if (!infinijump){
			doubleJump = false;
			}
			anim.SetTrigger ("DoubleJump");
			rb2d.AddForce(new Vector2(0f, jumpForce / 2));
			player1Charge = player1Charge - jumpCost;
			energy1.value -= jumpCost;
		}

		if (Input.GetButtonDown("Fire1") && player1Charge >= boostCost && Pauser.pause == false && pause == false){

			player1Charge = player1Charge - boostCost;
			energy1.value -= boostCost;
			anim.SetTrigger("Dash");
			//StartCoroutine (HitPause());
			if (facingRight == true){
				if (player1Selection == 6) {
					Instantiate (floorProp, new Vector2 (transform.position.x + 1, transform.position.y), Quaternion.identity);
				}
			} else {
				if (player1Selection == 6) {
					Instantiate (floorProp, new Vector2 (transform.position.x - 1, transform.position.y), Quaternion.identity);
				}
			}
			if (facingRight == true){
				if (player1Selection == 2) {
					rb2d.AddForce(new Vector2(jumpForce * 2, 0f));
				}
			} else {
				if (player1Selection == 2) {
					rb2d.AddForce(new Vector2(-jumpForce * 2, 0f));
				}
			}
		}

		if (player1Charge < 100) {
			player1Charge = player1Charge + 0.25f;
			energy1.value += 0.25f;
		}

		if (Input.GetButtonDown ("Block1") && player1Charge >= blockCost && Pauser.pause == false) {
			player1Charge = player1Charge - blockCost;
			energy1.value -= blockCost;
			StartCoroutine (WaitBool(1));
		}

	}


//------------------------------------------------------------------------------


	void FixedUpdate()
	{
		if (ultraPause == false && pause == false) {
			movex = Input.GetAxis ("Horizontal");
		}

		if (ultraPause == false) {
			if (movex > 0 && !facingRight) 
				Flip ();
			else if (movex < 0 && facingRight)
				Flip ();
		}

		if (isHitting == false) {
			if (Input.GetAxis ("Horizontal") != 0 && anim.GetBool ("Block") == false) {
				anim.SetBool ("Walking", true);
			} else {
				anim.SetBool ("Walking", false);
			}
		}

		rb2d.AddForce (Vector2.right * movex * maxSpeed); 
	}


//-------------------------------------------------------------------------------	


	void Flip()
	{
		if (ultraPause == false) {
			facingRight = !facingRight;
			Vector3 theScale = transform.localScale;
			theScale.x *= -1;
			transform.localScale = theScale;
		}
	}

	void OnCollisionEnter2D (Collision2D col)
	{
		bool temp;
		hitforce2 = CharacterMove2.GetHit2 ();
		temp = CharacterMove2.GetFacingRight ();

		if (col.gameObject.tag == "Hitter2") {
			if (temp == true){
				rb2d.AddForce(new Vector2(hitforce2, 0f));
			} else if (temp == false){
				rb2d.AddForce(new Vector2(-hitforce2, 0f));
			}
		}

		if(col.gameObject.tag == "Ground")
		{
			grounded = true;
			doubleJump = false;
		}

		if(col.gameObject.tag == "Killer")
		{
			int win2temp;
			bool diededTemp;
			win2temp = Spawner2.GetWin2();
			diededTemp = CharacterMove2.dieded2;

			if (shield == true){
				IconDestroyer.Boom();
				shield = false;
			} else{
				if (win2temp == 2 && diededTemp == false){
					Instantiate (pum2, transform.position, Quaternion.identity);
					dieded1 = true;
					Victor.pl1Win = false;
					if (player1Selection == 6){
						Instantiate (floorProp, transform.position, Quaternion.identity);
						Instantiate (floorProp, new Vector2 (transform.position.x + 1, transform.position.y), Quaternion.identity);
						Instantiate (floorProp, new Vector2 (transform.position.x - 1, transform.position.y), Quaternion.identity);
						Instantiate (floorProp, new Vector2 (transform.position.x, transform.position.y + 1), Quaternion.identity);
						Instantiate (floorProp, new Vector2 (transform.position.x, transform.position.y - 1), Quaternion.identity);
						Instantiate (floorProp, new Vector2 (transform.position.x + 1, transform.position.y - 1), Quaternion.identity);
					}
					transform.position = new Vector2 (100, 100);
					Invoke ("EndGame", 1f);
				} else if ((win2temp < 2 && diededTemp == false)){
					Instantiate (pum2, transform.position, Quaternion.identity);
					dieded1 = true;
					win2temp++;
					Spawner2.SetWin2(win2temp);
					if (player1Selection == 6){
						Instantiate (floorProp, transform.position, Quaternion.identity);
						Instantiate (floorProp, new Vector2 (transform.position.x + 1, transform.position.y), Quaternion.identity);
						Instantiate (floorProp, new Vector2 (transform.position.x - 1, transform.position.y), Quaternion.identity);
						Instantiate (floorProp, new Vector2 (transform.position.x, transform.position.y + 1), Quaternion.identity);
						Instantiate (floorProp, new Vector2 (transform.position.x, transform.position.y - 1), Quaternion.identity);
						Instantiate (floorProp, new Vector2 (transform.position.x + 1, transform.position.y - 1), Quaternion.identity);
					}
					transform.position = new Vector2 (100, 100);
					Invoke ("EndRound", 1f);
				} else {
					Instantiate (pum, transform.position, Quaternion.identity);
				}
			}
		}


	}

	void EndRound()
	{
		facingRight = false;
		ultraPause = true;
		Destroy (gameObject);
		Application.LoadLevel(1);
	}
	
	void EndGame()
	{
		ultraPause = true;
		Application.LoadLevel(2);
		Destroy (gameObject);
	}

	public IEnumerator WaitBool (int delay)
	{
		pause = true;
		yield return new WaitForSeconds (delay);
		pause = false;
	}

	public static bool GetFacingRight ()
	{
		return facingRight;
	}

	public IEnumerator Buffer()
	{
		yield return new WaitForSeconds (0.01f);
	}

	public IEnumerator HitPause ()
	{
		isHitting = true;
		yield return new WaitForSeconds (0.5f);
		isHitting = false;
	}

	public static int GetHit1()
	{
		return hitforce1;
	}


}