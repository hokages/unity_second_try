using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ParallaxController : MonoBehaviour {

	public Transform target;
	public ParallaxLayer[] layers;

	// Use this for initialization
	void Start () {
		foreach(ParallaxLayer layer in layers) {
			layer.Start(target);
		}
	}
	
	// Update is called once per frame
	void Update () {
		foreach(ParallaxLayer layer in layers) {
			layer.Update();
		}
	}
}

[System.Serializable]
public class ParallaxLayer {

	public Transform background;
//	public List<Transform> backgroundsMirrors;
	[Tooltip("If set 0 will be used sprite width")]
	public float layerWidth = 0;
	[Tooltip("If set false speed will be (-1 / position.z)")]
	public bool useZPosition = false;
	public Vector2 parallaxSpeed = new Vector2(1, 1);
	public Vector2 movingSpeed = new Vector2(0, 0);
	
	private Transform target;
	private Transform backgroundRight;
	private Transform backgroundLeft;
	private Vector3 lastCamPos;
	private Vector3 delta;
	private Vector3 startSpritePosition;
	private Vector3 movingSpeed3;

	public void Start(Transform cam) {
		startSpritePosition = background.position;
		movingSpeed3 = new Vector3(movingSpeed.x, movingSpeed.y, 1);

		if (useZPosition) {
			parallaxSpeed.x = -1 / background.position.z;
			parallaxSpeed.y = -1 / background.position.z;
		}
		target = cam;
		lastCamPos = target.position;

		float mirroredWidth = background.GetComponent<SpriteRenderer>().bounds.extents.x
			* background.localScale.x
			+ background.position.x;

		if (layerWidth == 0) {
			layerWidth = mirroredWidth - startSpritePosition.x;
		}

		Vector3 backgroundMirrorPosition = new Vector3(
			startSpritePosition.x + layerWidth, 
			startSpritePosition.y, 
			startSpritePosition.z);

		backgroundRight = Transform.Instantiate(background) as Transform;
		backgroundRight.transform.parent = background.transform.parent;
		backgroundRight.position = backgroundMirrorPosition;
		
		backgroundMirrorPosition.x = startSpritePosition.x - layerWidth;
		backgroundLeft = Transform.Instantiate(background) as Transform;
		backgroundLeft.transform.parent = background.transform.parent;
		backgroundLeft.position = backgroundMirrorPosition;
		
	}
	
	public void Update() {
		delta +=  lastCamPos - target.position + movingSpeed3;
		float deltaX = delta.x * Mathf.Abs(parallaxSpeed.x) + movingSpeed.x;

		if (deltaX >= layerWidth || deltaX <= -layerWidth) {
			delta.x = 0;
		}

		lastCamPos = target.position;

		float newPosX = target.position.x + startSpritePosition.x + delta.x * parallaxSpeed.x;
		float newPosY = target.position.y + startSpritePosition.y + delta.y * parallaxSpeed.y;

		background.position = new Vector3(newPosX, newPosY, background.position.z);
		backgroundRight.position = new Vector3(layerWidth + newPosX, newPosY, backgroundRight.position.z);
		backgroundLeft.position = new Vector3(-layerWidth + newPosX, newPosY, backgroundLeft.position.z);
	}
	
}
