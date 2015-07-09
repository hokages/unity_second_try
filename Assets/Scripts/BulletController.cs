using UnityEngine;
using System.Collections;

public class BulletController : MonoBehaviour {

	public Vector2 speed = new Vector2(-500 , 0);
	private string healthboxTagName = "healthbox";
	private float timer = 0.5f;

	// Use this for initialization
	void Start () {
		transform.GetComponent<Rigidbody2D>().AddForce(speed);
	}
	
	// Update is called once per frame
	void Update () {
		timer -= Time.deltaTime;
		if (timer <= 0 ) {
			Destroy(this.gameObject);
		}
		print (timer);
	}
	
	void OnTriggerEnter2D(Collider2D other) {
		if (other.tag == healthboxTagName) {
			other.GetComponent<Transform>().parent.GetComponent<NewPlayerController>().TakeADamage(10);
			DestroyObject(this.gameObject);
		}
	}
}
