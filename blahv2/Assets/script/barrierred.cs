using UnityEngine;
using System.Collections;

public class barrierred : MonoBehaviour {


	public Transform tra;
	bool isFalling;
	// Use this for initialization
	void Start () {
		isFalling = true;
		this.renderer.material.color = Color.red;
		rigidbody.freezeRotation = true;

	}
	
	// Update is called once per frame
	void Update () {
		if(this.transform.position.z != -1){
			this.transform.position = new Vector3 (this.transform.position.x, this.transform.position.y, -1);
		}
		if (this.transform.position.y > -3.2 && isFalling) {
			this.transform.Translate (Vector3.down * Time.deltaTime * 7);
		}
	
	}

	void openGate(){
		isFalling = false;
		this.transform.Translate (Vector3.up * Time.deltaTime * 6);
	}

	void closeGate (){
		isFalling = true;
	}
}
