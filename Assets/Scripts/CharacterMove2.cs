using UnityEngine;
using System.Collections;

public class CharacterMove2 : MonoBehaviour {
	
	[HideInInspector] public bool facingRight = true;
	[HideInInspector] public bool jump = false;
	public float moveForce = 365f;
	public float maxSpeed = 5f;
	public float jumpForce = 1000f;

	public Transform groundCheck;
	
	
	private bool grounded = false;
	private bool doubleJump = false;
	//private Animator anim;
	private Rigidbody2D rb2d;
	
	
	
	void Awake () 
	{
		//anim = GetComponent<Animator>();
		rb2d = GetComponent<Rigidbody2D>();
	}
	
	
	void Update () 
	{
		
		if (Input.GetButtonDown("Jump2") && grounded)
		{
			grounded = false;
			doubleJump = true;
			rb2d.AddForce(new Vector2(0f, jumpForce));
		} else if (Input.GetButtonDown("Jump2") && !grounded && doubleJump == true){
			doubleJump = false;
			rb2d.AddForce(new Vector2(0f, jumpForce));
		}



	}
	
	void FixedUpdate()
	{
		float h = Input.GetAxis("Horizontal2");
		
		//anim.SetFloat("Speed", Mathf.Abs(h));
		
		if (h * rb2d.velocity.x < maxSpeed)
			rb2d.AddForce(Vector2.right * h * moveForce);
		
		if (Mathf.Abs (rb2d.velocity.x) > maxSpeed)
			rb2d.velocity = new Vector2(Mathf.Sign (rb2d.velocity.x) * maxSpeed, rb2d.velocity.y);
		
		if (h > 0 && !facingRight)
			Flip ();
		else if (h < 0 && facingRight)
			Flip ();

		if (Input.GetButtonDown("Fire1")){
			for (int i = 0; i < 10; i++){
				moveForce = 2000;
				maxSpeed = 10;
			}
		}
		
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
	}
	
	
}