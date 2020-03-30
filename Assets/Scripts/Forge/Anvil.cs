using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Anvil
	:
	MonoBehaviour
{
	void Start()
	{
		craftingMenu = GameObject.Find( "Canvas" )
			.transform.Find( "Crafting Menu" ).gameObject;
	}

	public void BeginCrafting()
	{
		craftingMenu.SetActive( true );
	}

	GameObject craftingMenu;
}
