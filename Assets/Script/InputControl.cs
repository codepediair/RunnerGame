using UnityEngine;
using System.Collections;

public class InputControl : MonoBehaviour {

	private bool isMobile = true;
	private playerHandler _player;

	void Start(){
		if (Application.isEditor)
			isMobile = false;

		_player = GameObject.Find ("Player").GetComponent<playerHandler> ();
	}

	// Update is called once per frame
	void Update () {

		 if (isMobile) {
			int tmpC = Input.touchCount;
				tmpC--;
			if(Input.GetTouch(tmpC).phase == TouchPhase.Began){
				handelInteraction(true);
			}
			if(Input.GetTouch(tmpC).phase == TouchPhase.Ended){
				handelInteraction(false);
			}
		
		} else {
			if(Input.GetMouseButton(0)){
				handelInteraction(true);
			}
			
			if(Input.GetMouseButtonUp(0)){
				handelInteraction(false);
			}

		}

	
	}

	void handelInteraction(bool starting){

		if (starting) {
			_player.jump ();
		} else {
		}
	}
}
