using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class RoadsRefrence
{
    public string _RoadName;
    public GameObject _01Parent;
    public GameObject _01Door;
    public GameObject _02Parent;
    public GameObject _02Door;
}

public enum MapRegions
{
    A,
    B
}

public class LevelManager : MonoBehaviour 
{
    public MapRegions _CurrentMapRegion;
    public GameObject _RoomBenchParent;
    public List<RoadsRefrence> Roads;
}
