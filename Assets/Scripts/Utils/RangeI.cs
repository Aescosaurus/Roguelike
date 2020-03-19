using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class RangeI
{
	public RangeI( int min,int max )
	{
		this.min = min;
		this.max = max;
	}

	public static implicit operator int( RangeI rhs )
	{
		return( rhs.GetInt() );
	}

	public int GetInt()
	{
		return( Random.Range( min,max ) );
	}

	public int min;
	public int max;
}
