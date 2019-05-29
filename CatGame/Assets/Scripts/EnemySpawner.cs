using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class EnemySpawnerResource
{
    public EnemyType _Catagory;
    public Vector3 _SpawnPos;
    public GameObject _Enemy;
}

public class EnemySpawner : MonoBehaviour 
{
    /*
    public List<EnemySpawnerResource> _enemiesList;

	private void OnEnable()
	{
        StartCoroutine(WaitforGamemanagerAwake());
	}

    //WaitForGamManager
    IEnumerator WaitforGamemanagerAwake()
    {
        yield return new WaitUntil(() => GameManager.Instance);
        GameManager.Instance.CallRespawn += Respawn;
    }

	//Respawn
    public void Respawn(GameObject _SpawnerObject)
    {
        if(_SpawnerObject == this.gameObject)
        {
            for (int i = 0; i < _enemiesList.Count; i++)
            {
                if (!_enemiesList[i]._Enemy)
                {
                    _enemiesList[i]._Enemy = Instantiate(GetEnemyPrefab(_enemiesList[i]._Catagory));
                    _enemiesList[i]._Enemy.transform.position = _enemiesList[i]._SpawnPos;
                }
            }
        }
    }

    //FindPrefab
    GameObject fab;
    GameObject GetEnemyPrefab(EnemyType _catagory)
    {
        for (int i = 0; i < GameManager.Instance._GetEnemyPrefab.Count; i++)
        {
            if (GameManager.Instance._GetEnemyPrefab[i]._Catagory == _catagory)
            {
                fab = GameManager.Instance._GetEnemyPrefab[i]._EnemyPrefab;
            }
        }
        return fab;
    }*/
}
