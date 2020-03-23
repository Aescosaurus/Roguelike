using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove
	:
	MonoBehaviour
{
	void Start()
	{
		anim = GetComponent<Animator>();
		var walkAnim = Resources.Load<AnimationClip>( "Animation/Walk" );
		moveTimer = new Timer( walkAnim.length );
	}

	void Update()
	{
		if( !moving )
		{
			Vector3 move = new Vector3( Input.GetAxis( "Horizontal" ),
				0.0f,Input.GetAxis( "Vertical" ) );
			if( move.z < 0.0f ) move = Vector3.back;
			else if( move.z > 0.0f ) move = Vector3.forward;
			else if( move.x < 0.0f ) move = Vector3.left;
			else if( move.x > 0.0f ) move = Vector3.right;

			if( move.sqrMagnitude > 0.0f )
			{
				moving = true;
				transform.eulerAngles = new Vector3( 0.0f,
					Mathf.Atan2( move.x,move.z ) * Mathf.Rad2Deg,0.0f );
				anim.SetTrigger( "Walk" );
				newPos = transform.position + move;
			}
		}
		else
		{
			transform.position = Vector3.Lerp( transform.position,
				newPos,moveTimer.GetPercent() * 0.2f );
			if( moveTimer.Update( Time.deltaTime ) )
			{
				moving = false;
				moveTimer.Reset();
				transform.position = newPos;
			}
		}
	}

	Animator anim;
	bool moving = false;
	Vector3 newPos = Vector3.zero;
	Timer moveTimer;
}
