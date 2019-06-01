using UnityEngine;
using System.Collections;

public class ScoreHandler : MonoBehaviour {

	private int Score = 0;
	private int BestScore;


	// Use this for initialization
	void Start () {

		BestScore = gethighscorefromDB();
	
	}


	// Update is called once per frame
	void Update () {
	
	}

	void OnGUI(){

		GUI.color = Color.red;
		GUIStyle style = GUI.skin.GetStyle("Label");
		style.alignment = TextAnchor.UpperRight;
		style.fontSize = 50;
		GUI.Label (new Rect (20, 20, 200, 200), Score.ToString (), style);
		style.alignment = TextAnchor.UpperLeft;
		//GUI.Label (new Rect (Screen.width - 220, 20, 200, 200), "HighScore:" + BestScore.ToString (), style);

	}
	 public int Point{
		get{return Score;}
		set {Score=value;}
	}

	static string mr7sum(string str){

		str += GameObject.Find ("xxmr7").transform.GetChild(0).name;
		System.Security.Cryptography.MD5 h = System.Security.Cryptography.MD5.Create ();
		byte[] data = h.ComputeHash (System.Text.Encoding.Default.GetBytes (str));

		System.Text.StringBuilder sb = new System.Text.StringBuilder ();

		for (int i=0; i<data.Length; i++) {

			sb.Append(data[i].ToString("X2"));
		}
		return sb.ToString ();	
	}

	public void saveVal(int val){
		string tmpV = mr7sum (val.ToString ());
		PlayerPrefs.SetString ("score_Hash", tmpV);
		PlayerPrefs.SetInt ("Score", val);
	}

	private int dec(string val){
		int tmpV = 0;
		if (val == "") {
			saveVal (tmpV);
		} else {
			if (val.Equals (mr7sum (PlayerPrefs.GetInt ("score").ToString ()))) {
				tmpV = PlayerPrefs.GetInt ("score");
			} else {
				saveVal (0);
			}
		}
		return tmpV;
	}

	private int gethighscorefromDB(){

		return dec (PlayerPrefs.GetString ("score_Hash"));
	}

	public void sendTohighscore(){

		if (Score > BestScore) {
			saveVal(Score);
		}
	}

}
