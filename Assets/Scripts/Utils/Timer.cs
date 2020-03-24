using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Timer
{
	public Timer( float duration )
	{
		this.duration = duration;
		curTime = 0.0f;
	}

	public bool Update( float dt )
	{
		if( curTime <= duration ) curTime += dt;

		return( IsDone() );
	}

	public void Reset()
	{
		curTime = 0.0f;
	}

	public bool IsDone()
	{
		return( curTime >= duration );
	}
	public float GetPercent()
	{
		return( Mathf.Min( 1.0f,curTime / duration ) );
	}

	public float duration;
	float curTime;
}
