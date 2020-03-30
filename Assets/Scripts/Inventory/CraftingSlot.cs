using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class CraftingSlot
	:
	MonoBehaviour,
	IDropHandler,
	IPointerDownHandler
{
	public void OnDrop( PointerEventData eventData )
	{
		var obj = eventData.pointerDrag;
		if( obj != null )
		{
			item = obj.GetComponent<ItemDragDrop>();
			transform.GetChild( 0 ).GetComponent<Image>()
				.sprite = item.item.icon;
			item.gameObject.SetActive( false );
			print( item.gameObject.activeSelf );
		}
	}

	public void OnPointerDown( PointerEventData eventData )
	{
		if( item != null )
		{
			item.gameObject.SetActive( true );
			item.cg.blocksRaycasts = true;
			print( item.gameObject.activeSelf );
			item = null;
			transform.GetChild( 0 ).GetComponent<Image>()
				.sprite = null;
		}
	}

	public ItemDragDrop item = null;
}
