using UnityEngine;
using System.Collections;

public class NewPlayerController : UnitController {

	void FixedUpdate() {
		grounded = Physics2D.Linecast(transform.position, groundCheck.position, whatIsGround);
		move = Input.GetAxis("Horizontal");
	}

	// Update is called once per frame
	void Update() {
		if (CanJump() && (Input.GetKeyDown (KeyCode.W) || Input.GetKeyDown (KeyCode.UpArrow))) {
			Jump();
		}

		Move();
		
		if (Input.GetKey(KeyCode.Escape)) {
			Application.Quit();
		}
		
		if (Input.GetKey(KeyCode.R)) {
			Application.LoadLevel(Application.loadedLevel);
		}
		
		if (Input.GetKey(KeyCode.F)) {
			GameObject bill = GameObject.Instantiate(bullet) as GameObject;
			bill.transform.position = transform.position + new Vector3(-1, 0, 0);
		}

	}

	void OnGUI(){
		GUI.Box (new Rect (0, 0, 100, 25), "Health: " + health);
	}

}