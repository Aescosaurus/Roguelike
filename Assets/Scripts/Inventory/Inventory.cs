using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory
	:
	MonoBehaviour
{
	public void AddItem( Item item )
	{
		items.Add( item );
		UpdateUI();
	}

	void UpdateUI()
	{

	}

	List<Item> items = new List<Item>();
	public const int itemSlots = 9;
}
