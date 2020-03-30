using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnHandler
	:
	MonoBehaviour
{
	void Start()
	{
		entities.Add( FindObjectOfType<PlayerMove>() );
		entities.AddRange( FindObjectsOfType<Enemy>() );
	}

	void Update()
	{
		if( entities[turn] != null )
		{
			var curEntity = entities[turn];
			if( curEntity.IsMyTurn() )
			{
				curEntity.ProcessTurn();
			}
			else if( !curEntity.IsBusy() )
			{
				if( ++turn >= entities.Count ) turn = 0;
				entities[turn].StartTurn();
			}
		}
		else
		{
			entities.RemoveAt( turn );
		}
	}

	List<Entity> entities = new List<Entity>();
	int turn = 0;
}
