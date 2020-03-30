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
		Attack,
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
		transform.position = Vector3.Lerp( transform.position,
			newPos,moveTimer.GetPercent() * 0.2f );

		if( ( moving || harvesting || attacking ) &&
			( moveTimer.Update( Time.deltaTime ) ||
			( cam.transform.position - transform.position )
			.sqrMagnitude > drawDist * drawDist ) )
		{
			moveTimer.Reset();
			PlayAnim( Anim.Idle );

			if( moving )
			{
				transform.position = newPos;
			}
			else if( harvesting )
			{
				if( targetObj != null )
				{
					// TODO: Give resources.
				}
			}
			else if( attacking )
			{
				var healthScr = targetObj.GetComponent<Health>();
				if( healthScr != null )
				{
					// TODO: More complex damage calc.
					healthScr.Damage( 1 );
				}
			}

			moving = false;
			harvesting = false;
			attacking = false;
			EndTurn();
		}
	}

	public virtual void ProcessTurn() { }

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
				PlayAnim( Anim.Walk );
				targetObj = objAhead;
				moving = true;
				newPos = transform.position + dir;
			}
		}
	}
	protected void Harvest( Vector3 dir )
	{
		var objAhead = LookAhead( dir );
		if( objAhead != null )
		{
			transform.eulerAngles = new Vector3( 0.0f,
				Mathf.Atan2( dir.x,dir.z ) * Mathf.Rad2Deg,0.0f );
			PlayAnim( Anim.Harvest );
			targetObj = objAhead;
			harvesting = true;
		}
	}
	protected void Attack( Vector3 dir )
	{
		var objAhead = LookAhead( dir );
		if( objAhead != null )
		{
			transform.eulerAngles = new Vector3( 0.0f,
				Mathf.Atan2( dir.x,dir.z ) * Mathf.Rad2Deg,0.0f );
			var hpScr = objAhead.GetComponent<Health>();
			if( hpScr != null )
			{
				PlayAnim( Anim.Attack );
				targetObj = objAhead;
				attacking = true;
			}
		}
	}

	public void StartTurn()
	{
		myTurn = true;
	}
	public void EndTurn()
	{
		myTurn = false;
	}
	public void UpdatePos()
	{
		newPos = transform.position;
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
		return ( moving || harvesting || attacking );
	}
	public bool IsMyTurn()
	{
		return ( myTurn );
	}
	protected GameObject LookAhead( Vector3 dir )
	{
		var boxen = Physics.OverlapBox( transform.position + dir,
			Vector3.one * 0.2f );
		GameObject actualBox = null;
		foreach( var box in boxen )
		{
			if( box.gameObject != gameObject ) actualBox = box.gameObject;
		}
		return ( actualBox );
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
		return ( new Vector3( randX,0.0f,randY ) );
	}

	Animator anim;
	List<AnimationClip> anims = new List<AnimationClip>();
	protected DungeonGenerator tilemap;
	MeshRenderer mesh;
	Camera cam;
	GameObject targetObj = null;

	Vector3 newPos;
	bool moving = false;
	bool harvesting = false;
	bool attacking = false;
	Timer moveTimer;
	bool myTurn = false;
	const float drawDist = 9.0f;
}
