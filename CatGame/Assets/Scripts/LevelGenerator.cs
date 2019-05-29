using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using System;

//[System.Serializable]
public class LevelRoomResources
{
    public List<int> Fill = new List<int>
    {
        1,1,1,1,1,1,1,1,
        1,1,1,1,1,1,1,1,
        1,1,1,1,1,1,1,1,
        1,1,1,1,1,1,1,1,
        1,1,1,1,1,1,1,1,
        1,1,1,1,1,1,1,1,
        1,1,1,1,1,1,1,1,
        1,1,1,1,1,1,1,1,
    };

    public List<int> EdgeR = new List<int>
    {
        1,1,1,1,1,1,1,1,
        1,1,1,1,1,1,1,1,
        1,1,1,0,0,0,0,0,
        1,1,1,0,0,0,0,0,
        1,1,1,0,0,0,0,0,
        1,1,1,0,0,0,0,0,
        1,1,1,1,1,1,1,1,
        1,1,1,1,1,1,1,1,
    };

    public List<int> EdgeL = new List<int>
    {
        1,1,1,1,1,1,1,1,
        1,1,1,1,1,1,1,1,
        0,0,0,0,0,0,1,1,
        0,0,0,0,0,0,1,1,
        0,0,0,0,0,0,1,1,
        0,0,0,0,0,0,1,1,
        1,1,1,1,1,1,1,1,
        1,1,1,1,1,1,1,1,
    };

    public List<int> EdgeU = new List<int>
    {
        1,1,0,0,0,0,1,1,
        1,1,0,0,0,0,1,1,
        1,1,0,0,0,0,1,1,
        1,1,0,0,0,0,1,1,
        1,1,0,0,0,0,1,1,
        1,1,0,0,0,0,1,1,
        1,1,1,1,1,1,1,1,
        1,1,1,1,1,1,1,1,
    };

    public List<int> EdgeD = new List<int>
    {
        1,1,1,1,1,1,1,1,
        1,1,1,1,1,1,1,1,
        1,1,0,0,0,0,1,1,
        1,1,0,0,0,0,1,1,
        1,1,0,0,0,0,1,1,
        1,1,0,0,0,0,1,1,
        1,1,0,0,0,0,1,1,
        1,1,0,0,0,0,1,1,
    };

    public List<int> MidleLR = new List<int>
    {
        1,1,1,1,1,1,1,1,
        1,1,1,1,1,1,1,1,
        0,0,0,0,0,0,0,0,
        0,0,0,0,0,0,0,0,
        0,0,0,0,0,0,0,0,
        0,0,0,0,0,0,0,0,
        1,1,1,1,1,1,1,1,
        1,1,1,1,1,1,1,1,
    };

    public List<int> MidleUD = new List<int>
    {
        1,1,0,0,0,0,1,1,
        1,1,0,0,0,0,1,1,
        1,1,0,0,0,0,1,1,
        1,1,0,0,0,0,1,1,
        1,1,0,0,0,0,1,1,
        1,1,0,0,0,0,1,1,
        1,1,0,0,0,0,1,1,
        1,1,0,0,0,0,1,1,
    };

    public List<int> ConersLU = new List<int>
    {
        1,1,0,0,0,0,1,1,
        1,1,0,0,0,0,1,1,
        0,0,0,0,0,0,1,1,
        0,0,0,0,0,0,1,1,
        0,0,0,0,0,0,1,1,
        0,0,0,0,0,0,1,1,
        1,1,1,1,1,1,1,1,
        1,1,1,1,1,1,1,1,
    };

    public List<int> ConersRU = new List<int>
    {
        1,1,0,0,0,0,1,1,
        1,1,0,0,0,0,1,1,
        1,1,0,0,0,0,0,0,
        1,1,0,0,0,0,0,0,
        1,1,0,0,0,0,0,0,
        1,1,0,0,0,0,0,0,
        1,1,1,1,1,1,1,1,
        1,1,1,1,1,1,1,1,
    };

