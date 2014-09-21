using UnityEngine;
using System.Collections;

public class barrierblue : MonoBehaviour {

	public bool isMoving;

	// Use this for initialization
	void Start () {
		isMoving = false;
		this.renderer.material.color = Color.blue;
	}
	
	// Update is called once per frame
	void Update () {
		if(this.transform.position.z != -1){
			this.transform.position = new Vector3 (this.transform.position.x, this.transform.position.y, -1);
		}
		if (isMoving) {			
			this.transform.Translate (Vector3.down * Time.deltaTime * 6);
		}
		if (this.transform.position.x > 2) {
			this.transform.position = new Vector3 (2, this.transform.position.y, -1);
		}
		if (this.transform.position.x < 1) {
			this.transform.position = new Vector3 (1, this.transform.position.y, -1);
		}
	
	}
	void openGateBlue(){
		//if (this.transform.position.x > 1 && isMoving) {
			isMoving = false;
			this.transform.Translate (Vector3.up * Time.deltaTime * 7);
		//}
	}

	void closeGateBlue(){
		isMoving = true;
	}
	

}
