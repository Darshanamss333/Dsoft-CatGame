using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour 
{

    [SerializeField]
    GameObject _hitParticleObj;
	private void OnTriggerEnter2D(Collider2D collision)
	{
        if (collision.gameObject.tag == "Player")
        {
            GameManager.Instance.TemporyUnlockData.PlayerCash += 1;

            //ParticleCreat
            GameObject _newPar = Instantiate(_hitParticleObj);
            _newPar.transform.position = transform.position;

            Destroy(this.gameObject);
        }
	}
}
