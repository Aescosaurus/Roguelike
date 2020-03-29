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
		inv = FindObjectOfType<Inventory>();
	}

	public void Attack()
	{
		--hp;
		anim.SetTrigger( "Harvest" );
		transform.localScale *= 0.8f;
		if( hp <= 0 )
		{
			inv.AddItem( item );
			Destroy( gameObject );
		}
	}

	Animator anim;
	Inventory inv;

	[SerializeField] Item item = null;

	int hp = 3;
}
