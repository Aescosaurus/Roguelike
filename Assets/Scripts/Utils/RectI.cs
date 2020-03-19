using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public class RectI
{
	public RectI( int left,int right,int top,int bot )
	{
		this.left = left;
		this.right = right;
		this.top = top;
		this.bot = bot;

		Fix();

		Assert.IsTrue( left <= right );
		Assert.IsTrue( top <= bot );
	}

	void Fix()
	{
		if( right < left )
		{
			var temp = left;
			left = right;
			right = temp;
		}
		if( bot < top )
		{
			var temp = top;
			top = bot;
			bot = temp;
		}
	}

	public void MoveBy( Vector2 amount )
	{
		left += ( int )amount.x;
		right += ( int )amount.x;
		top += ( int )amount.y;
		bot += ( int )amount.y;
	}

	public RectI GetExpanded( int amount )
	{
		return( new RectI( left - amount,right + amount,
			top + amount,bot - amount ) );
	}

	public Vector2 GetTopLeft()
	{
		return( new Vector2( left,top ) );
	}

	public Vector2 GetBotRight()
	{
		return( new Vector2( right,bot ) );
	}

	public Vector2 GetRandPoint()
	{
		return ( new Vector2( Random.Range( left,right ),
			Random.Range( top,bot ) ) );
	}

	public IEnumerable<Vector2> RTop()
	{
		Vector2 temp = new Vector2( 0,top );
		for( int x = left; x < right; ++x )
		{
			temp.x = x;
			yield return ( temp );
		}
	}

	public IEnumerable<Vector2> RBot()
	{
		Vector2 temp = new Vector2( 0,bot );
		for( int x = left; x < right; ++x )
		{
			temp.x = x;
			yield return ( temp );
		}
	}

	public IEnumerable<Vector2> RLeft()
	{
		Vector2 temp = new Vector2( left,0 );
		for( int y = bot; y < top; ++y )
		{
			temp.y = y;
			yield return ( temp );
		}
	}

	public IEnumerable<Vector2> RRight()
	{
		Vector2 temp = new Vector2( right,0 );
		for( int y = bot; y < top; ++y )
		{
			temp.y = y;
			yield return ( temp );
		}
	}

	public override string ToString()
	{
		return( left + " " + right + " " +
			top + " " + bot );
	}

	public bool ContainsPoint( Vector2 point )
	{
		return( point.x < left && point.x < right &&
			point.y > bot && point.y < top );
	}

	public int left;
	public int right;
	public int top;
	public int bot;
}
