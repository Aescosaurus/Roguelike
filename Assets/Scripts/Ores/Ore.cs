using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ore
	:
	MonoBehaviour
{
	void Start()
	{
		anim = GetComponentInChildren<Animator>();
	}

	public void Attack()
	{
		--hp;
		anim.SetTrigger( "Harvest" );
		transform.localScale *= 0.8f;
		if( hp <= 0 )
		{
			Destroy( gameObject );
		}
	}

	Animator anim;
	int hp = 3;
}
