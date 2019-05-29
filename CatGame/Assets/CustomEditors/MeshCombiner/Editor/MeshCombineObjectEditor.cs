using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine.SceneManagement;
using UnityEngine.U2D;

[CustomEditor(typeof(MeshCombineObject))]
public class MeshCombineObjectEditor:Editor
{
    MeshCombineObject _Target;

	private void OnEnable()
	{
        _Target = (MeshCombineObject)target;
        _Target._MeshObject = _Target.GetComponent<MeshFilter>().mesh;
        _Target.GetComponent<MeshRenderer>().material = _Target._MeshMaterial;

        //HideDefalt Transform Handle
        Tools.hidden = true;
	}

	private void OnDisable()
	{
        //UnhideDefalt Transform Handle
        Tools.hidden = false;
	}

	public override void OnInspectorGUI()
	{
        base.OnInspectorGUI();

        //AddNewObject
        if(GUILayout.Button("+"))
        {
            _Target.CreateNewObjectChild();
        }

        //RenderMesh
        _Target.RenderMesh();


        if(GUI.changed)
        {
            //Save
            Undo.RecordObjects(this.targets, "Inspector");
            EditorUtility.SetDirty(_Target);
            EditorSceneManager.MarkSceneDirty(SceneManager.GetActiveScene()); 
        }

	}


	private void OnSceneGUI()
	{

        //LockTransform
        _Target.transform.position = Vector3.zero;

        for (int ci = 0; ci < _Target._meshObjectDatas.Count; ci++)
        {
            if(ci == _selectedObjectIndex)
            {
                Handles.color = Color.green;
                EditorGUI.BeginChangeCheck();

                if (Tools.current == Tool.Move)
                {
                    //MoveSprite
                    Vector3 _deltaPos = Handles.PositionHandle(_Target._meshObjectDatas[ci]._Position, Quaternion.identity);
                    if (EditorGUI.EndChangeCheck())
                    {
                        Undo.RecordObject(target, "Pos");
                        _Target._meshObjectDatas[ci]._Position = _deltaPos;

                        EditorUtility.SetDirty(target);
                        //EditorSceneManager.MarkSceneDirty(SceneManager.GetActiveScene());
                    }
                }
                else if (Tools.current == Tool.Scale)
                {
                    Vector3 _deltaSize = Handles.ScaleHandle(_Target._meshObjectDatas[ci]._Size, _Target._meshObjectDatas[ci]._Position, Quaternion.identity, 2);
                    if (EditorGUI.EndChangeCheck())
                    {
                        Undo.RecordObject(target, "Siz");
                        _Target._meshObjectDatas[ci]._Size = _deltaSize;

                        EditorUtility.SetDirty(target);
                    }
                }
                else if (Tools.current == Tool.View)
                {
                    //ChangeSprite
                    if (_Target._Meshsprites.Length > 0)
                    {
                        if (Handles.Button(_Target._meshObjectDatas[ci]._Position + new Vector3(.8f, 0, 0), Quaternion.identity, 0.2f, 0.2f, Handles.RectangleCap))
                        {
                            _Target._meshObjectDatas[ci]._MeshIndex = Mathf.Clamp(_Target._meshObjectDatas[ci]._MeshIndex + 1, 0, _Target._Meshsprites.Length - 1);
                        }
                        if (Handles.Button(_Target._meshObjectDatas[ci]._Position + new Vector3(-.8f, 0, 0), Quaternion.identity, 0.2f, 0.2f, Handles.RectangleCap))
                        {
                            _Target._meshObjectDatas[ci]._MeshIndex = Mathf.Clamp(_Target._meshObjectDatas[ci]._MeshIndex - 1, 0, _Target._Meshsprites.Length - 1);
                        }
                    }

                    Handles.color = Color.red;
                    //RemoveSprite
                    if (Handles.Button(_Target._meshObjectDatas[ci]._Position + new Vector3(1, 1, 0), Quaternion.identity, 0.2f, 0.2f, Handles.CircleCap))
                    {
                        _Target._meshObjectDatas.RemoveAt(ci);
                    }
                }


                //HighLight MouseOver
                Handles.color = Color.red;
                Handles.DrawLine(_Target._meshObjectDatas[ci]._Verticals[0], _Target._meshObjectDatas[ci]._Verticals[1]);
                Handles.DrawLine(_Target._meshObjectDatas[ci]._Verticals[1], _Target._meshObjectDatas[ci]._Verticals[2]);
                Handles.DrawLine(_Target._meshObjectDatas[ci]._Verticals[2], _Target._meshObjectDatas[ci]._Verticals[3]);
                Handles.DrawLine(_Target._meshObjectDatas[ci]._Verticals[3], _Target._meshObjectDatas[ci]._Verticals[0]);
            }
            else
            {
                //HighLight AllSprites
                Handles.color = Color.gray;
                Handles.DrawLine(_Target._meshObjectDatas[ci]._Verticals[0], _Target._meshObjectDatas[ci]._Verticals[1]);
                Handles.DrawLine(_Target._meshObjectDatas[ci]._Verticals[1], _Target._meshObjectDatas[ci]._Verticals[2]);
                Handles.DrawLine(_Target._meshObjectDatas[ci]._Verticals[2], _Target._meshObjectDatas[ci]._Verticals[3]);
                Handles.DrawLine(_Target._meshObjectDatas[ci]._Verticals[3], _Target._meshObjectDatas[ci]._Verticals[0]);
            }


            SelectSprite();

        }
    }

    MeshCollider _collider;
    int _selectedObjectIndex;
    private void SelectSprite()
    {
        
        if (!_collider)
        {
            _collider = _Target.gameObject.AddComponent<MeshCollider>();
        }

        //Select
        Event _e = Event.current;
        Vector3 _mousePos = _e.mousePosition;
        Ray ray = HandleUtility.GUIPointToWorldRay(_mousePos);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, 500))
        {
            if (_collider)
            {
                Vector3 _DeltaSelectPos = hit.point;

                //ChackDataObject
                for (int i = 0; i < _Target._meshObjectDatas.Count; i++)
                {
                    Vector3 _0 = _Target._meshObjectDatas[i]._Verticals[0];
                    Vector3 _1 = _Target._meshObjectDatas[i]._Verticals[1];
                    Vector3 _2 = _Target._meshObjectDatas[i]._Verticals[2];
                    Vector3 _3 = _Target._meshObjectDatas[i]._Verticals[3];

                    /*
                     * 0,1
                     * 3,2
                    */

                    if (_DeltaSelectPos.x > _0.x && _DeltaSelectPos.y < _0.y && _DeltaSelectPos.x < _1.x && _DeltaSelectPos.y < _1.y && _DeltaSelectPos.x < _2.x && _DeltaSelectPos.y > _2.y && _DeltaSelectPos.x > _3.x && _DeltaSelectPos.y > _3.y)
                    {
                        if (_e.type == EventType.MouseDown && _e.button == 1)
                        {
                            _selectedObjectIndex = i;
                        }

                        if(_selectedObjectIndex != i)
                        {
                            //HighLight
                            Handles.color = Color.green;
                            Handles.DrawLine(_0, _1);
                            Handles.DrawLine(_1, _2);
                            Handles.DrawLine(_2, _3);
                            Handles.DrawLine(_3, _0);
                        }
                    }

                }

            }
        }

        DestroyImmediate(_collider);
    }
}
