using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour {

	public Transform target;		// Reference to the player's transform.
	public float smoothTime = 0.1f; // time for dampen
	public float xMargin = 1f;		// Distance in the x axis the player can move before the camera follows.
	public float yMargin = 1f;		// Distance in the y axis the player can move before the camera follows.
	private Vector2 velocity; // speed of camera movement
	
	bool CheckXMargin() {
		// Returns true if the distance between the camera and the player in the x axis is greater than the x margin.
		return Mathf.Abs(transform.position.x - target.position.x) > xMargin;
	}	
	
	bool CheckYMargin() {
		// Returns true if the distance between the camera and the player in the y axis is greater than the y margin.
		return Mathf.Abs(transform.position.y - target.position.y) > yMargin;
	}
	
	void TrackPlayer() {
		// By default the target x and y coordinates of the camera are it's current x and y coordinates.
		float targetX = transform.position.x;
		float targetY = transform.position.y;
		
		// If the player has moved beyond the x margin...
		if (CheckXMargin()) {
			// ... the target x coordinate should be a Lerp between the camera's current x position and the player's current x position.
			targetX = Mathf.SmoothDamp(
				transform.position.x,
				target.transform.position.x,
				ref velocity.x,
				smoothTime
			);
		}
		
		// If the player has moved beyond the y margin...
		if(CheckYMargin()) {
			// ... the target y coordinate should be a Lerp between the camera's current y position and the player's current y position.
			targetY = Mathf.SmoothDamp (
				transform.position.y,
				target.transform.position.y,
				ref velocity.y,
				smoothTime
			);
		}

		
		// Set the camera's position to the target position with the same z component.
		transform.position = new Vector3(targetX, targetY, transform.position.z);
	}
	
	void Update() {
		TrackPlayer();
	}
}
