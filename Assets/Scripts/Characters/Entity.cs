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
		Harvest,
		Count
	}

	protected virtual void Start()
	{
		anim = GetComponent<Animator>();
		tilemap = FindObjectOfType<DungeonGenerator>();
		mesh = GetComponentInChildren<MeshRenderer>();
		cam = Camera.main;

		for( int i = 0; i < ( int )Anim.Count; ++i )
		{
			anims.Add( Resources.Load<AnimationClip>(
				"Animation/Character/" + ( ( Anim )i ).ToString() ) );
		}

		moveTimer = new Timer( anims[( int )Anim.Walk].length );

		// transform.position = tilemap.GetRandPos();
		Assert.IsNotNull( transform.Find( "Model" ) );
	}

	void Update()
	{
		if( moving )
		{
			transform.position = Vector3.Lerp( transform.position,
				newPos,moveTimer.GetPercent() * 0.2f );
			if( moveTimer.Update( Time.deltaTime ) ||
				/*!mesh.isVisible*/
				( cam.transform.position - transform.position )
				.sqrMagnitude > drawDist * drawDist )
			{
				moving = false;
				moveTimer.Reset();
				transform.position = newPos;
				// anim.ResetTrigger( "Walk" );
				// for( int i = 0; i < ( int )Anim.Count; ++i )
				// {
				// 	anim.ResetTrigger( ( ( Anim )i ).ToString() );
				// }
				// anim.Play( "Idle" );
				PlayAnim( Anim.Idle );
				EndTurn();
			}
		}
		else if( harvesting )
		{
			if( moveTimer.Update( Time.deltaTime ) )
			{
				harvesting = false;
				moveTimer.Reset();
				PlayAnim( Anim.Idle );
				EndTurn();
			}
		}
	}

	public virtual void ProcessTurn() {}

	protected void Move( Vector3 dir )
	{
		Assert.IsTrue( dir.y == 0.0f );
		Assert.IsTrue( dir.x == 0.0f || dir.z == 0.0f );
		Assert.IsTrue( dir.x != 0.0f || dir.z != 0.0f );

		var objAhead = LookAhead( dir );
		transform.eulerAngles = new Vector3( 0.0f,
			Mathf.Atan2( dir.x,dir.z ) * Mathf.Rad2Deg,0.0f );

		if( objAhead == null )
		{
			var pos = transform.position + dir;
			if( tilemap.GetTile( ( int )pos.x,( int )pos.z ) == 0 )
			{
				moving = true;
				// anim.SetTrigger( "Walk" );
				PlayAnim( Anim.Walk );
				newPos = transform.position + dir;
			}
		}
	}
	protected void Harvest( Vector3 dir )
	{
		transform.eulerAngles = new Vector3( 0.0f,
			Mathf.Atan2( dir.x,dir.z ) * Mathf.Rad2Deg,0.0f );
		PlayAnim( Anim.Harvest );
		harvesting = true;
	}

	public void StartTurn()
	{
		myTurn = true;
	}
	public void EndTurn()
	{
		myTurn = false;
	}
	protected void PlayAnim( Anim a )
	{
		for( int i = 0; i < ( int )Anim.Count; ++i )
		{
			anim.ResetTrigger( ( ( Anim )i ).ToString() );
		}
		anim.SetTrigger( a.ToString() );
	}

	public bool IsBusy()
	{
		return( moving || harvesting );
	}
	public bool IsMyTurn()
	{
		return( myTurn );
	}
	protected GameObject LookAhead( Vector3 dir )
	{
		var boxen = Physics.OverlapBox( transform.position + dir,
			Vector3.one * 0.2f );
		GameObject actualBox = null;
		foreach( var box in boxen )
		{
			actualBox = box.gameObject;
		}
		return( actualBox );
	}
	protected Vector3 GetRandDir()
	{
		int randX = 0;
		int randY = 0;
		while( ( randX == 0 && randY == 0 ) ||
			( randX != 0 && randY != 0 ) )
		{
			randX = Random.Range( -1,2 );
			randY = Random.Range( -1,2 );
		}
		return( new Vector3( randX,0.0f,randY ) );
	}

	Animator anim;
	List<AnimationClip> anims = new List<AnimationClip>();
	protected DungeonGenerator tilemap;
	MeshRenderer mesh;
	Camera cam;

	Vector3 newPos = Vector3.zero;
	bool moving = false;
	bool harvesting = false;
	Timer moveTimer;
	bool myTurn = false;
	const float drawDist = 9.0f;
}
