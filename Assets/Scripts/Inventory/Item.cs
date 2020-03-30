using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class Item
{
	enum Type
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
		name = other.name;
		type = other.type;
		icon = other.icon;
		power = other.power;
		strength = other.strength;
		potency = other.potency;
	}

	[SerializeField] string name = "";
	[SerializeField] Type type = Type.Resource;
	[SerializeField] public Sprite icon = null;
	[SerializeField] int power = -1;
	[SerializeField] int strength = -1;
	[SerializeField] int potency = -1;
}
