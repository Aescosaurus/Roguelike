using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove
	:
	Entity
{
	protected override void Start()
	{
		base.Start();

		transform.position = tilemap.GetRandPos();
	}

	public override void ProcessTurn()
	{
		if( !IsBusy() )
		{
			Vector3 move = new Vector3( Input.GetAxis( "Horizontal" ),
				0.0f,Input.GetAxis( "Vertical" ) );
			if( move.z < 0.0f ) move = Vector3.back;
			else if( move.z > 0.0f ) move = Vector3.forward;
			else if( move.x < 0.0f ) move = Vector3.left;
			else if( move.x > 0.0f ) move = Vector3.right;

			if( move.sqrMagnitude > 0.0f )
			{
				var objAhead = LookAhead( move );
				Move( move );

				if( objAhead != null )
				{
					var oreScr = objAhead.GetComponent<Ore>();
					if( oreScr != null )
					{
						Harvest( move );
						oreScr.Attack();
					}
					var enemyScr = objAhead.GetComponent<Entity>();
					if( enemyScr != null )
					{
						Attack( move );
					}
				}
			}
		}
	}
}