    public List<int> ConersRD = new List<int>
    {
        1,1,1,1,1,1,1,1,
        1,1,1,1,1,1,1,1,
        1,1,0,0,0,0,0,0,
        1,1,0,0,0,0,0,0,
        1,1,0,0,0,0,0,0,
        1,1,0,0,0,0,0,0,
        1,1,0,0,0,0,1,1,
        1,1,0,0,0,0,1,1,
    };

    public List<int> ConersLD = new List<int>
    {
        1,1,1,1,1,1,1,1,
        1,1,1,1,1,1,1,1,
        0,0,0,0,0,0,1,1,
        0,0,0,0,0,0,1,1,
        0,0,0,0,0,0,1,1,
        0,0,0,0,0,0,1,1,
        1,1,0,0,0,0,1,1,
        1,1,0,0,0,0,1,1,
    };

    public List<int> TriangleLUD = new List<int>
    {
        1,1,0,0,0,0,1,1,
        1,1,0,0,0,0,1,1,
        0,0,0,0,0,0,1,1,
        0,0,0,0,0,0,1,1,
        0,0,0,0,0,0,1,1,
        0,0,0,0,0,0,1,1,
        1,1,0,0,0,0,1,1,
        1,1,0,0,0,0,1,1,
    };

    public List<int> TriangleRUD = new List<int>
    {
        1,1,0,0,0,0,1,1,
        1,1,0,0,0,0,1,1,
        1,1,0,0,0,0,0,0,
        1,1,0,0,0,0,0,0,
        1,1,0,0,0,0,0,0,
        1,1,0,0,0,0,0,0,
        1,1,0,0,0,0,1,1,
        1,1,0,0,0,0,1,1,
    };

    public List<int> TriangleRUL = new List<int>
    {
        1,1,0,0,0,0,1,1,
        1,1,0,0,0,0,1,1,
        0,0,0,0,0,0,0,0,
        0,0,0,0,0,0,0,0,
        0,0,0,0,0,0,0,0,
        0,0,0,0,0,0,0,0,
        1,1,1,1,1,1,1,1,
        1,1,1,1,1,1,1,1,
    };

    public List<int> MidleLURD = new List<int>
    {
        1,1,0,0,0,0,1,1,
        1,1,0,0,0,0,1,1,
        0,0,0,0,0,0,0,0,
        0,0,0,0,0,0,0,0,
        0,0,0,0,0,0,0,0,
        0,0,0,0,0,0,0,0,
        1,1,0,0,0,0,1,1,
        1,1,0,0,0,0,1,1,
    };


}

public class LevelGenerator : MonoBehaviour
{

    //Ground
	public LevelRoomResources _LevelRoomResources;
    public Tilemap _GroundTilemap;
    public RuleTile _GroundRuleTile;
    public List<Vector3Int> _GroundRoomsPos;

    //BackGround
    public Tilemap _BackgroundTilemap;
    public RuleTile _BackgroundRuleTile;

    //Platform
    public Tilemap _PlatformTilemap;
    public RuleTile _PlatformRuleTile;


    //GenerateRoom----------------------------
    void GenerateRoom(Vector3 _startWorldPos,List<int> _roomType)
    {
        int Ypos;
        int Xpos;
        int index;

        Vector3Int _startCellPos = _GroundTilemap.WorldToCell(_startWorldPos);
        _startCellPos += new Vector3Int(-4, 4, 0);

        for (int Hi = 0; Hi < 8; Hi++)
        {
            Ypos = _startCellPos.y - (Hi * 1);

            for (int Wi = 0; Wi < 8; Wi++)
            {
                Xpos = _startCellPos.x + (Wi * 1);
                index = (Hi * 8) + Wi;

                if(_roomType[index] == 1)
                {
                    _GroundTilemap.SetTile(new Vector3Int(Xpos, Ypos, 0), _GroundRuleTile);
                }
                if(_roomType[index] == 0)
                {
                    _GroundTilemap.SetTile(new Vector3Int(Xpos, Ypos, 0), null);
                }
            }
        }
    }

