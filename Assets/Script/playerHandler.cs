using UnityEngine;
using System.Collections;

public class playerHandler : MonoBehaviour {

	private bool inAir = false;
	private int _animState = Animator.StringToHash("animstat");
	private Animator _animator;

	// Use this for initialization
	void Start () {
		_animator = this.transform.GetComponent<Animator> ();
	
	}
	
	// Update is called once per frame
	void Update () {

		if (!inAir && this.GetComponent<Rigidbody2D>().velocity.y > 1.0f) {
			_animator.SetInteger (_animState, 1);
			inAir = true;
		} else if (inAir && this.GetComponent<Rigidbody2D>().velocity.y == 0.0f) {
			_animator.SetInteger (_animState, 0);
			inAir = false;
		}
	
	}

	public void jump(){

		this.GetComponent<Rigidbody2D>().AddForce (Vector2.up * 5500);
	} 
}
