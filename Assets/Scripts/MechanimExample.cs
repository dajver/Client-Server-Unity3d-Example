using UnityEngine;
using System.Collections;

public class MechanimExample : MonoBehaviour
{
	Animator _animator;
	public float Speed;

	void Start ()
	{
		_animator = GetComponent<Animator> ();
	}
	
	// Update is called once per frame
	void Update ()
	{
		_animator.SetFloat ("Speed", Speed);
	}
}
