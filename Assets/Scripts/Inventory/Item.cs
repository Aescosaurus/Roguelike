using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class Item
{
	public enum Type
	{
		Resource,
		Weapon,
		Armor,
		Potion
	}

	public void UpdateUI( GameObject gameObject )
	{
		gameObject.GetComponent<Image>().sprite = icon;
		var rt = gameObject.GetComponent<RectTransform>();
		rt.sizeDelta = new Vector2( 0.0f,rt.rect.width );
	}

	public void Copy( Item other )
	{
		resType = other.resType;
		type = other.type;
		icon = other.icon;
		power = other.power;
		strength = other.strength;
		potency = other.potency;
	}

	[SerializeField] public ResourceType resType = ResourceType.Cactus;
	[SerializeField] public Type type = Type.Resource;
	[SerializeField] public Sprite icon = null;
	[SerializeField] public int power = -1;
	[SerializeField] public int strength = -1;
	[SerializeField] public int potency = -1;
}
