using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class CharacterMove2 : MonoBehaviour {

//-----------------------Variables----------------------

	public static bool facingRight = false;
	[HideInInspector] public bool jump = false;
	public float movex;
	public float maxSpeed;
	public float jumpForce;
	public static float player2Charge;
	public int player2Selection;

	public Slider energy2;
	
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

	public static bool dieded2;

	private Animator anim;

	public bool pause;

	public bool ultraPause;

	public bool isHitting;

	public static int hitforce2;
	private int hitforce1;

	private bool blocker;

//---------------------------Start (awake)------------------------------------

	void Awake () 
	{
		ultraPause = true;

		movex = 0;

		isHitting = false;

		facingRight = true;

		StartCoroutine (Buffer ());

		dieded2 = false;

		if (facingRight == false) {
			Flip();
		}

		rb2d = GetComponent<Rigidbody2D>();
		energy2 = GameObject.Find ("Slider2").GetComponent<Slider> ();
		anim = GetComponent<Animator> ();

		player2Charge = 100;
		energy2.value = 100;

		player2Selection = SelectionScrip.GetPl2Selection ();

//--------------------------------Character stats----------------------------------------

		if (player2Selection == 1){
			infinijump = true;
		}

		if (player2Selection == 1){
			boostCost = 15f;
		} else if (player2Selection == 2) {
			boostCost = 10f;
		} else if (player2Selection == 6) {
			boostCost = 40f;
		} else{
			boostCost = 20f;
		} 
		if (player2Selection == 1){
			jumpCost = 5f;
		} else{
			jumpCost = 10f;
		} 

		if (player2Selection == 2) {
			rb2d.mass = 25;
			rb2d.gravityScale = 0.6f;
			phMt.friction = 0.1f;
			phMt.bounciness = 0.6f;
			jumpForce = 6000f;
			maxSpeed = 50000f;
		} else if (player2Selection == 3) {
			rb2d.mass = 35;
			rb2d.gravityScale = 1.0f;
			phMt.friction = 0.01f;
			phMt.bounciness = 0.7f;
			jumpForce = 8000f;
			maxSpeed = 80000f;
		} else if (player2Selection == 4) {
			rb2d.mass = 15;
			rb2d.gravityScale = 0.3f;
			phMt.friction = 0.1f;
			phMt.bounciness = 0.6f;
			jumpForce = 3000f;
			maxSpeed = 40000f;
		} else if (player2Selection == 5) {
			rb2d.mass = 10f;
			rb2d.gravityScale = 1f;
			phMt.friction = 1f;
			phMt.bounciness = 0.6f;
			jumpForce = 4000f;
			maxSpeed = 20000f;
		} else if (player2Selection == 6) {
			rb2d.mass = 5;
			rb2d.gravityScale = 1.5f;
			phMt.friction = 0.1f;
			phMt.bounciness = 0.3f;
			jumpForce = 3000f;
			maxSpeed = 10000f;
		} else {
			rb2d.mass = 20;
			rb2d.gravityScale = 1f;
			phMt.friction = 0.1f;
			phMt.bounciness = 0.6f;
			jumpForce = 6000f;
			maxSpeed = 50000f;
		}

		if (player2Selection == 4) {
			Instantiate (shieldIcon, new Vector2(-7.6f, 3.2f), Quaternion.identity);
			shield = true;
		} else {
			shield = false;
		}

		if (player2Selection == 1) {
			hitforce2 = 20000;
		} else if (player2Selection == 2) {
			hitforce2 = 5000;
		} else if (player2Selection == 3) {
			hitforce2 = 15000;
		} else if (player2Selection == 5) {
			hitforce2 = 30000;
		} else {
			hitforce2 = 10000;
		}

		if (player2Selection == 2) {
			blockCost = 0.5f;
		} else if (player2Selection == 4) {
			blockCost = 1f;
		} else {
			blockCost = 1.5f;
		}

		ultraPause = false;

	}


//-----------------------------------Update-------------------------------------------------	


	void Update () 
	{
		
		if (blocker == true) {
			anim.SetBool ("Block", true);
			rb2d.isKinematic = true;
		} else {
			anim.SetBool ("Block", false);
			rb2d.isKinematic = false;
		}

		if (Input.GetButtonDown("Jump2") && grounded && player2Charge >= jumpCost && Pauser.pause == false && pause == false)
		{
			grounded = false;
			doubleJump = true;
			rb2d.AddForce(new Vector2(0f, jumpForce));
			player2Charge = player2Charge - jumpCost;
			energy2.value -= jumpCost;
			anim.SetTrigger ("IsJumping");

		} else if (Input.GetButtonDown("Jump2") && !grounded && doubleJump == true && player2Charge >= jumpCost && Pauser.pause == false && pause == false){
			if (!infinijump){
				doubleJump = false;
			}
			anim.SetTrigger ("DoubleJump");
			rb2d.AddForce(new Vector2(0f, jumpForce / 2));
			player2Charge = player2Charge -jumpCost;
			energy2.value -= jumpCost;
		}

		if (Input.GetButtonDown("Fire2") && player2Charge >= boostCost && Pauser.pause == false && pause == false){
			player2Charge = player2Charge - boostCost;
			energy2.value -= boostCost;
			anim.SetTrigger("Dash");
			if (facingRight == true){
				if (player2Selection == 6) {
					Instantiate (floorProp, new Vector2 (transform.position.x + 1, transform.position.y), Quaternion.identity);
				}
			} else {
				if (player2Selection == 6) {
					Instantiate (floorProp, new Vector2 (transform.position.x - 1, transform.position.y), Quaternion.identity);
				}
			}
			if (facingRight == true){
				if (player2Selection == 2) {
					rb2d.AddForce(new Vector2(jumpForce * 2, 0f));
				}
			} else {
				if (player2Selection == 2) {
					rb2d.AddForce(new Vector2(-jumpForce * 2, 0f));
				}
			}
		}

		if (player2Charge < 100) {
			player2Charge = player2Charge + 0.25f;
			energy2.value += 0.25f;
		}

		if (Input.GetButton ("Block2") && pause == false && ultraPause == false) {
			if (player2Charge >= blockCost && Pauser.pause == false) {
				blocker = true;
				player2Charge = player2Charge - blockCost;
				energy2.value -= blockCost;
			} else {
				Instantiate (pum, transform.position, Quaternion.identity);
				StartCoroutine (Stun (2));
			}
		} else {
			blocker = false;
		}

	}


//----------------------------------------FixedUpdate---------------------------------------------


	void FixedUpdate()
	{
		if (ultraPause == false && pause == false) {
			movex = Input.GetAxis ("Horizontal2");
		}
		if (ultraPause == false) {
			if (movex > 0 && !facingRight) 
				Flip ();
			else if (movex < 0 && facingRight)
				Flip ();
		}
		
		rb2d.AddForce (Vector2.right * movex * maxSpeed); 

		if (isHitting == false) {
			if (Input.GetAxis ("Horizontal2") != 0 && anim.GetBool ("Block") == false) {
				anim.SetBool ("Walking", true);
			} else {
				anim.SetBool ("Walking", false);
			}
		}

	}


//--------------------------------------Methods-----------------------------------------------


	void Flip()
	{
		if (ultraPause == false) {
			facingRight = !facingRight;
			Vector3 theScale = transform.localScale;
			theScale.x *= -1;
			transform.localScale = theScale;
		}
	}

	void EndRound()
	{
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

	public static bool GetFacingRight ()
	{
		return facingRight;
	}

	public static int GetHit2 ()
	{
		return hitforce2;
	}

//-------------------------------Collision detection-------------------------------------

	void OnCollisionEnter2D (Collision2D col)
	{
		bool temp;
		hitforce1 = CharacterMove.GetHit1 ();
		temp = CharacterMove.GetFacingRight ();

		if (col.gameObject.tag == "Hitter1" && rb2d.isKinematic == false) {
			if (temp == true){
				rb2d.AddForce(new Vector2(hitforce1, 0f));
				StartCoroutine (Stun(0.1f));
			} else if (temp == false){
				rb2d.AddForce(new Vector2(-hitforce1, 0f));
				StartCoroutine (Stun(0.1f));
			}
		}

		if(col.gameObject.tag == "Ground")
		{
			grounded = true;
			doubleJump = false;
		}

		if(col.gameObject.tag == "Killer")
		{
		
			bool diededTemp;
			int win1temp;
			win1temp = Spawner.GetWin1();
			diededTemp = CharacterMove.dieded1;

			if (shield == true){
				IconDestroyer.Boom();
				shield = false;
			} else{
				if (win1temp == 2 && diededTemp == false){
					Instantiate (pum2, transform.position, Quaternion.identity);
					dieded2 = true;
					Victor.pl1Win = true;
					if (player2Selection == 6){
						Instantiate (floorProp, transform.position, Quaternion.identity);
						Instantiate (floorProp, new Vector2 (transform.position.x + 1, transform.position.y), Quaternion.identity);
						Instantiate (floorProp, new Vector2 (transform.position.x - 1, transform.position.y), Quaternion.identity);
						Instantiate (floorProp, new Vector2 (transform.position.x, transform.position.y + 1), Quaternion.identity);
						Instantiate (floorProp, new Vector2 (transform.position.x, transform.position.y - 1), Quaternion.identity);
						Instantiate (floorProp, new Vector2 (transform.position.x + 1, transform.position.y - 1), Quaternion.identity);
					}
					transform.position = new Vector2 (100, 100);
					Invoke ("EndGame", 1f);
				} else if (win1temp < 2 && diededTemp == false){
					Instantiate (pum2, transform.position, Quaternion.identity);
					dieded2 = true;
					win1temp++;
					Spawner.SetWin1(win1temp);
					if (player2Selection == 6){
						Instantiate (floorProp, transform.position, Quaternion.identity);
						Instantiate (floorProp, new Vector2 (transform.position.x + 1, transform.position.y), Quaternion.identity);
						Instantiate (floorProp, new Vector2 (transform.position.x - 1, transform.position.y), Quaternion.identity);
						Instantiate (floorProp, new Vector2 (transform.position.x, transform.position.y + 1), Quaternion.identity);
						Instantiate (floorProp, new Vector2 (transform.position.x, transform.position.y - 1), Quaternion.identity);
						Instantiate (floorProp, new Vector2 (transform.position.x + 1, transform.position.y - 1), Quaternion.identity);
					}
					transform.position = new Vector2 (100, 100);
					Invoke ("EndRound", 1f);
				} else{
					Instantiate (pum, transform.position, Quaternion.identity);
				}
			}
		}



	}

//---------------------------------Coroutines-------------------------------

	public IEnumerator WaitBool (float delay)
	{
		blocker = false;
		pause = true;
		yield return new WaitForSeconds (delay);
		pause = false;
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

	public IEnumerator Stun (float delay)
	{
		pause = true;
		ultraPause = true;
		yield return new WaitForSeconds (delay);
		pause = false;
		ultraPause = false;
	}

}