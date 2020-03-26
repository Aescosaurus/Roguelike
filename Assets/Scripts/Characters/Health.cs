using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health
	:
	MonoBehaviour
{
	public void Damage( int damage )
	{
		hp -= damage;
		if( hp <= 0 )
		{
			print( name + " died." );
			// Destroy( gameObject );
		}
	}

	[SerializeField] int hp;
}
