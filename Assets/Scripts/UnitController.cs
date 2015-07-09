using UnityEngine;
using System.Collections;

public abstract class UnitController : MonoBehaviour {
	public int health = 100;
	public float maxSpeed = 10f;
	public float jumpForce = 700f;
	public Transform groundCheck;
	public LayerMask whatIsGround;
	public GameObject bullet;

	protected float move;
	protected bool facingRight = true;
	protected bool grounded = false;
	
	protected virtual void Flip() {
		facingRight = !facingRight;
		Vector3 theScale = transform.localScale;
		theScale.x *= -1;
		transform.localScale = theScale;
	}

	protected virtual bool CanJump() {
		return grounded;
	}

	protected virtual void Jump() {
		GetComponent<Rigidbody2D>().AddForce(new Vector2(0f, jumpForce));
	}
	
	protected virtual void Move() {
		GetComponent<Rigidbody2D>().velocity = new Vector2(move * maxSpeed, GetComponent<Rigidbody2D>().velocity.y);
		if (move > 0 && !facingRight) {
			Flip();
		} else if (move < 0 && facingRight) {
			Flip();
		}
	}

	public virtual void TakeADamage(int dmg) {
		health -= dmg;
	}
	
}
