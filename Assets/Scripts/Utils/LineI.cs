using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineI
{
	public LineI( Vector2 start,Vector2 end )
	{
		this.start = start;
		this.end = end;
	}

	public void MoveBy( Vector2 amount )
	{
		start += amount;
		end += amount;
	}

	public IEnumerable<Vector2> RLoop()
	{
		Vector2 temp = start;
		Vector2 diff = ( end - start ).normalized;
		int n = ( int )( end - start ).magnitude;
		for( Vector2 pos = start; n > 0; pos += diff,--n )
		{
			yield return( pos );
		}
	}

	public override string ToString()
	{
		return( start + " " + end );
	}

	public Vector2 start;
	public Vector2 end;
}
