using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {
	public float maxSpeed = 10f;
	public float jumpForce = 700f;
	public Transform groundCheck;
	public LayerMask whatIsGround;
	public float move;

	private bool facingRight = true;
	private bool grounded = false;
	
	void Flip(){
		facingRight = !facingRight;
		Vector3 theScale = transform.localScale;
		theScale.x *= -1;
		transform.localScale = theScale;
	}
	
	void FixedUpdate() {
		grounded = Physics2D.Linecast(transform.position, groundCheck.position, whatIsGround);
		move = Input.GetAxis("Horizontal");
	}
	
	// Update is called once per frame
	void Update() {
		if (grounded && (Input.GetKeyDown (KeyCode.W) || Input.GetKeyDown (KeyCode.UpArrow))) {
			GetComponent<Rigidbody2D>().AddForce(new Vector2(0f, jumpForce));
		}
				
		GetComponent<Rigidbody2D>().velocity = new Vector2(move * maxSpeed, GetComponent<Rigidbody2D>().velocity.y);
		
		if (move > 0 && !facingRight) {
			Flip();
		} else if (move < 0 && facingRight) {
			Flip();
		}

		if (Input.GetKey(KeyCode.Escape)) {
			Application.Quit();
		}
						
		if (Input.GetKey(KeyCode.R)) {
			Application.LoadLevel(Application.loadedLevel);
		}
	}
	
	void OnGUI(){
//		GUI.Box (new Rect (0, 0, 100, 100), "grounded: " + grounded);
	}
	
}