    //GenerateLevelPath-------------------------
    enum Directions
    {
        Left,
        Up,
        Right,
        Down
    }
    public void GenerateLevel()
    {
        //ClearTileMap
        _GroundTilemap.ClearAllTiles();

        _GroundRoomsPos = new List<Vector3Int>();
        _LevelRoomResources = new LevelRoomResources();
        int _numberOfMaxRooms = 40;
        Directions dir;
        Vector3Int _currPos = Vector3Int.zero;
        Vector3Int _offset = Vector3Int.zero;
        //Generate Room Positions
        for (int RPi = 0; RPi < _numberOfMaxRooms; RPi++)
        {
            if (!_GroundRoomsPos.Contains(_currPos))
            {
                //AddRoomPos to List
                _GroundRoomsPos.Add(_currPos);
            }

            //ChooseNextDir
            dir = (Directions)UnityEngine.Random.Range(0, 4);
            switch (dir)
            {
                case Directions.Left:
                    _offset = new Vector3Int(-8, 0, 0);
                    break;
                case Directions.Up:
                    _offset = new Vector3Int(0, 8, 0);
                    break;
                case Directions.Right:
                    _offset = new Vector3Int(8, 0, 0);
                    break;
                case Directions.Down:
                    _offset = new Vector3Int(0, -8, 0);
                    break;
            }
            _currPos = _currPos + _offset;
        }


        //Choose Room type
        for (int Ri = 0; Ri < _GroundRoomsPos.Count; Ri++)
        {

            bool _leftOk = _GroundRoomsPos.Contains(_GroundRoomsPos[Ri] + new Vector3Int(-8, 0, 0));
            bool _upOk = _GroundRoomsPos.Contains(_GroundRoomsPos[Ri] + new Vector3Int(0, 8, 0));
            bool _rightOk = _GroundRoomsPos.Contains(_GroundRoomsPos[Ri] + new Vector3Int(8, 0, 0));
            bool _downOk = _GroundRoomsPos.Contains(_GroundRoomsPos[Ri] + new Vector3Int(0, -8, 0));

            //Single------------------------
            //Left
            if (_leftOk && !_upOk && !_rightOk && !_downOk)
            {
                GenerateRoom(_GroundRoomsPos[Ri], _LevelRoomResources.EdgeL);
            }

            //Right
            else if (!_leftOk && !_upOk && _rightOk && !_downOk)
            {
                GenerateRoom(_GroundRoomsPos[Ri], _LevelRoomResources.EdgeR);
            }

            //Up
            else if (!_leftOk && _upOk && !_rightOk && !_downOk)
            {
                GenerateRoom(_GroundRoomsPos[Ri], _LevelRoomResources.EdgeU);
            }

            //Down
            else if (!_leftOk && !_upOk && !_rightOk && _downOk)
            {
                GenerateRoom(_GroundRoomsPos[Ri], _LevelRoomResources.EdgeD);
            }

            //UpDown---------------------------
            //Left&Right
            else if (_leftOk && !_upOk && _rightOk && !_downOk)
            {
                GenerateRoom(_GroundRoomsPos[Ri], _LevelRoomResources.MidleLR);
            }

            //Up&Down
            else if (!_leftOk && _upOk && !_rightOk && _downOk)
            {
                GenerateRoom(_GroundRoomsPos[Ri], _LevelRoomResources.MidleUD);
            }

            //Conesr------------------------
            //Left%Up
            else if (_leftOk && _upOk && !_rightOk && !_downOk)
            {
                GenerateRoom(_GroundRoomsPos[Ri], _LevelRoomResources.ConersLU);
            }

            //Right%Up
            else if (!_leftOk && _upOk && _rightOk && !_downOk)
            {
                GenerateRoom(_GroundRoomsPos[Ri], _LevelRoomResources.ConersRU);
            }

            //Left%Down
            else if (_leftOk && !_upOk && !_rightOk && _downOk)
            {
                GenerateRoom(_GroundRoomsPos[Ri], _LevelRoomResources.ConersLD);
            }

            //Right%Down
            else if (!_leftOk && !_upOk && _rightOk && _downOk)
            {
                GenerateRoom(_GroundRoomsPos[Ri], _LevelRoomResources.ConersRD);
            }

            //Triangle------------------------
            //Right%Down&left
            else if (_leftOk && !_upOk && _rightOk && _downOk)
            {
                GenerateRoom(_GroundRoomsPos[Ri], _LevelRoomResources.MidleLR);
            }

            //Left%Up&Right
            else if (_leftOk && _upOk && _rightOk && !_downOk)
            {
                GenerateRoom(_GroundRoomsPos[Ri], _LevelRoomResources.TriangleRUL);
            }

            //Up%Down&Left
            else if (_leftOk && _upOk && !_rightOk && _downOk)
            {
                GenerateRoom(_GroundRoomsPos[Ri], _LevelRoomResources.TriangleLUD);
            }

            //Up%Down&Right
            else if (!_leftOk && _upOk && _rightOk && _downOk)
            {
                GenerateRoom(_GroundRoomsPos[Ri], _LevelRoomResources.TriangleRUD);
            }

            //----------------------------------
            //AllOpen
            else if (_leftOk && _upOk && _rightOk && _downOk)
            {
                GenerateRoom(_GroundRoomsPos[Ri], _LevelRoomResources.MidleLR);
            }

            //Fill
            else
            {
                GenerateRoom(_GroundRoomsPos[Ri], _LevelRoomResources.Fill);
            }
        }


        //FillFrame
        for (int FFi = 0; FFi < _GroundRoomsPos.Count; FFi++)
        {

            Vector3Int _OffsetCheckLeft = _GroundRoomsPos[FFi] + new Vector3Int(-8, 0, 0);
            Vector3Int _OffsetCheckLeftUp = _GroundRoomsPos[FFi] + new Vector3Int(-8, 8, 0);
            Vector3Int _OffsetCheckUp = _GroundRoomsPos[FFi] + new Vector3Int(0, 8, 0);
            Vector3Int _OffsetCheckRightUp = _GroundRoomsPos[FFi] + new Vector3Int(8, 8, 0);
            Vector3Int _OffsetCheckRight = _GroundRoomsPos[FFi] + new Vector3Int(8, 0, 0);
            Vector3Int _OffsetCheckRightDown = _GroundRoomsPos[FFi] + new Vector3Int(8, -8, 0);
            Vector3Int _OffsetCheckDown = _GroundRoomsPos[FFi] + new Vector3Int(0, -8, 0);
            Vector3Int _OffsetCheckLeftDown = _GroundRoomsPos[FFi] + new Vector3Int(-8, -8, 0);

            //Left
            if (!_GroundRoomsPos.Contains(_OffsetCheckLeft))
            {
                GenerateRoom(_OffsetCheckLeft, _LevelRoomResources.Fill);
            }
            //LeftUp
            if (!_GroundRoomsPos.Contains(_OffsetCheckLeftUp))
            {
                GenerateRoom(_OffsetCheckLeftUp, _LevelRoomResources.Fill);
            }
            //Up
            if (!_GroundRoomsPos.Contains(_OffsetCheckUp))
            {
                GenerateRoom(_OffsetCheckUp, _LevelRoomResources.Fill);
            }
            //RightUp
            if (!_GroundRoomsPos.Contains(_OffsetCheckRightUp))
            {
                GenerateRoom(_OffsetCheckRightUp, _LevelRoomResources.Fill);
            }
            //Right
            if (!_GroundRoomsPos.Contains(_OffsetCheckRight))
            {
                GenerateRoom(_OffsetCheckRight, _LevelRoomResources.Fill);
            }
            //RightDown
            if (!_GroundRoomsPos.Contains(_OffsetCheckRightDown))
            {
                GenerateRoom(_OffsetCheckRightDown, _LevelRoomResources.Fill);
            }
            //Down
            if (!_GroundRoomsPos.Contains(_OffsetCheckDown))
            {
                GenerateRoom(_OffsetCheckDown, _LevelRoomResources.Fill);
            }
            //LeftDown
            if (!_GroundRoomsPos.Contains(_OffsetCheckLeftDown))
            {
                GenerateRoom(_OffsetCheckLeftDown, _LevelRoomResources.Fill);
            }
        }
    }


