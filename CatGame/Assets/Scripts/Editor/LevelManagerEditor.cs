using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine.SceneManagement;

[CustomEditor(typeof(LevelManager))]
public class LevelManagerEditor : Editor
{

    LevelManager manager;

    private void OnEnable()
    {
        manager = (LevelManager)target;
        //Debug.Log(manager.gameObject.GetInstanceID());
    }

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        if(GUILayout.Button("+"))
        {
            if(manager.Roads.Count != 0)
            {
                manager.Roads.Add(new RoadsRefrence());
            }
        }

        if (GUI.changed)
        {
            EditorUtility.SetDirty(manager);
            EditorSceneManager.MarkSceneDirty(SceneManager.GetActiveScene());
        }

    }

    private void OnSceneGUI()
    {
        for (int i = 0; i < manager.Roads.Count; i++)
        {
            GameObject _01parent = manager.Roads[i]._01Parent;
            GameObject _02parent = manager.Roads[i]._02Parent;
            GameObject _01door = manager.Roads[i]._01Door;
            GameObject _02door = manager.Roads[i]._02Door;

            if (_01door && _02door)
            {
                Handles.color = Color.green;
                Handles.DrawLine(manager.Roads[i]._01Door.transform.position, manager.Roads[i]._02Door.transform.position);
                manager.Roads[i]._01Door.transform.position = Handles.FreeMoveHandle(_01door.transform.position, Quaternion.identity, 1, Vector3.zero, Handles.CircleCap);
                manager.Roads[i]._02Door.transform.position = Handles.FreeMoveHandle(_02door.transform.position, Quaternion.identity, 1, Vector3.zero, Handles.CircleCap);

                //AutoAdd Road Parent
                manager.Roads[i]._01Parent = _01door.transform.parent.gameObject;
                manager.Roads[i]._02Parent = _02door.transform.parent.gameObject;
                if(manager.Roads[i]._01Door && manager.Roads[i]._02Door)
                {
                    manager.Roads[i]._RoadName = _01parent.name + "-" + _02parent.name;
                    Handles.Label(_01door.transform.position + ((_02door.transform.position - _01door.transform.position) / 2), manager.Roads[i]._RoadName);

                    //SetDoore Triger Veriables
                    _01door.GetComponent<Door_Trigger>()._OtherSideparent = _02parent;
                    _01door.GetComponent<Door_Trigger>()._OtherSideDoor = _02door;
                    _02door.GetComponent<Door_Trigger>()._OtherSideparent = _01parent;
                    _02door.GetComponent<Door_Trigger>()._OtherSideDoor = _01door;
                }


                //Rotate Door
                if (Handles.Button(_01door.transform.position + new Vector3(0, 1, 0), Quaternion.identity, .5f, .5f, Handles.CylinderCap))
                {
                    _01door.transform.eulerAngles = _01door.transform.eulerAngles + new Vector3(0, 0, -90);
                }
                if (Handles.Button(_02door.transform.position + new Vector3(0, 1, 0), Quaternion.identity, .5f, .5f, Handles.CylinderCap))
                {
                    _02door.transform.eulerAngles = _02door.transform.eulerAngles + new Vector3(0, 0, -90);
                }
            }

            if(!Application.isPlaying)
            {
                //Save
                if (manager.Roads[i]._01Door && manager.Roads[i]._02Door)
                {
                    EditorUtility.SetDirty(_01door.GetComponent<Door_Trigger>());
                    EditorUtility.SetDirty(_02door.GetComponent<Door_Trigger>());
                }

                EditorSceneManager.MarkSceneDirty(SceneManager.GetActiveScene());
            }

        }


    }
}
