using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraFollowerScript : MonoBehaviour 
{
    CinemachineConfiner _confiner;

	private void Start()
	{
        _confiner = GetComponent<CinemachineConfiner>();
	}

    public void ChangeBlock(BoxCollider _collider)
    {
        _confiner.m_BoundingVolume = _collider;
    }
}
