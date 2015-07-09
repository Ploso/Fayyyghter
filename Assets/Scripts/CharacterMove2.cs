using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class CharacterMove2 : MonoBehaviour {
	
	[HideInInspector] public bool facingRight = false;
	[HideInInspector] public bool jump = false;
	public float moveForce = 365f;
	public float maxSpeed = 5f;
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

	public PhysicsMaterial2D phMt;

	public bool shield;

	public GameObject shieldIcon;
	public GameObject floorProp;
	public GameObject pum;
	public GameObject pum2;

	public static bool dieded2;

	private Animator anim;
	
	void Awake () 
	{
		dieded2 = false;

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
		} else{
			boostCost = 20f;
		} 
		if (player2Selection == 1){
			jumpCost = 5f;
		} else{
			jumpCost = 10f;
		} 

		if (player2Selection == 3) {
			rb2d.mass = 40;
			phMt.bounciness = 0.95f;
			jumpForce = 8000f;
			rb2d.gravityScale = 1.5f;
			phMt.friction = 0.01f;
		} else if (player2Selection == 5) {
			jumpForce = 8000f;
			phMt.friction = 1f;
			phMt.bounciness = 0.6f;
		} else {
			phMt.friction = 0.1f;
			phMt.bounciness = 0.6f;
			jumpForce = 6000f;
		}

		if (player2Selection == 4) {
			Instantiate (shieldIcon, new Vector2(-7.6f, 3.2f), Quaternion.identity);
			shield = true;
		} else {
			shield = false;
		}

	}
	
	
	void Update () 
	{
		
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

			if (facingRight == true){
				rb2d.AddForce(new Vector2(jumpForce * 1.5f, 0f));
				if (player2Selection == 6) {
					Instantiate (floorProp, new Vector2 (transform.position.x - 1, transform.position.y), Quaternion.identity);
				}

			} else {
				rb2d.AddForce(new Vector2(-jumpForce * 1.5f, 0f));
				if (player2Selection == 6) {
					Instantiate (floorProp, new Vector2 (transform.position.x + 1, transform.position.y), Quaternion.identity);
				}

			}
		}

		if (player2Charge < 100) {
			player2Charge = player2Charge + 0.25f;
			energy2.value += 0.25f;
		}

	}
	
	void FixedUpdate()
	{
		float h = Input.GetAxis("Horizontal2");
		
		if (h > 0 && !facingRight)
			Flip ();
		else if (h < 0 && facingRight)
			Flip ();


		
	}
	
	
	void Flip()
	{
		facingRight = !facingRight;
		Vector3 theScale = transform.localScale;
		theScale.x *= -1;
		transform.localScale = theScale;
	}
	
	void OnCollisionEnter2D (Collision2D col)
	{
		if(col.gameObject.tag == "Ground")
		{
			grounded = true;
			doubleJump = false;
		}

		if(col.gameObject.tag == "Player")
		{
			if (facingRight == true){
				Instantiate (pum, new Vector2(transform.position.x + 0.5f, transform.position.y), Quaternion.identity);
			} else {
				Instantiate (pum, new Vector2(transform.position.x - 0.5f, transform.position.y), Quaternion.identity);
			}
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
		Application.LoadLevel(1);
	}

	void EndGame()
	{
		Application.LoadLevel(2);
	}
	

	
}