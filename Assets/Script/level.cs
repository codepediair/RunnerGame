using UnityEngine;
using System.Collections;

public class level: MonoBehaviour {
	
	// Use this for initialization
	private GameObject collectedTiles;
	private const float tileWidth= 1.792f;
	public GameObject tilePos;
	private int heightLevel = 0;
	
	private GameObject gameLayer;
	private GameObject bgLayer;
	
	private GameObject tmpTile;
	
	private float startUpPosY;
	
	public float gameSpeed = 3.0f;
	private float outofbounceX;
	private int blankCounter = 0;
	private int middleCounter = 0;
	private string lastTile = "right";
	//private string lastFance = "right";
	private float startTime;
	private bool Barrel_RedAdded = false;
	private bool BoxAdded = false;

	private float outofBounceY;
	private GameObject player;

	void Awake(){

		Application.targetFrameRate = 60;
	}
	
	
	void Start () 
	{
		gameLayer = GameObject.Find("Game Layer");
		bgLayer = GameObject.Find("BackgroundLayer");
		collectedTiles = GameObject.Find("Tiles");
		for(int i = 0; i<30; i++){
			GameObject tmpG1 = Instantiate(Resources.Load("TileL", typeof(GameObject))) as GameObject;
			tmpG1.transform.parent = collectedTiles.transform.FindChild("Tile_left").transform;
			tmpG1.transform.position = Vector2.zero;
			GameObject tmpG2 = Instantiate(Resources.Load("TileM", typeof(GameObject))) as GameObject;
			tmpG2.transform.parent = collectedTiles.transform.FindChild("Tile_Middle").transform;
			tmpG2.transform.position = Vector2.zero;
			GameObject tmpG3 = Instantiate(Resources.Load("TileR", typeof(GameObject))) as GameObject;
			tmpG3.transform.parent = collectedTiles.transform.FindChild("Tile_Right").transform;
			tmpG3.transform.position = Vector2.zero;
			GameObject tmpG4 = Instantiate(Resources.Load("TileB", typeof(GameObject))) as GameObject;
			tmpG4.transform.parent = collectedTiles.transform.FindChild("Blank").transform;
			tmpG4.transform.position = Vector2.zero;
		}
		for (int i= 0; i<2; i++) {
			GameObject tmpG5 = Instantiate(Resources.Load("Box", typeof(GameObject))) as GameObject;
			tmpG5.transform.parent = collectedTiles.transform.FindChild("Block1").transform;
			tmpG5.transform.position = Vector2.zero;
		}
		
		for (int i= 0; i<2; i++) {
			GameObject tmpG6 = Instantiate(Resources.Load("Barrel_Red", typeof(GameObject))) as GameObject;
			tmpG6.transform.parent = collectedTiles.transform.FindChild("Block2").transform;
			tmpG6.transform.position = Vector2.zero;
		}


		collectedTiles.transform.position = new Vector2 (0.0f, 50.0f);
		
		tilePos = GameObject.Find("First_Tile");
		startUpPosY = tilePos.transform.position.y -2.0f;
		outofbounceX = tilePos.transform.position.x - 2.0f;
		outofBounceY = startUpPosY - 3.0f;
		player = GameObject.Find("Player");
		
		fillScene ();
		startTime = Time.time;
	}
	
	
	
	
	// Update is called once per frame
	void FixedUpdate () 
	{
		
		
		if (startTime - Time.time % 5 == 0) {

			gameSpeed += 0.5f;
		}

		gameLayer.transform.position = new Vector2 (gameLayer.transform.position.x - gameSpeed * Time.deltaTime, 0);
		bgLayer.transform.position = new Vector2 (bgLayer.transform.position.x - gameSpeed/4 * Time.deltaTime, 0);
		
		foreach (Transform child in gameLayer.transform) {
			
			if(child.position.x < outofbounceX){
				
				switch(child.gameObject.name){
					
				case "TileL(Clone)":
					child.gameObject.transform.position = collectedTiles.transform.FindChild("Tile_left").transform.position;
					child.gameObject.transform.parent = collectedTiles.transform.FindChild("Tile_left").transform;
					break;
				case "TileM(Clone)":
					child.gameObject.transform.position = collectedTiles.transform.FindChild("Tile_Middle").transform.position;
					child.gameObject.transform.parent = collectedTiles.transform.FindChild("Tile_Middle").transform;
					break;
				case "TileR(Clone)":
					child.gameObject.transform.position = collectedTiles.transform.FindChild("Tile_Right").transform.position;
					child.gameObject.transform.parent = collectedTiles.transform.FindChild("Tile_Right").transform;
					break;
				case "TileB(Clone)":
					child.gameObject.transform.position = collectedTiles.transform.FindChild("Blank").transform.position;
					child.gameObject.transform.parent = collectedTiles.transform.FindChild("Blank").transform;
					break;
				case "Box(Clone)":
					child.gameObject.transform.position = collectedTiles.transform.FindChild("Block1").transform.position;
					child.gameObject.transform.parent = collectedTiles.transform.FindChild("Block1").transform;
					break;
				case "Barrel_Red(Clone)":
					child.gameObject.transform.position = collectedTiles.transform.FindChild("Block2").transform.position;
					child.gameObject.transform.parent = collectedTiles.transform.FindChild("Block2").transform;
					break;
				/*case "Reward":
					GameObject.Find("Reward").GetComponent<crateScript>().inPlay = false;
					break;*/
					
				default:
					Destroy(child.gameObject);
					break;
					
				}
				
				
			}
			
			
			
			
		}

		
		
		if (gameLayer.transform.childCount < 25)
			spawnTile ();
		
		if (player.transform.position.y < outofBounceY)
			killPlayer ();
		
	}

