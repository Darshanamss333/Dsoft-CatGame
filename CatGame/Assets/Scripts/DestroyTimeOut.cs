using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyTimeOut : MonoBehaviour 
{
    public float delaySec;

	private void Start()
	{
        StartCoroutine(wait());
	}

    IEnumerator wait()
    {
        yield return new WaitForSeconds(delaySec);
        DestroyImmediate(this.gameObject);
        //return null;
    }
}
