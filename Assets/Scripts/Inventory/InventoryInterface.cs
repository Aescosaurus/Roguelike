using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryInterface
	:
	MonoBehaviour
{
	void Start()
	{
		GameObject itemPrefab = Resources.Load<GameObject>(
			"Prefabs/Inventory/Item" );
		for( int i = 0; i < Inventory.itemSlots; ++i )
		{
			Instantiate( itemPrefab,transform );
		}
	}
}
