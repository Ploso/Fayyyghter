using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class CharacterMove : MonoBehaviour {
	
	[HideInInspector] public bool facingRight = true;
	[HideInInspector] public bool jump = false;
	public float moveForce = 365f;
	public float maxSpeed = 5f;
	public float jumpForce = 1000f;
	public static float player1Charge;

	public Slider energy1;
	
	public Transform groundCheck;
	
	
	private bool grounded = false;
	private bool doubleJump = false;
	//private Animator anim;
	private Rigidbody2D rb2d;

	public static bool rightBump1;
	public static bool leftBump1;
	
	

	void Awake () 
	{
		//anim = GetComponent<Animator>();
		rb2d = GetComponent<Rigidbody2D>();
		rightBump1 = true;
		leftBump1 = true;

		player1Charge = 100f;
		energy1.value = 100f;
	}
	

	void Update () 
	{
		
		if (Input.GetButtonDown("Jump") && grounded && player1Charge >= 10)
		{
			grounded = false;
			doubleJump = true;
			rb2d.AddForce(new Vector2(0f, jumpForce));
			player1Charge = player1Charge -10f;
			energy1.value -= 10f;
		} else if (Input.GetButtonDown("Jump") && !grounded && doubleJump == true && player1Charge >= 10){
			doubleJump = false;
			rb2d.AddForce(new Vector2(0f, jumpForce));
			player1Charge = player1Charge -10f;
			energy1.value -= 10f;
		}

		if (Input.GetButtonDown("Fire1") && player1Charge >= 20){
			player1Charge = player1Charge -20f;
			energy1.value -= 20f;

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
			Victor.pl1Win = false;
			Application.LoadLevel(2);
		}


		if (col.gameObject.tag == "Player") 
		{
			bool leftBump2 = CharacterMove2.GetLeft2();
			bool rightBump2 = CharacterMove2.GetRight2();

			if (leftBump2 == true){
				Debug.Log("bölö");
				rb2d.AddForce(new Vector2(-jumpForce, 0f));
				//leftBump2 = false;
				//CharacterMove2.setLeft2(leftBump2);
			}
			if (rightBump2 == true){
				Debug.Log("bölö");
				rb2d.AddForce(new Vector2(jumpForce, 0f));
				//rightBump2 = false;
				//CharacterMove2.setRight2(rightBump2);
			}
		}
	}

	public static bool GetRight1()
	{
		return rightBump1;
	}
	
	public static bool GetLeft1()
	{
		return leftBump1;
	}

	public static void setRight1(bool tempRight)
	{
		rightBump1 = tempRight;
	}

	public static void setLeft1(bool tempLeft)
	{
		leftBump1 = tempLeft;
	}
	
	
}