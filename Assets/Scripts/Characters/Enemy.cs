using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.Assertions;

public class Enemy
	:
	Entity
{
	class Node : IComparable
	{
		public Node( Vector3 start )
		{
			pos = start;
		}
		public List<Node> CalcNeighbors()
		{
			List<Node> neighbors = new List<Node>();
			neighbors.Add( new Node( pos + Vector3.forward ) );
			neighbors.Add( new Node( pos + Vector3.back ) );
			neighbors.Add( new Node( pos + Vector3.left ) );
			neighbors.Add( new Node( pos + Vector3.right ) );
			return( neighbors );
		}
		public int CompareTo( object rhs )
		{
			var node = rhs as Node;
			return( cost.CompareTo( node.cost ) );
		}
		public Vector3 pos;
		public Node backpath = null;
		public float cost = 0.0f;
	}

	protected override void Start()
	{
		base.Start();

		player = FindObjectOfType<PlayerMove>();
	}

	public override void ProcessTurn()
	{
		var path = GeneratePath( transform.position,player.transform.position );
		if( path.Count > 0 )
		{
			var diff = path[0] - transform.position;
			diff.Normalize();
			var ahead = LookAhead( diff );
			if( ahead == null ) Move( diff );
			else if( ahead.GetComponent<PlayerMove>() != null )
			{
				Attack( diff );
			}
			else
			{
				Move( GetRandDir() );
			}
		}
		else EndTurn();
	}

	List<Vector3> GeneratePath( Vector3 start,Vector3 target )
	{
		List<Vector3> path = new List<Vector3>();
		List<Node> pqueue = new List<Node>();
		List<Vector3> visited = new List<Vector3>();
		pqueue.Add( new Node( start ) );
		visited.Add( pqueue[0].pos );
		while( pqueue.Count > 0 )
		{
			var item = pqueue[0];
			pqueue.RemoveAt( 0 );
			if( item.pos == target )
			{
				var backpath = item;
				while( backpath != null )
				{
					if( backpath.pos != start ) path.Add( backpath.pos );
					backpath = backpath.backpath;
				}
				path.Reverse();
				pqueue.Clear();
			}
			else
			{
				foreach( var neigh in item.CalcNeighbors() )
				{
					var newDist = ( neigh.pos - target ).sqrMagnitude;
					if( !visited.Contains( neigh.pos ) && tilemap.GetTile(
						( int )neigh.pos.x,( int )neigh.pos.z ) == 0 )
					{
						visited.Add( neigh.pos );
						neigh.cost = newDist;
						neigh.backpath = item;
						pqueue.Add( neigh );
						pqueue.Sort();
					}
					else if( newDist < neigh.cost )
					{
						neigh.cost = newDist;
						neigh.backpath = item;
					}
				}
			}
		}
		return( path );
	}

	PlayerMove player;
}
