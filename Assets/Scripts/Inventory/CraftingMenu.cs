using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CraftingMenu
	:
	MonoBehaviour
{
	void Start()
	{
		slot1 = transform.Find( "Item1" ).GetComponent<CraftingSlot>();
		slot2 = transform.Find( "Item2" ).GetComponent<CraftingSlot>();
		inv = FindObjectOfType<Inventory>();
	}

	public void Craft()
	{
		if( slot1.item != null && slot2.item != null )
		{
			var item = Recipes.Craft( slot1.item.item,slot2.item.item );
			if( item != null )
			{
				inv.AddItem( item );
				inv.RemoveItem( slot1.item.item );
				inv.RemoveItem( slot2.item.item );
				slot1.transform.GetChild( 0 )
					.GetComponent<Image>().sprite = null;
				slot2.transform.GetChild( 0 )
					.GetComponent<Image>().sprite = null;
				Destroy( slot1.item.gameObject );
				Destroy( slot2.item.gameObject );
			}
		}
	}

	CraftingSlot slot1;
	CraftingSlot slot2;
	Inventory inv;
}
