using UnityEngine;
using System.Collections;

public class Move : MonoBehaviour
{
	public int speed = 5;
	public int gravity = 5;
	private CharacterController cc;
	
	void Start ()
	{
		cc = GetComponent<CharacterController> ();
	}
	
	void Update ()
	{
		if (cc != null) {
			if (networkView.isMine) {
				if (Input.GetKey ("d")) {
					animation.CrossFade("right");
					cc.Move (new Vector3 (Input.GetAxis ("Horizontal") * speed * Time.deltaTime, -gravity * Time.deltaTime, Input.GetAxis ("Vertical") * speed * Time.deltaTime));
				}else if (Input.GetKey ("a")) {
					animation.CrossFade("left");
					cc.Move (new Vector3 (Input.GetAxis ("Horizontal") * speed * Time.deltaTime, -gravity * Time.deltaTime, Input.GetAxis ("Vertical") * speed * Time.deltaTime));
				}else if (Input.GetKey ("w")) {
					animation.CrossFade("forward");
					cc.Move (new Vector3 (Input.GetAxis ("Horizontal") * speed * Time.deltaTime, -gravity * Time.deltaTime, Input.GetAxis ("Vertical") * speed * Time.deltaTime));
				} else if (Input.GetKey ("s")) {
					animation.CrossFade("back");	
					cc.Move (new Vector3 (Input.GetAxis ("Horizontal") * speed * Time.deltaTime, -gravity * Time.deltaTime, Input.GetAxis ("Vertical") * speed * Time.deltaTime));
				} else {
					animation.CrossFade("idle");
				}
			}
		}
	}
}
