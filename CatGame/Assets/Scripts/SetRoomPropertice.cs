using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetRoomPropertice : MonoBehaviour {

    [SerializeField]
    MapRegions _setRoomRegion;
    [SerializeField]
    GameObject _benchParent;
	void Start () 
    {
        StartCoroutine(WaitforGamemanagerAwake());
	}
    //WaitForGamManager
    IEnumerator WaitforGamemanagerAwake()
    {
        yield return new WaitUntil(() => GameManager.Instance);
        yield return new WaitUntil(() => GameManager.Instance._LevelManager);

        GameManager.Instance._LevelManager._CurrentMapRegion = _setRoomRegion;
        GameManager.Instance._LevelManager._RoomBenchParent = _benchParent;
        GameManager.Instance.CameraColliderBoxObject = this.gameObject;
    }
}
