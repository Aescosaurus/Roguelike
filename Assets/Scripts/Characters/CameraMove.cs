using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove
	:
	MonoBehaviour
{
	void Start()
	{
		cam = Camera.main;
		offset = cam.transform.position - transform.position;
	}

	void Update()
	{
		cam.transform.position = transform.position + offset;
	}

	Camera cam;
	Vector3 offset;
}