    //BackGroundGenerate----------------------------------
    public void BackGroundGenerate()
    {
        //ClearTileMap
        _BackgroundTilemap.ClearAllTiles();

        for (int BRi = 0; BRi < _GroundRoomsPos.Count; BRi++) 
        {
            int Ypos;
            int Xpos;
            int index;

            Vector3Int _startCellPos = _GroundTilemap.WorldToCell(_GroundRoomsPos[BRi]);
            _startCellPos += new Vector3Int(-4, 4, 0);

            for (int Hi = 0; Hi < 8; Hi++)
            {
                Ypos = _startCellPos.y - (Hi * 1);

                for (int Wi = 0; Wi < 8; Wi++)
                {
                    Xpos = _startCellPos.x + (Wi * 1);
                    index = (Hi * 8) + Wi;

                    if (_LevelRoomResources.Fill[index] == 1)
                    {
                        _BackgroundTilemap.SetTile(new Vector3Int(Xpos, Ypos, 0), _BackgroundRuleTile);
                    }
                    if (_LevelRoomResources.Fill[index] == 0)
                    {
                        _BackgroundTilemap.SetTile(new Vector3Int(Xpos, Ypos, 0), null);
                    }
                }
            }
        }
    }


	//PlatformGenerate
	public void PlatformGenerater()
	{
        _PlatformTilemap.ClearAllTiles();
        for (int PRi = 0; PRi < _GroundRoomsPos.Count; PRi++) 
        {
            int _PaltformsPerRoom = UnityEngine.Random.Range(1, 2);

            for (int Pi = 0; Pi < 1; Pi++) 
            {
                List<int> _patern = new List<int>();
                int _paternType = UnityEngine.Random.Range(0, 4);

                //ChosePattern
                switch(_paternType)
                {
                    case 0:
                        _patern = new List<int>
                        {
                            0,0,0,0,
                            0,0,0,0,
                            0,0,1,1,
                            0,0,0,0
                        };
                        break;
                    case 1:
                        _patern = new List<int>
                        {
                            0,0,0,0,
                            0,0,0,0,
                            1,1,0,0,
                            0,0,0,0
                        };
                        break;
                    case 2:
                        _patern = new List<int>
                        {
                            0,0,0,0,
                            0,0,0,0,
                            0,0,0,0,
                            1,1,1,0
                        };
                        break;
                    case 3:
                        _patern = new List<int>
                        {
                            0,0,0,0,
                            0,0,0,0,
                            1,1,0,0,
                            1,1,0,0
                        };
                        break;
                    case 4:
                        _patern = new List<int>
                        {
                            0,0,0,0,
                            0,0,0,0,
                            0,0,1,1,
                            0,0,1,1
                        };
                        break;
                    case 5:
                        _patern = new List<int>
                        {
                            0,0,0,0,
                            0,0,0,0,
                            0,1,1,0,
                            0,1,1,0
                        };
                        break;
                }


                //AddPaternToTileMap
                int Ypos;
                int Xpos;
                int index;

                Vector3Int _startCellPos = _GroundTilemap.WorldToCell(_GroundRoomsPos[PRi]);
                _startCellPos += new Vector3Int(-2, 2,0);

                for (int Hi = 0; Hi < 4; Hi++)
                {
                    Ypos = _startCellPos.y - (Hi * 1);

                    for (int Wi = 0; Wi < 4; Wi++)
                    {
                        Xpos = _startCellPos.x + (Wi * 1);
                        index = (Hi * 4) + Wi;

                        if (_patern[index] == 1)
                        {
                            _GroundTilemap.SetTile(new Vector3Int(Xpos, Ypos, 0), _PlatformRuleTile);
                        }
                    }
                }

            }
        }
	}
}