	private void killPlayer (){
		this.GetComponent<ScoreHandler> ().sendTohighscore ();
		Application.LoadLevel (0);
	}
	
	private	void fillScene()
	{
		//Fill start
		for (int i = 0; i<15; i++)
		{
			setTile("middle");
		}
		setTile("right");
	}
	
	private void setTile(string type)
	{
		switch (type){
		case "left":
			tmpTile = collectedTiles.transform.FindChild("Tile_left").transform.GetChild(0).gameObject;
			break;
		case "middle":
			tmpTile = collectedTiles.transform.FindChild("Tile_Middle").transform.GetChild(0).gameObject;
			break;
		case "right":
			tmpTile = collectedTiles.transform.FindChild("Tile_Right").transform.GetChild(0).gameObject;
			break;
		case "blank":
			tmpTile = collectedTiles.transform.FindChild("Blank").transform.GetChild(0).gameObject;
			break;
		}
		tmpTile.transform.parent = gameLayer.transform;
		tmpTile.transform.position = new Vector3(tilePos.transform.position.x+(tileWidth),startUpPosY+(heightLevel * tileWidth),0);
		tilePos = tmpTile;
		lastTile = type;
	}


	private void spawnTile(){
		
		if (blankCounter > 0) {
			
			setTile("blank");
			blankCounter--;
			return;
			
		}
		if (middleCounter > 0) {

			randomizeBlock();
			randomizeBarrel_Red();
			setTile("middle");
			middleCounter--;
			return;
			
		}
		Barrel_RedAdded = false;
		BoxAdded = false;
		
		if (lastTile == "blank") {
			
			changeHeight();
			setTile("left");
			middleCounter = (int)Random.Range(1,8);
			
		}else if(lastTile =="right"){
			this.GetComponent<ScoreHandler>().Point++;
			blankCounter = (int)Random.Range(1,2);
		}else if(lastTile == "middle"){
			setTile("right");
		}
		
		
	}
	 

	private void changeHeight()
	{
		int newHeightLevel = (int)Random.Range(0,4);
		if(newHeightLevel<heightLevel)
			heightLevel--;
		else if(newHeightLevel>heightLevel)
			heightLevel++;
	}

	private void randomizeBlock(){

		if(BoxAdded){
			return;
		}

		if ((int)Random.Range (0,4)==1){
			GameObject newBlockBox = collectedTiles.transform.FindChild("Block1").transform.GetChild(0).gameObject;
			newBlockBox.transform.parent = gameLayer.transform;
			newBlockBox.transform.position = new Vector2(tilePos.transform.position.x , startUpPosY + (heightLevel+tileWidth + (tileWidth*2)));
		}

	}

	private void randomizeBarrel_Red(){
		
		if(Barrel_RedAdded){
			return;
		}
		
		if ((int)Random.Range (0,4)==1){
			GameObject newBlockBarrel = collectedTiles.transform.FindChild("Block2").transform.GetChild(0).gameObject;
			newBlockBarrel.transform.parent = gameLayer.transform;
			newBlockBarrel.transform.position = new Vector2(tilePos.transform.position.x , startUpPosY + (heightLevel+tileWidth + (tileWidth*2)));
		}
		
	}
	
}

