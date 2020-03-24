using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public class Entity
	:
	MonoBehaviour
{
	protected enum Anim
	{
		Idle = 0,
		Walk,
		Count
	}

	protected virtual void Start()
	{
		anim = GetComponent<Animator>();
		tilemap = FindObjectOfType<DungeonGenerator>();

		for( int i = 0; i < ( int )Anim.Count; ++i )
		{
			anims.Add( Resources.Load<AnimationClip>(
				"Animation/" + ( ( Anim )i ).ToString() ) );
		}

		moveTimer = new Timer( anims[( int )Anim.Walk].length );

		transform.position = tilemap.GetRandPos();
	}

	protected virtual void Update()
	{
		if( moving )
		{
			transform.position = Vector3.Lerp( transform.position,
				newPos,moveTimer.GetPercent() * 0.2f );
			if( moveTimer.Update( Time.deltaTime ) )
			{
				moving = false;
				moveTimer.Reset();
				transform.position = newPos;
			}
		}
	}

	protected void Move( Vector3 dir )
	{
		if( dir.sqrMagnitude > 0.0f )
		{
			Assert.IsTrue( dir.x == 0.0f || dir.y == 0.0f );

			transform.eulerAngles = new Vector3( 0.0f,
				Mathf.Atan2( dir.x,dir.z ) * Mathf.Rad2Deg,0.0f );
			var pos = transform.position + dir;
			if( tilemap.GetTile( ( int )pos.x,( int )pos.z ) == 0 )
			{
				moving = true;
				anim.SetTrigger( "Walk" );
				newPos = transform.position + dir;
			}
		}
	}

	protected bool IsMoving()
	{
		return( moving );
	}

	Animator anim;
	List<AnimationClip> anims = new List<AnimationClip>();
	DungeonGenerator tilemap;

	Vector3 newPos = Vector3.zero;
	bool moving = false;
	Timer moveTimer;
}
