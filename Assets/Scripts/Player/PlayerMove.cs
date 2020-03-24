using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove
	:
	Entity
{
	protected override void Update()
	{
		base.Update();

		if( !IsMoving() )
		{
			Vector3 move = new Vector3( Input.GetAxis( "Horizontal" ),
				0.0f,Input.GetAxis( "Vertical" ) );
			if( move.z < 0.0f ) move = Vector3.back;
			else if( move.z > 0.0f ) move = Vector3.forward;
			else if( move.x < 0.0f ) move = Vector3.left;
			else if( move.x > 0.0f ) move = Vector3.right;

			if( move.sqrMagnitude > 0.0f )
			{
				base.Move( move );
			}
		}
	}
}
