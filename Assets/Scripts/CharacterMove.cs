using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class CharacterMove : MonoBehaviour {
	
	[HideInInspector] public bool facingRight = true;
	[HideInInspector] public bool jump = false;
	public float moveForce = 365f;
	public float maxSpeed = 5f;
	public float jumpForce;
	public static float player1Charge;
	public int player1Selection;

	public Slider energy1;
	
	public Transform groundCheck;
	
	
	private bool grounded = false;
	private bool doubleJump = false;
	private Rigidbody2D rb2d;

	public float jumpCost;
	public float boostCost;

	public PhysicsMaterial2D phMt;

	public bool shield;


	void Awake () 
	{
		rb2d = GetComponent<Rigidbody2D> ();
		energy1 = GameObject.Find ("Slider1").GetComponent<Slider> ();

		player1Charge = 100f;
		energy1.value = 100f;

		player1Selection = SelectionScrip.GetPl1Selection ();

		if (player1Selection == 1) {
			jumpForce = 7000f;
		} else {
			jumpForce = 6000f;
		}

		if (player1Selection == 2){
			boostCost = 15f;
		} else{
			boostCost = 20f;
		} 
		if (player1Selection == 2){
			jumpCost = 1f;
		} else{
			jumpCost = 10f;
		} 

		if (player1Selection == 3) {
			phMt.bounciness = 0.9f;
		} else {
			phMt.bounciness = 0.6f;
		}

		if (player1Selection == 4) {
			shield = true;
		} else {
			shield = false;
		}

	}
	

	void Update () 
	{
		
		if (Input.GetButtonDown("Jump") && grounded && player1Charge >= jumpCost)
		{
			grounded = false;
			doubleJump = true;
			rb2d.AddForce(new Vector2(0f, jumpForce));
			player1Charge = player1Charge - jumpCost;
			energy1.value -= jumpCost;

		} else if (Input.GetButtonDown("Jump") && !grounded && doubleJump == true && player1Charge >= jumpCost){
			doubleJump = false;
			rb2d.AddForce(new Vector2(0f, jumpForce));
			player1Charge = player1Charge - jumpCost;
			energy1.value -= jumpCost;
		}

		if (Input.GetButtonDown("Fire1") && player1Charge >= boostCost){

			player1Charge = player1Charge - boostCost;
			energy1.value -= boostCost;

			if (facingRight == true){
				rb2d.AddForce(new Vector2(jumpForce * 1.5f, 0f));
			} else {
				rb2d.AddForce(new Vector2(-jumpForce * 1.5f, 0f));
			}
		}

		if (player1Charge < 100) {
			player1Charge = player1Charge + 0.1f;
			energy1.value += 0.1f;
		}

	}
	
	void FixedUpdate()
	{
		float h = Input.GetAxis("Horizontal");
		
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

		if(col.gameObject.tag == "Killer")
		{
			if (shield == true){
				shield = false;
			} else{
				Victor.pl1Win = false;
				Application.LoadLevel(2);
			}
		}


	}


	
	
}