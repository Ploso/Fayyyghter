﻿using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class CharacterMove : MonoBehaviour {
	
	[HideInInspector] public bool facingRight;
	[HideInInspector] public bool jump = false;
	public float moveForce = 365f;
	public float maxSpeed = 5f;
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

	public PhysicsMaterial2D phMt;

	public bool shield;
	
	public GameObject shieldIcon;
	public GameObject floorProp;
	public GameObject pum;
	public GameObject pum2;

	public static bool dieded1;

	private Animator anim;

	public GameObject hitbox;

	void Awake () 
	{

		dieded1 = false;

		Flip ();

		rb2d = GetComponent<Rigidbody2D> ();
		energy1 = GameObject.Find ("Slider1").GetComponent<Slider> ();
		anim = GetComponent<Animator> ();

		player1Charge = 100f;
		energy1.value = 100f;

		player1Selection = SelectionScrip.GetPl1Selection ();

		if (player1Selection == 1) {
			infinijump = true;
		}

		if (player1Selection == 2){
			boostCost = 15f;
		} else{
			boostCost = 20f;
		} 
		if (player1Selection == 1){
			jumpCost = 5f;
		} else{
			jumpCost = 10f;
		} 

		if (player1Selection == 3) {
			rb2d.mass = 40;
			phMt.bounciness = 0.95f;
			jumpForce = 8000f;
			rb2d.gravityScale = 1.5f;
			phMt.friction = 0.01f;
		} else if (player1Selection == 5) {
			jumpForce = 8000f;
			phMt.friction = 1f;
			phMt.bounciness = 0.6f;
		} else {
			phMt.friction = 0.1f;
			phMt.bounciness = 0.6f;
			jumpForce = 6000f;
		}

		if (player1Selection == 4) {
			Instantiate (shieldIcon, new Vector2(7.6f, 3.2f), Quaternion.identity);
			shield = true;
		} else {
			shield = false;
		}

		
	}
	

	void Update () 
	{
		
		if (Input.GetButtonDown("Jump") && grounded && player1Charge >= jumpCost && Pauser.pause == false)
		{
			grounded = false;
			doubleJump = true;
			rb2d.AddForce(new Vector2(0f, jumpForce));
			player1Charge = player1Charge - jumpCost;
			energy1.value -= jumpCost;
			anim.SetTrigger ("IsJumping");

		} else if (Input.GetButtonDown("Jump") && !grounded && doubleJump == true && player1Charge >= jumpCost && Pauser.pause == false){
			if (!infinijump){
			doubleJump = false;
			}
			anim.SetTrigger ("DoubleJump");
			rb2d.AddForce(new Vector2(0f, jumpForce));
			player1Charge = player1Charge - jumpCost;
			energy1.value -= jumpCost;
		}

		if (Input.GetButtonDown("Fire1") && player1Charge >= boostCost && Pauser.pause == false){

			player1Charge = player1Charge - boostCost;
			energy1.value -= boostCost;
			anim.SetTrigger("Dash");
			Instantiate (hitbox, new Vector2 (transform.position.x + 0.5f, transform.position.y), Quaternion.identity);
			if (facingRight == true){
				rb2d.AddForce(new Vector2(jumpForce * 1.5f, 0f));
				if (player1Selection == 6) {
					Instantiate (floorProp, new Vector2 (transform.position.x - 1, transform.position.y), Quaternion.identity);
				}
			} else {
				rb2d.AddForce(new Vector2(-jumpForce * 1.5f, 0f));
				if (player1Selection == 6) {
					Instantiate (floorProp, new Vector2 (transform.position.x + 1, transform.position.y), Quaternion.identity);
				}
			}
		}

		if (player1Charge < 100) {
			player1Charge = player1Charge + 0.25f;
			energy1.value += 0.25f;
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

		if(col.gameObject.tag == "Player2")
		{
			if (facingRight == true){
				Instantiate (pum, new Vector2(transform.position.x + 0.5f, transform.position.y), Quaternion.identity);
			} else {
				Instantiate (pum, new Vector2(transform.position.x - 0.5f, transform.position.y), Quaternion.identity);
			}
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
					transform.position = new Vector2 (100, 100);
					Invoke ("EndGame", 1f);
				} else if ((win2temp < 2 && diededTemp == false)){
					Instantiate (pum2, transform.position, Quaternion.identity);
					dieded1 = true;
					win2temp++;
					Spawner2.SetWin2(win2temp);
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
		Application.LoadLevel(1);
	}
	
	void EndGame()
	{
		Application.LoadLevel(2);
	}



	
	
}