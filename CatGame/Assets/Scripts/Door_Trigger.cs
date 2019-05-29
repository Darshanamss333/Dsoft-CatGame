using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door_Trigger : MonoBehaviour 
{
    public GameObject _OtherSideparent;
    public GameObject _OtherSideDoor;

	private void OnTriggerEnter2D(Collider2D collision)
	{
        if (collision.tag == "Player" && GameManager.Instance._doorEnterProcesing == false)
        {
            GameManager.Instance.PlayerEnterDoore(transform.parent.gameObject,_OtherSideparent,_OtherSideDoor);
        }
	}
}
