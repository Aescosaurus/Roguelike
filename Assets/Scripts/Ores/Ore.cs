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
	}

	Animator anim;
	int hp = 3;
}
