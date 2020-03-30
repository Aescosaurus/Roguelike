using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public class DungeonGenerator
	:
	MonoBehaviour
{
	void Awake()
	{
		List<RectI> rooms = new List<RectI>();
		List<LineI> halls = new List<LineI>();

		rooms.Add( new RectI( 0,roomWidth,0,roomHeight ) );

		int lastRoom = 0; // 0=up 1=left 2=right.
		int nRooms = roomCount;
		for( int i = 0; i < nRooms; ++i )
		{
			int rng;
			// if( lastRoom == 0 ) rng = Random.Range( 0,2 + 1 );
			// else if( lastRoom == 1 ) rng = Random.Range( 0,1 + 1 );
			// else rng = Random.Range( 0,10 ) > 5 ? 0 : 2;
			rng = Random.Range( 0,2 + 1 );
			lastRoom = rng;
			var hallPos = rooms[i].GetRandPoint();
			halls.Add( new LineI( hallPos,hallPos ) );
			switch( rng )
			{
				case 0: GenerateRoomUp( rooms ); break;
				case 1: GenerateRoomLeft( rooms ); break;
				default: GenerateRoomRight( rooms ); break;
			}
			GenerateHall( rooms[rooms.Count - 1],rooms[rooms.Count - 2],halls );
		}

		Vector2 minPos = Vector2.one;
		Vector2 maxPos = -Vector2.one;
		foreach( var room in rooms )
		{
			var min = room.GetTopLeft();
			var max = room.GetBotRight();
			if( min.x < minPos.x ) minPos.x = min.x;
			if( min.y < minPos.y ) minPos.y = min.y;
			if( max.x > maxPos.x ) maxPos.x = max.x;
			if( max.y > maxPos.y ) maxPos.y = max.y;
		}

		width = ( int )( maxPos.x - minPos.x ) + 2;
		height = ( int )( maxPos.y - minPos.y ) + 2;
		minPos -= Vector2.one;

		foreach( var room in rooms ) room.MoveBy( -minPos );
		foreach( var hall in halls ) hall.MoveBy( -minPos );

		for( int i = 0; i < width * height; ++i )
		{
			tiles.Add( 1 );
		}

		foreach( var room in rooms )
		{
			for( int y = room.top; y < room.bot; ++y )
			{
				for( int x = room.left; x < room.right; ++x )
				{
					tiles[y * width + x] = 0;
				}
			}
		}

		foreach( var hall in halls )
		{
			foreach( var pos in hall.RLoop() )
			{
				tiles[( int )pos.y * width + ( int )pos.x] = 0;
			}
		}

		for( int y = 0; y < height; ++y )
		{
			for( int x = 0; x < width; ++x )
			{
				if( GetTile( x,y ) > 0 &&
					( ( GetTile( x,y - 1 ) == 0 ||
					GetTile( x,y + 1 ) == 0 ||
					GetTile( x - 1,y ) == 0 ||
					GetTile( x + 1,y ) == 0 ) ||
					Random.Range( 0,100 ) > 50 ) )
				{
					SpawnWall( new Vector2( x,y ) );
				}
			}
		}

		GameObject floor = GameObject.Find( "Floor" );
		floor.transform.position = new Vector3( width / 2,0.0f,height / 2 );
		floor.transform.localScale = new Vector3(
			( float )width * 1.5f,1.0f,( float )height * 1.5f );

		// for( int i = 0; i < enemyCount; ++i )
		// {
		// 	var enemy = Instantiate( enemyPrefabs[Random.Range(
		// 		0,enemyPrefabs.Length )],transform );
		// 	enemy.transform.position = GetRandPos();
		// }
		SpawnEntities( enemyPrefabs,enemyCount );

		// for( int i = 0; i < oreCount; ++i )
		// {
		// 	var ore = Instantiate( orePrefabs[Random.Range(
		// 		0,orePrefabs.Length )],transform );
		// 	ore.transform.position = GetRandPos();
		// }
		SpawnEntities( orePrefabs,oreCount );

		// for( int i = 0; i < forgeCount; ++i )
		// {
		// 	var forge = Instantiate( forgePrefabs[ Random.Range(
		// 		0,forgePrefabs.Length )],transform );
		// 	forge.transform.position = GetRandPos();
		// }
		SpawnEntities( forgePrefabs,forgeCount );
	}

	void GenerateRoomUp( List<RectI> rooms )
	{
		Assert.IsTrue( rooms.Count > 0 );
		var lastRoom = rooms[rooms.Count - 1];

		int bot = lastRoom.top - hallLength;
		int centerX = ( int )lastRoom.GetRandPoint().x;
		int hWidth = roomWidth / 2;
		rooms.Add( new RectI( centerX - hWidth,centerX + hWidth,
			bot - roomHeight,bot ) );
	}
	void GenerateRoomLeft( List<RectI> rooms )
	{
		Assert.IsTrue( rooms.Count > 0 );
		var lastRoom = rooms[rooms.Count - 1];

		int right = lastRoom.left - hallLength;
		int centerY = ( int )lastRoom.GetRandPoint().y;
		int hHeight = roomHeight / 2;
		rooms.Add( new RectI( right - roomWidth,right,
			centerY - hHeight,centerY + hHeight ) );
	}
	void GenerateRoomRight( List<RectI> rooms )
	{
		Assert.IsTrue( rooms.Count > 0 );
		var lastRoom = rooms[rooms.Count - 1];

		int left = lastRoom.right + hallLength;
		int centerY = ( int )lastRoom.GetRandPoint().y;
		int hHeight = roomHeight / 2;
		rooms.Add( new RectI( left,left + roomWidth,
			centerY - hHeight,centerY + hHeight ) );
	}
	void GenerateHall( RectI prevRoom,RectI curRoom,List<LineI> halls )
	{
		var rand1 = prevRoom.GetRandPoint();
		var rand2 = curRoom.GetRandPoint();
		var corner = new Vector2( rand1.x,rand2.y );
		if( Random.Range( 0,100 ) > 50 ) corner = new Vector2( rand2.x,rand1.y );

		halls.Add( new LineI( rand1,corner ) );
		halls.Add( new LineI( corner,rand2 ) );
	}

	void SpawnWall( Vector2 pos )
	{
		var wallPos = new Vector3( pos.x,0.0f,pos.y );
		var randWall = wallPrefabs[Random.Range(
			0,wallPrefabs.Length )];

		for( int i = 0; i < wallHeight; ++i )
		{
			++wallPos.y;
			var wall = Instantiate( randWall,transform );
			wall.transform.position = wallPos;
			int rotations = Random.Range( 0,3 );
			for( int j = 0; j < rotations; ++j )
			{
				wall.transform.Rotate( Vector3.up,90.0f );
			}
		}
	}
	void SpawnEntities( GameObject[] prefabs,RangeI count )
	{
		for( int i = 0; i < count; ++i )
		{
			var entity = Instantiate( prefabs[Random.Range(
				0,prefabs.Length )],transform );
			entity.transform.position = GetRandPos();
			int rotations = Random.Range( 0,3 );
			for( int j = 0; j < rotations; ++j )
			{
				entity.transform.Rotate( Vector3.up,90.0f );
			}
			var entityScr = entity.GetComponent<Entity>();
			if( entityScr != null ) entityScr.UpdatePos();
		}
	}

	public int GetTile( int x,int y )
	{
		if( x < 0 || x >= width ||
			y < 0 || y >= height )
		{
			return( 1 );
		}
		return( tiles[y * width + x] );
	}
	// Return random pos within a room or hallway.
	public Vector3 GetRandPos()
	{
		Vector2 randPos;
		do
		{
			randPos.x = Random.Range( 0,width );
			randPos.y = Random.Range( 0,height );
		}
		while( GetTile( ( int )randPos.x,( int )randPos.y ) > 0 );
		return( new Vector3( randPos.x,1.0f,randPos.y ) );
	}

	List<int> tiles = new List<int>();
	int width;
	int height;
	[SerializeField] GameObject[] wallPrefabs = null;
	[SerializeField] GameObject[] enemyPrefabs = null;
	[SerializeField] GameObject[] orePrefabs = null;
	[SerializeField] GameObject[] forgePrefabs = null;
	[SerializeField] RangeI roomCount = new RangeI( 5,10 );
	[SerializeField] RangeI roomWidth = new RangeI( 3,7 );
	[SerializeField] RangeI roomHeight = new RangeI( 3,7 );
	[SerializeField] RangeI hallLength = new RangeI( 3,5 );
	[SerializeField] RangeI wallHeight = new RangeI( 1,1 );
	[SerializeField] RangeI enemyCount = new RangeI( 2,5 );
	[SerializeField] RangeI oreCount = new RangeI( 7,10 );
	[SerializeField] RangeI forgeCount = new RangeI( 1,2 );
}
