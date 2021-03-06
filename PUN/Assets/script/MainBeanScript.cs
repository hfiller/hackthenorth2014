﻿using UnityEngine;
using System.Collections;

public class MainBeanScript : MonoBehaviour {

	public float speed = 5.0f;
	public static bool canJump;
	//public Animator ani;

	// Use this for initialization
	void Start () {
		canJump = true;
		rigidbody.freezeRotation = true;
		this.renderer.material.color = Color.red;
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
				rigidbody.AddForce(Vector2.up * 200);
				canJump = false;
			}
		}
		if(this.transform.position.z != -1){
			this.transform.position = new Vector3 (this.transform.position.x, this.transform.position.y, -1);
		}
	}

	void OnCollisionEnter(Collision other){
		if(other.gameObject.tag == "map") {
			canJump = true;
		}
		if(other.gameObject.tag == "spikes"){
			//a guess
			this.transform.Translate(new Vector3(-2, 1.5f, 0));
		}
	}
}
