using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine.SceneManagement;

[CustomEditor(typeof(MapPartsName))]
public class MapPartsNameEditor: Editor
{

    MapPartsName _targetScript;

    private void OnEnable()
    {
        _targetScript = (MapPartsName)target;
    }

	public override void OnInspectorGUI()
	{
        base.OnInspectorGUI();

        if(_targetScript._textMesh == null)
        {
            _targetScript._textMesh = _targetScript.GetComponent<TextMesh>();
        }
        else
        {
            _targetScript._textMesh.text = _targetScript.gameObject.name;
        }

        if (!Application.isPlaying)
        {
            //Save
            EditorUtility.SetDirty(_targetScript._textMesh);
            EditorSceneManager.MarkSceneDirty(SceneManager.GetActiveScene());
        }
	}
}
