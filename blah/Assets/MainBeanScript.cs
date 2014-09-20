using UnityEngine;
using System.Collections;

public class MainBeanScript : MonoBehaviour {

	public float speed = 5.0f;
	public static bool canJump;
	//public Animator ani;

	// Use this for initialization
	void Start () {
		canJump = true;
		rigidbody.freezeRotation = true;
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKey (KeyCode.LeftArrow)) {
			transform.Translate (Vector2.right * speed * Time.deltaTime * (-1));
		}
		if (Input.GetKey (KeyCode.RightArrow)) {
			transform.Translate (Vector2.right * speed * Time.deltaTime);
		}
		if (canJump == true){
			if (Input.GetKey (KeyCode.UpArrow)) {
				rigidbody.AddForce(Vector2.up * 300);
				canJump = false;
			}
		}
		if(this.transform.position.z != 0){
			this.transform.position = new Vector3 (this.transform.position.x, this.transform.position.y, 0);
		}
	}

	void OnCollisionEnter(Collision other){
		if(other.gameObject.tag == "brick" || other.gameObject.tag == "button"){
			canJump = true;
		}
	}
}
