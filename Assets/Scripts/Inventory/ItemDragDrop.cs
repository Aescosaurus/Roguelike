using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ItemDragDrop
	:
	MonoBehaviour,
	IPointerDownHandler,
	IBeginDragHandler,
	IEndDragHandler,
	IDragHandler
{
	void Start()
	{
		rTrans = GetComponent<RectTransform>();
		origPos = transform.position;
		canv = FindObjectOfType<Canvas>();
	}

	public void OnBeginDrag( PointerEventData eventData )
	{
		origPos = transform.position;
	}

	public void OnDrag( PointerEventData eventData )
	{
		rTrans.anchoredPosition += eventData.delta / canv.scaleFactor;
	}

	public void OnEndDrag( PointerEventData eventData )
	{
		transform.position = origPos;
	}

	public void OnPointerDown( PointerEventData eventData )
	{
	}

	RectTransform rTrans;
	Vector3 origPos;
	Canvas canv;
}
