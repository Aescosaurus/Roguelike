using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory
	:
	MonoBehaviour
{
	void Start()
	{
		itemPrefab = Resources.Load<GameObject>(
			"Prefabs/Inventory/Item" );
	}

	public void AddItem( Item item )
	{
		if( items.Count < itemSlots )
		{
			items.Add( item );
			var slot = Instantiate( itemPrefab,transform );
			var itemScr = slot.GetComponent<ItemDragDrop>();
			itemScr.item = item;
			StartCoroutine( UpdateItemUI( slot,itemScr ) );
			// slot.GetComponent<Item>().Copy( item );
		}
	}

	IEnumerator UpdateItemUI( GameObject slot,ItemDragDrop itemScr )
	{
		yield return( new WaitForEndOfFrame() );
		itemScr.item.UpdateUI( slot );
	}

	List<Item> items = new List<Item>();
	public const int itemSlots = 9;
	GameObject itemPrefab;
}
