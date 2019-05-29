using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialTriger : MonoBehaviour 
{
    [SerializeField]
    GameObject ActiveObject;

	private void OnTriggerEnter2D(Collider2D collision)
	{
        if (collision.tag == "Player")
        {
            ActiveObject.SetActive(true);
        }
	}

	private void OnTriggerExit2D(Collider2D collision)
	{
        if (collision.tag == "Player")
        {
            ActiveObject.SetActive(false);
        }
	}

}
