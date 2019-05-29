using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.U2D;

[System.Serializable]
public class MeshObjectData
{
    [SerializeField]
    public Vector3 _Position;
    public Vector3 _Size;
    public Vector3[] _Verticals;
    public int[] _Trangles;
    public Vector2[] _Uvs;
    public Sprite _Sprite;
    public int _MeshIndex;

    public MeshObjectData( Vector3 pos,int index)
    {
        _Verticals = new Vector3[] { new Vector3(0, 1, 0), new Vector3(1, 1, 0), new Vector3(1, 0, 0), new Vector3(0, 0, 0) };
        _Trangles = new int[] { 0, 2, 3, 2, 0, 1};
        _Uvs = new Vector2[] { new Vector2(0, 1), new Vector2(1, 1), new Vector2(1, 0), new Vector2(0, 0) };
        _Position = pos;
        _Size = new Vector3(1, 1, 1);
        _MeshIndex = index;

    }
}

[RequireComponent(typeof(MeshRenderer))]
[RequireComponent(typeof(MeshFilter))]
public class MeshCombineObject : MonoBehaviour 
{
    public Material _MeshMaterial;
    public Mesh _MeshObject;

    //[HideInInspector]
    public List<MeshObjectData> _meshObjectDatas;
    public Sprite[] _Meshsprites;
    public Vector3[] _MeshVerts;
    public int[] _MeshTrian;
    public Vector2[] _MeshUv;


    public void CreateNewObjectChild()
    {
        if(_meshObjectDatas.Count == 0)
        {
            _meshObjectDatas = new List<MeshObjectData>();
            _meshObjectDatas.Add(new MeshObjectData(new Vector3(1,1,0),0));
        }
        else
        {
            _meshObjectDatas.Add(new MeshObjectData(_meshObjectDatas[_meshObjectDatas.Count -1]._Position + new Vector3(1, 1, 0),_meshObjectDatas[_meshObjectDatas.Count - 1]._MeshIndex));
        }
    }

    public void RenderMesh()
    {
        if(_meshObjectDatas.Count > 0)
        {
            //SetVertsAndtringlesSize
            _MeshVerts = new Vector3[_meshObjectDatas.Count * 4];
            _MeshTrian = new int[_meshObjectDatas.Count * 6];
            _MeshUv = new Vector2[_meshObjectDatas.Count * 4];


            //SetVertsAndtringlesData
            for (int Ri = 0; Ri < _meshObjectDatas.Count; Ri++)
            {
                //ClampIndex
                _meshObjectDatas[Ri]._MeshIndex = Mathf.Clamp(_meshObjectDatas[Ri]._MeshIndex, 0, _Meshsprites.Length - 1);

                //SetSprite
                _meshObjectDatas[Ri]._Sprite = _Meshsprites[_meshObjectDatas[Ri]._MeshIndex];


                if(_meshObjectDatas[Ri]._Sprite)
                {
                    //GetData
                    Vector3 _datapos = _meshObjectDatas[Ri]._Position;
                    Vector3 _dataSize = _meshObjectDatas[Ri]._Size;


                    //Uvs
                    Rect _rect = _meshObjectDatas[Ri]._Sprite.rect;
                    float _PixelsPU = _meshObjectDatas[Ri]._Sprite.pixelsPerUnit;
                    float _Tw = _meshObjectDatas[Ri]._Sprite.texture.width;
                    float _Th = _meshObjectDatas[Ri]._Sprite.texture.height;
                    float _x = _rect.x;
                    float _y = _rect.y;
                    float _w = _rect.width;
                    float _h = _rect.height;

                    _meshObjectDatas[Ri]._Uvs[0] = pixelstoUvCordiness(_x, _y + _h, _Tw, _Th);
                    _meshObjectDatas[Ri]._Uvs[1] = pixelstoUvCordiness(_x + _w, _y + _h, _Tw, _Th);
                    _meshObjectDatas[Ri]._Uvs[2] = pixelstoUvCordiness(_x + _w, _y, _Tw, _Th);
                    _meshObjectDatas[Ri]._Uvs[3] = pixelstoUvCordiness(_x, _y, _Tw, _Th);
                    for (int Ui = 0; Ui < 4; Ui++)
                    {
                        _MeshUv[(4 * Ri) + Ui] = _meshObjectDatas[Ri]._Uvs[Ui];
                    }


                    //verts
                    _meshObjectDatas[Ri]._Verticals[0] = _datapos + new Vector3(-pixelstoUnit(_w, _h, _PixelsPU).x * _dataSize.x, pixelstoUnit(_w, _h, _PixelsPU).y * _dataSize.y, 0);
                    _meshObjectDatas[Ri]._Verticals[1] = _datapos + new Vector3(pixelstoUnit(_w, _h, _PixelsPU).x * _dataSize.x, pixelstoUnit(_w, _h, _PixelsPU).y * _dataSize.y, 0);
                    _meshObjectDatas[Ri]._Verticals[2] = _datapos + new Vector3(pixelstoUnit(_w, _h, _PixelsPU).x * _dataSize.x, -pixelstoUnit(_w, _h, _PixelsPU).y * _dataSize.y, 0);
                    _meshObjectDatas[Ri]._Verticals[3] = _datapos + new Vector3(-pixelstoUnit(_w, _h, _PixelsPU).x * _dataSize.x, -pixelstoUnit(_w, _h, _PixelsPU).y * _dataSize.y, 0);

                    for (int Vi = 0; Vi < 4; Vi++)
                    {
                        _MeshVerts[(4 * Ri) + Vi] = _meshObjectDatas[Ri]._Verticals[Vi];
                    }


                    //Triag
                    _meshObjectDatas[Ri]._Trangles[0] = (Ri * 4) + 0;
                    _meshObjectDatas[Ri]._Trangles[1] = (Ri * 4) + 2;
                    _meshObjectDatas[Ri]._Trangles[2] = (Ri * 4) + 3;
                    _meshObjectDatas[Ri]._Trangles[3] = (Ri * 4) + 2;
                    _meshObjectDatas[Ri]._Trangles[4] = (Ri * 4) + 0;
                    _meshObjectDatas[Ri]._Trangles[5] = (Ri * 4) + 1;
                    for (int Ti = 0; Ti < 6; Ti++)
                    {
                        _MeshTrian[(6 * Ri) + Ti] = _meshObjectDatas[Ri]._Trangles[Ti];
                    }
                }

            }

            //renderAll
            _MeshObject.Clear();
            _MeshObject.vertices = _MeshVerts;
            _MeshObject.triangles = _MeshTrian;
            _MeshObject.uv = _MeshUv;
            _MeshObject.RecalculateNormals();

        }
    }


    Vector2 pixelstoUvCordiness(float x, float y, float Tw,float Th)
    {
        return new Vector2(1 / Tw * x, 1 / Th * y);
    }

    Vector3 pixelstoUnit(float w,float h,float ppu)
    {
        return new Vector3(1 / ppu * w * 0.5f, 1 / ppu * h * 0.5f, 0);
    }
}
