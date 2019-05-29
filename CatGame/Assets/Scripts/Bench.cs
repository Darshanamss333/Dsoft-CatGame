using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bench : MonoBehaviour 
{
	private void Start()
	{
        StartCoroutine(WaitforGamemanagerAwake());
	}

    //WaitForGamManager
    IEnumerator WaitforGamemanagerAwake()
    {
        yield return new WaitUntil(() => GameManager.Instance);
        GameManager.Instance.CallActionWhenLeftJoyDown += Interact;
    }

    void Interact(int _pressDir)
    {
        if(_pressDir == 0 && TrigerStay)
        {
            GameManager.Instance.EnterToBench(this.gameObject);

            //ParticleCreat
            GameObject _newPar = Instantiate(_saveParticleObject);
            _newPar.transform.position = this.transform.position;
        }
    }
    bool TrigerStay;
    [SerializeField]
    ParticleSystem _enterParticle;
    [SerializeField]
    GameObject _saveParticleObject;
	private void OnTriggerEnter2D(Collider2D collision)
	{
        TrigerStay = true;
        _enterParticle.Play();
	}
	private void OnTriggerExit2D(Collider2D collision)
	{
        TrigerStay = false;
	}
}
