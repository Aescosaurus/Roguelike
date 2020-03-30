using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Recipes
{
	class Recipe
	{
		public Recipe( ResourceType item1,ResourceType item2,
			Item.Type equipType )
		{
			this.item1 = item1;
			this.item2 = item2;
			this.equipType = equipType;
		}
		public bool Match( ResourceType item1,ResourceType item2 )
		{
			return( ( item1 == this.item1 && item2 == this.item2 ) ||
				( item1 == this.item2 && item2 == this.item1 ) );
		}
		ResourceType item1;
		ResourceType item2;
		public Item.Type equipType;
	}

	public static Item Craft( Item item1,Item item2 )
	{
		foreach( var rec in recipeList )
		{
			if( rec.Match( item1.resType,item2.resType ) )
			{
				Item equip = new Item();
				equip.type = rec.equipType;
				equip.power = Mathf.Max( item1.power,item2.power );
				equip.strength = Mathf.Max( item1.strength,item2.strength );
				equip.potency = Mathf.Max( item1.potency,item2.potency );
				return ( equip );
			}
		}
		
		return( null );
	}

	static List<Recipe> recipeList = new List<Recipe>()
	{
		new Recipe( ResourceType.Cactus,ResourceType.Lapis,Item.Type.Armor ),
		new Recipe( ResourceType.Cactus,ResourceType.Bone,Item.Type.Potion ),
		new Recipe( ResourceType.Lapis,ResourceType.Bone,Item.Type.Weapon )
	};
}
