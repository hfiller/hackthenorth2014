using UnityEngine;
using System.Collections;

public class generator : MonoBehaviour {
	
	public GameObject brick;
	public GameObject button;
	//public Level level;

	public int[,] map;
	
	// Use this for initialization
	void Start () {
		int mapwidth = 20;
		int mapheight = 20;

		map = new int[,] {
			{1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1},
			{1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1},
			{1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1},
			{1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1},
			{1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1},
			{1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1},
			{1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1},
			{1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1},
			{1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1},
			{1,0,0,0,0,0,0,0,0,0,0,0,1,1,1,0,0,0,0,1},
			{1,0,0,0,0,0,0,0,0,0,0,0,1,1,1,0,0,0,0,1},
			{1,0,0,0,0,0,0,0,0,0,0,0,1,0,1,0,0,0,0,1},
			{1,0,0,0,0,0,0,0,0,0,0,0,1,0,1,0,0,0,0,1},
			{1,0,0,0,0,0,0,0,0,0,0,0,1,0,1,0,0,0,0,1},
			{1,0,0,0,0,0,0,0,0,0,0,0,0,3,5,5,5,0,0,1},
			{1,0,0,0,0,0,0,0,0,0,0,0,0,3,0,0,0,0,0,1},
			{1,0,0,0,0,0,0,0,0,0,0,0,0,3,0,0,0,0,0,1},
			{1,1,1,1,0,0,5,5,1,1,7,1,1,1,1,1,4,1,1,1},
			{1,1,1,1,6,6,6,6,1,1,1,1,1,1,1,1,1,1,1,1},
			{1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1},


		
		};
		//level = new Level ("merp.txt");
		//create using map 1 - first layer
		for (int i = mapheight-1; i >= 0; i--) {
			for (int j = 0; j < mapwidth; j++) {
				int blockType = map[i,j];
				if(blockType == 1){
					GameObject t = (GameObject)Instantiate (brick, new Vector3 
					(this.transform.position.x - this.renderer.bounds.size.x/2 + brick.renderer.bounds.size.x * j, 
					 this.transform.position.y + 10 + brick.renderer.bounds.size.y * -i, this.transform.position.z), Quaternion.identity);
					//name the tile based on its position
					t.name = "tile" + i + "$" + j;
				}
				if(blockType == 4 || blockType == 5){
					GameObject t = (GameObject)Instantiate (brick, new Vector3 
					(this.transform.position.x - this.renderer.bounds.size.x/2 + brick.renderer.bounds.size.x * j, 
					 this.transform.position.y + 10 + brick.renderer.bounds.size.y * -i, this.transform.position.z), Quaternion.identity);
					//name the tile based on its position
					t.name = "tile" + i + "$" + j;
					GameObject.Find("tile" + i + "$" + j).renderer.material.color = Color.blue;
				}
				if(blockType == 3 || blockType == 2){
					GameObject t = (GameObject)Instantiate (brick, new Vector3 
					(this.transform.position.x - this.renderer.bounds.size.x/2 + brick.renderer.bounds.size.x * j, 
					 this.transform.position.y + 10 + brick.renderer.bounds.size.y * -i, this.transform.position.z), Quaternion.identity);
					//name the tile based on its position
					t.name = "tile" + i + "$" + j;
					GameObject.Find("tile" + i + "$" + j).rigidbody.detectCollisions = true;
					GameObject.Find("tile" + i + "$" + j).renderer.material.color = Color.red;
				}
				if(blockType == 6){
					GameObject t = (GameObject)Instantiate (brick, new Vector3 
					(this.transform.position.x - this.renderer.bounds.size.x/2 + brick.renderer.bounds.size.x * j, 
					this.transform.position.y + 10 + brick.renderer.bounds.size.y * -i, this.transform.position.z), Quaternion.identity);
					//name the tile based on its position
					t.name = "tile" + i + "$" + j;
					GameObject.Find("tile" + i + "$" + j).renderer.material.color = Color.black;
				}
				if(blockType == 7){
					GameObject t = (GameObject)Instantiate (button, new Vector3 
					(this.transform.position.x - this.renderer.bounds.size.x/2 + brick.renderer.bounds.size.x * j, 
					 this.transform.position.y + 10 + brick.renderer.bounds.size.y * -i, this.transform.position.z), Quaternion.identity);
					//name the tile based on its position
					t.name = "tile" + i + "$" + j;
					GameObject.Find("tile" + i + "$" + j).renderer.material.color = Color.green;
				}
			}
		}
	}
	
	// Update is called once per frame
	void Update () {

	}

	void buttonswitch (){
		GameObject.Find ("tile" + 14 + "$" + 13).transform.Translate (Vector3.up * Time.deltaTime);
		GameObject.Find ("tile" + 15 + "$" + 13).transform.Translate (Vector3.up * Time.deltaTime);
		GameObject.Find ("tile" + 16 + "$" + 13).transform.Translate (Vector3.up * Time.deltaTime);
	}

	void OnCollisionEnter(Collision other){
		if (other.gameObject.tag == "brick") {

		}
	}
}














