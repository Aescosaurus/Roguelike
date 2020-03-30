using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ItemDragDrop
	:
	MonoBehaviour,
	IBeginDragHandler,
	IEndDragHandler,
	IDragHandler
{
	void Start()
	{
		rTrans = GetComponent<RectTransform>();
		origPos = transform.position;
		canv = FindObjectOfType<Canvas>();
		cg = GetComponent<CanvasGroup>();
	}

	public void OnBeginDrag( PointerEventData eventData )
	{
		origPos = transform.position;
		cg.blocksRaycasts = false;
	}

	public void OnDrag( PointerEventData eventData )
	{
		rTrans.anchoredPosition += eventData.delta / canv.scaleFactor;
	}

	public void OnEndDrag( PointerEventData eventData )
	{
		transform.position = origPos;
		cg.blocksRaycasts = true;
	}

	RectTransform rTrans;
	Vector3 origPos;
	Canvas canv;
	public CanvasGroup cg;
	public Item item;
}
