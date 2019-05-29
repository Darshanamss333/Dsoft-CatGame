using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.SceneManagement;
using UnityEditor.SceneManagement;

[CustomEditor(typeof(EnemySpawner))]
public class EnemySpawnerEditor : Editor
{
    /*
    EnemySpawner spawner;

	private void OnEnable()
	{
        spawner = (EnemySpawner)target;
	}

	private void OnSceneGUI()
	{
        if(!Application.isPlaying)
        {
            for (int i = 0; i < spawner._enemiesList.Count; i++)
            {
                Handles.color = Color.cyan;
                spawner._enemiesList[i]._SpawnPos = Handles.FreeMoveHandle(spawner._enemiesList[i]._SpawnPos, Quaternion.identity, 0.5f, Vector3.zero, Handles.CylinderHandleCap);
                Handles.Label(spawner._enemiesList[i]._SpawnPos + new Vector3(0.1f, 0, 0), spawner._enemiesList[i]._Catagory.ToString());

                if (!spawner._enemiesList[i]._Enemy)
                {
                    GameObject _new = Instantiate(GetEnemyPrefab(spawner._enemiesList[i]._Catagory));
                    spawner._enemiesList[i]._Enemy = _new;
                }
                else
                {
                    spawner._enemiesList[i]._Enemy.transform.position = spawner._enemiesList[i]._SpawnPos;
                }
            }

            EditorUtility.SetDirty(spawner);
            EditorSceneManager.MarkSceneDirty(SceneManager.GetActiveScene());
        }
	}

    //FindPrefab
    GameObject fab;
    GameObject GetEnemyPrefab(EnemyType _catagory)
    {
        GameManager _gameManager = GameObject.Find("GameManager").GetComponent<GameManager>(); 
        for (int i = 0; i < _gameManager._GetEnemyPrefab.Count; i++)
        {
            if(_gameManager._GetEnemyPrefab[i]._Catagory == _catagory)
            {
                fab = _gameManager._GetEnemyPrefab[i]._EnemyPrefab;
            }
        }
        return fab;
    }*/
}
