﻿using UnityEngine;
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
	
	
	private bool grounded = false;
	private bool doubleJump = false;

	private Rigidbody2D rb2d;

	public static bool rightBump2;
	public static bool leftBump2;

	public float jumpCost;
	public float boostCost;

	public PhysicsMaterial2D phMt;

	public bool shield;

	
	void Awake () 
	{
		rb2d = GetComponent<Rigidbody2D>();
		energy2 = GameObject.Find ("Slider2").GetComponent<Slider> ();

		rightBump2 = true;
		leftBump2 = true;

		player2Charge = 100;
		energy2.value = 100;

		player2Selection = SelectionScrip.GetPl2Selection ();

		if (player2Selection == 1){
			jumpForce = 7000f;
		} else {
			jumpForce = 6000f;
		}

		if (player2Selection == 2){
			boostCost = 15f;
		} else{
			boostCost = 20f;
		} 
		if (player2Selection == 2){
			jumpCost = 1f;
		} else{
			jumpCost = 10f;
		} 

		if (player2Selection == 3) {
			phMt.bounciness = 0.9f;
		} else {
			phMt.bounciness = 0.6f;
		}

		if (player2Selection == 4) {
			shield = true;
		} else {
			shield = false;
		}
	}
	
	
	void Update () 
	{
		
		if (Input.GetButtonDown("Jump2") && grounded && player2Charge >= jumpCost)
		{
			grounded = false;
			doubleJump = true;
			rb2d.AddForce(new Vector2(0f, jumpForce));
			player2Charge = player2Charge - jumpCost;
			energy2.value -= jumpCost;
		} else if (Input.GetButtonDown("Jump2") && !grounded && doubleJump == true && player2Charge >= jumpCost){
			doubleJump = false;
			rb2d.AddForce(new Vector2(0f, jumpForce));
			player2Charge = player2Charge -jumpCost;
			energy2.value -= jumpCost;
		}

		if (Input.GetButtonDown("Fire2") && player2Charge >= boostCost){
			player2Charge = player2Charge - boostCost;
			energy2.value -= boostCost;

			if (facingRight == true){
				rb2d.AddForce(new Vector2(jumpForce * 1.5f, 0f));
				rightBump2 = true;

			} else {
				rb2d.AddForce(new Vector2(-jumpForce * 1.5f, 0f));
				leftBump2 = true;

			}
		}

		if (player2Charge < 100) {
			player2Charge = player2Charge + 0.1f;
			energy2.value += 0.1f;
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

		if(col.gameObject.tag == "Killer")
		{
			if (shield == true){
				shield = false;
			} else{
				Victor.pl1Win = true;
				Application.LoadLevel(2);
			}
		}



	}
	

	
}