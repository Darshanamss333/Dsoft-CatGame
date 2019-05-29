using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class AnimationEventsResources
{
    //----------------------------------------------------
    public string _Name;
    [SerializeField]
    List<GameObject> _activeObjects = new List<GameObject>();

    //activeAll
    public void ActiveAll()
    {
        for (int i = 0; i < _activeObjects.Count; i++) 
        {
            if(_activeObjects[i] != null)
            {
                if (_activeObjects[i].activeInHierarchy)
                {
                    _activeObjects[i].SetActive(false);
                }
                else
                {
                    _activeObjects[i].SetActive(true);
                }
            }
        }
    }

}

public class AnimationEvents : MonoBehaviour 
{

    public List<AnimationEventsResources> _AnimEventResources = new List<AnimationEventsResources>();

    //geteventindex
    int getEventIndex(string _eventName)
    {
        int _return = -1;
        for (int i = 0; i < _AnimEventResources.Count; i++)
        {
            if(_AnimEventResources[i]._Name == _eventName)
            {
                _return = i;
            }
        }
        return _return;
    }

    //CallActiveOnce
    void ActiveOnce(string _IndexPlusValue)
    {
        string[] _re = _IndexPlusValue.Split(char.Parse(","));
        int _index = int.Parse(_re[0]);
        bool _active = false;

        if(_re[1] == "T")
        {
            _active = true;
        }
        if(_re[1] == "F")
        {
            _active = false;
        }

        _objects[_index].SetActive(_active);
    }


    //CallActiveAll
    void ActiveEvent(string _eventName)
    {
        _AnimEventResources[getEventIndex(_eventName)].ActiveAll();
    }

    //PlayAnimation
    void PlayAnimationEvent(string _animation)
    {
        GetComponent<Animator>().CrossFade(_animation, 0);
    }


    public List<GameObject> _objects = new List<GameObject>();
    //Create
    public void CreatEvent(int _objectIndex)
    {
        GameObject _newobj  = Instantiate(_objects[_objectIndex]);
        _newobj.transform.position = transform.position;
    }

    //CreateSetParent
    public void CreatSetParentEvent(int _objectIndex)
    {
        GameObject _newobj = Instantiate(_objects[_objectIndex],transform);
        _newobj.transform.localPosition = Vector3.zero;
    }

    //AddHealthValue
    public void AddHealth()
    {
        GameManager.Instance.TemporyUnlockData.PlayerHealthValue += 1;
        GameManager.Instance.TemporyUnlockData.FocusStorageValue -= 2;
    }

    //playerMovement
    public void PlayerMovementBool(int _true)
    {
        if(_true == 1)
        {
            GameManager.Instance._Player.GetComponent<Player_Controler>()._PlayerMovement = true;
        }
        else
        {
            GameManager.Instance._Player.GetComponent<Player_Controler>()._PlayerMovement = false;
            GameManager.Instance._Player.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        }
    }

    //CreateSetScaleX
    public void CreateSetScaleEvent(int _objectIndex)
    {
        GameObject _newobj = Instantiate(_objects[_objectIndex]);
        _newobj.transform.position = transform.position;
        _newobj.transform.localScale = new Vector3(transform.localScale.x, 1, 1);
    }
}
