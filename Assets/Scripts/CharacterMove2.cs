using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class CharacterMove2 : MonoBehaviour {
	
	[HideInInspector] public bool facingRight = false;
	[HideInInspector] public bool jump = false;
	public float moveForce = 365f;
	public float maxSpeed = 5f;
	public float jumpForce = 1000f;
	public static float player2Charge;

	public Slider energy2;
	
	public Transform groundCheck;
	
	
	private bool grounded = false;
	private bool doubleJump = false;
	//private Animator anim;
	private Rigidbody2D rb2d;

	public static bool rightBump2;
	public static bool leftBump2;

	//public GameObject rightFlame;
	//public GameObject leftFlame;
	
	
	
	void Awake () 
	{
		//anim = GetComponent<Animator>();
		rb2d = GetComponent<Rigidbody2D>();
		rightBump2 = true;
		leftBump2 = true;

		player2Charge = 100;
		energy2.value = 100;
	}
	
	
	void Update () 
	{
		
		if (Input.GetButtonDown("Jump2") && grounded && player2Charge >= 10)
		{
			grounded = false;
			doubleJump = true;
			rb2d.AddForce(new Vector2(0f, jumpForce));
			player2Charge = player2Charge -10f;
			energy2.value -= 10f;
		} else if (Input.GetButtonDown("Jump2") && !grounded && doubleJump == true && player2Charge >= 10){
			doubleJump = false;
			rb2d.AddForce(new Vector2(0f, jumpForce));
			player2Charge = player2Charge -10f;
			energy2.value -= 10f;
		}

		if (Input.GetButtonDown("Fire2") && player2Charge >= 20){
			player2Charge = player2Charge -20f;
			energy2.value -= 20f;

			if (facingRight == true){
				rb2d.AddForce(new Vector2(jumpForce * 1.5f, 0f));
				rightBump2 = true;
				//Instantiate(rightFlame, transform.position, Quaternion.identity);
			} else {
				rb2d.AddForce(new Vector2(-jumpForce * 1.5f, 0f));
				leftBump2 = true;
				//Instantiate(leftFlame, transform.position, Quaternion.identity);
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
		
		//anim.SetFloat("Speed", Mathf.Abs(h));
		
		//if (h * rb2d.velocity.x < maxSpeed && grounded)
			//rb2d.AddForce(Vector2.right * h * moveForce);
		
		//if (Mathf.Abs (rb2d.velocity.x) > maxSpeed && grounded)
			//rb2d.velocity = new Vector2(Mathf.Sign (rb2d.velocity.x) * maxSpeed, rb2d.velocity.y);
		
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
			Victor.pl1Win = true;
			Application.LoadLevel(2);
		}

		if (col.gameObject.tag == "Player") 
		{
			bool leftBump1 = CharacterMove.GetLeft1();
			bool rightBump1 = CharacterMove.GetRight1();
			
			if (leftBump1 == true){
				Debug.Log("bölö");
				rb2d.AddForce(new Vector2(-jumpForce, 0f));
				//leftBump1 = false;
				//CharacterMove.setLeft1(leftBump1);
			}
			if (rightBump1 == true){
				Debug.Log("bölö");
				rb2d.AddForce(new Vector2(jumpForce, 0f));
				//rightBump1 = false;
				//CharacterMove.setRight1(rightBump1);
			}
		}


	}

	public static bool GetRight2()
	{
		return rightBump2;
	}

	public static bool GetLeft2()
	{
		return leftBump2;
	}

	public static void setRight2(bool tempRight)
	{
		rightBump2 = tempRight;
	}
	
	public static void setLeft2(bool tempLeft)
	{
		leftBump2 = tempLeft;
	}
	
	
}