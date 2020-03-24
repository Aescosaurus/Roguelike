using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skeleton
	:
	Entity
{
	public override void ProcessTurn()
	{
		Vector3 dir = Vector3.forward;
		int rng = Random.Range( 0,100 );
		if( rng < 25 ) dir = Vector3.back;
		else if( rng < 50 ) dir = Vector3.left;
		else dir = Vector3.right;
		Move( dir );
		EndTurn();
	}
}
