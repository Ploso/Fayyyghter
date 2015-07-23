using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class CharacterMove2 : MonoBehaviour {
	
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

//---------------------------------------------------------------------------

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

		if (player2Selection == 1){
			infinijump = true;
		}

		if (player2Selection == 2){
			boostCost = 15f;
		} else if (player2Selection == 6) {
			boostCost = 50f;
		} else{
			boostCost = 20f;
		} 
		if (player2Selection == 1){
			jumpCost = 5f;
		} else{
			jumpCost = 10f;
		} 

		if (player2Selection == 3) {
			rb2d.mass = 35;
			phMt.bounciness = 0.80f;
			jumpForce = 8000f;
			rb2d.gravityScale = 1.0f;
			phMt.friction = 0.01f;
			maxSpeed = 80000f;
		} else if (player2Selection == 5) {
			jumpForce = 8000f;
			phMt.friction = 1f;
			phMt.bounciness = 0.6f;
			maxSpeed = 50000f;
		} else if (player2Selection == 6) {
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

		if (player2Selection == 4) {
			Instantiate (shieldIcon, new Vector2(-7.6f, 3.2f), Quaternion.identity);
			shield = true;
		} else {
			shield = false;
		}

		if (player2Selection == 1) {
			hitforce2 = 20000;
		} else if (player2Selection == 2) {
			hitforce2 = 8000;
		} else if (player2Selection == 3) {
			hitforce2 = 15000;
		} else if (player2Selection == 5) {
			hitforce2 = 30000;
		} else {
			hitforce2 = 10000;
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

		if (Input.GetButtonDown("Jump2") && grounded && player2Charge >= jumpCost && Pauser.pause == false)
		{
			grounded = false;
			doubleJump = true;
			rb2d.AddForce(new Vector2(0f, jumpForce));
			player2Charge = player2Charge - jumpCost;
			energy2.value -= jumpCost;
			anim.SetTrigger ("IsJumping");

		} else if (Input.GetButtonDown("Jump2") && !grounded && doubleJump == true && player2Charge >= jumpCost && Pauser.pause == false){
			if (!infinijump){
				doubleJump = false;
			}
			anim.SetTrigger ("DoubleJump");
			rb2d.AddForce(new Vector2(0f, jumpForce / 2));
			player2Charge = player2Charge -jumpCost;
			energy2.value -= jumpCost;
		}

		if (Input.GetButtonDown("Fire2") && player2Charge >= boostCost && Pauser.pause == false){
			player2Charge = player2Charge - boostCost;
			energy2.value -= boostCost;
			anim.SetTrigger("Dash");
			//StartCoroutine (HitPause());
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

		if (Input.GetButtonDown ("Block2") && player2Charge >= blockCost && Pauser.pause == false) {
			player2Charge = player2Charge - blockCost;
			energy2.value -= blockCost;
			StartCoroutine (WaitBool(1));
		}

	}


//-------------------------------------------------------------------------------------


	void FixedUpdate()
	{
		if (ultraPause == false) {
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


//-------------------------------------------------------------------------------------


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
		hitforce1 = CharacterMove.GetHit1 ();
		temp = CharacterMove.GetFacingRight ();

		if (col.gameObject.tag == "Hitter1") {
			if (temp == true){
				rb2d.AddForce(new Vector2(hitforce1, 0f));
			} else if (temp == false){
				rb2d.AddForce(new Vector2(-hitforce1, 0f));
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

	public static int GetHit2 ()
	{
		return hitforce2;
	}
}