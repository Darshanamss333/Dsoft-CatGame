using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActiveObject : MonoBehaviour 
{
    enum ActiveCondition
    {
        _UipanelControlState,
        _UipanelMapState,
        _UiPanelInventoryState,
        _UiSwordLvl,
        _UiForcusStorageLvl,
        _IfMapUnlock,
        _IfInventoryUnlock,
        _IfMapEmpty,
        _IfMapNotUnlock,
        _IfInventoryNotUnlock
    }

    [SerializeField]
    ActiveCondition _condition;
    [SerializeField]
    GameObject _object;

    [SerializeField]
    List<GameObject> _objects;

	private void Update()
	{
        //UiControll
        if(_condition == ActiveCondition._UipanelControlState)
        {
            if(GameManager.Instance._CurrentUIPanelState == UIPanelState.ControlPanel)
            {
                _object.SetActive(true);
            }
            else
            {
                _object.SetActive(false);
            }
        }

        //UIMapstate
        if (_condition == ActiveCondition._UipanelMapState)
        {
            if (GameManager.Instance._CurrentUIPanelState == UIPanelState.MapPanel)
            {
                _object.SetActive(true);
            }
            else
            {
                _object.SetActive(false);
            }
        }

        //UIInventory
        if (_condition == ActiveCondition._UiPanelInventoryState)
        {
            if (GameManager.Instance._CurrentUIPanelState == UIPanelState.InventoryPanel)
            {
                _object.SetActive(true);
            }
            else
            {
                _object.SetActive(false);
            }
        }

        //SwordLevel
        if(_condition == ActiveCondition._UiSwordLvl)
        {
            for (int i = 0; i < _objects.Count; i++)
            {
                if(i == (int)GameManager.Instance.PermenetUnlockData.SwordLavel)
                {
                    _objects[i].SetActive(true);
                }
                else
                {
                    _objects[i].SetActive(false);
                }
            }
        }

        //ForcusStorage
        if (_condition == ActiveCondition._UiForcusStorageLvl)
        {
            for (int i = 0; i < _objects.Count; i++)
            {
                if (i == (int)GameManager.Instance.PermenetUnlockData.FocusStorageLevel)
                {
                    _objects[i].SetActive(true);
                }
                else
                {
                    _objects[i].SetActive(false);
                }
            }
        }

        //MapButton
        if(_condition == ActiveCondition._IfMapUnlock)
        {
            if (GameManager.Instance.PermenetUnlockData.Map)
            {
                if(_object.activeInHierarchy == false)
                {
                    _object.SetActive(true);
                }
            }
            else
            {
                if (_object.activeInHierarchy == true)
                {
                    _object.SetActive(false);
                }
            }
        }

        //inventoryButton
        if (_condition == ActiveCondition._IfInventoryUnlock)
        {
            if (GameManager.Instance.PermenetUnlockData.Inventory)
            {
                if (_object.activeInHierarchy == false)
                {
                    _object.SetActive(true);
                }
            }
            else
            {
                if (_object.activeInHierarchy == true)
                {
                    _object.SetActive(false);
                }
            }
        }

        //EmptyMapButton
        if (_condition == ActiveCondition._IfMapEmpty)
        {
            if (GameManager.Instance._CurrentUIPanelState == UIPanelState.EmptyMap)
            {
                if (_object.activeInHierarchy == false)
                {
                    _object.SetActive(true);
                }
            }
            else
            {
                if (_object.activeInHierarchy == true)
                {
                    _object.SetActive(false);
                }
            }
        }


        //UnlockTrigers__________________________________________________________________
        //MapUnlockTriger
        if (_condition == ActiveCondition._IfMapNotUnlock)
        {
            if (GameManager.Instance.PermenetUnlockData.Map)
            {
                if (_object.activeInHierarchy == true)
                {
                    _object.SetActive(false);
                }
            }
            else
            {
                if (_object.activeInHierarchy == false)
                {
                    _object.SetActive(true);
                }
            }
        }

        //inventoryUnlockTriger
        if (_condition == ActiveCondition._IfInventoryNotUnlock)
        {
            if (GameManager.Instance.PermenetUnlockData.Inventory)
            {
                if (_object.activeInHierarchy == true)
                {
                    _object.SetActive(false);
                }
            }
            else
            {
                if (_object.activeInHierarchy == false)
                {
                    _object.SetActive(true);
                }
            }
        }
	}
}
