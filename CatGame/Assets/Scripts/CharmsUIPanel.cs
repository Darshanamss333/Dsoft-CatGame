using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class CharmsUidata
{
    public _Charms _CharmID;
    public GameObject _UiObject;
    public GameObject _UiObjectParent;
}

public class CharmsUIPanel : MonoBehaviour 
{

	private void OnEnable()
	{
        if (_Co != null)
        {
            StopCoroutine(WaitforLoadAwake());
        }
        _Co = StartCoroutine(WaitforLoadAwake());
	}

    Coroutine _Co;
    IEnumerator WaitforLoadAwake()
    {
        yield return new WaitUntil(() => GameManager.Instance);
        yield return new WaitUntil(() => GameManager.Instance._LoadComplete == true);
        ReFreshCharms();
    }

    //Charms___________________________________________________________________________________________________________
    [Header("Charms")]
    public List<CharmsUidata> _charmsUidatas;
    public GameObject _equipParent;
    public GameObject _nochParent;
    [SerializeField]
    GameObject _nochObject;
    [SerializeField]
    int _numberOfequips;


    Button _button;
    RectTransform _rtransform;
    GameObject _uiobj;
    GameObject _uiobjParent;
    Vector3 _equipOffset = Vector3.zero;

    void ReFreshCharms()
    {
        NocheRefresh();

        _numberOfequips = 0;
        _equipOffset = Vector3.zero;
        for (int i = 0; i < _charmsUidatas.Count; i++)
        {
            _button = _charmsUidatas[i]._UiObject.GetComponent<Button>();
            _rtransform = _charmsUidatas[i]._UiObject.GetComponent<RectTransform>();
            _uiobj = _charmsUidatas[i]._UiObject;
            _uiobjParent = _charmsUidatas[i]._UiObjectParent;

            switch (_charmsUidatas[i]._CharmID)
            {
                //CoinCollector
                case _Charms.CoinCollector:
                    RefreshIndexCharm(GameManager.Instance.PermenetUnlockData.CoinCollectorUnlock, GameManager.Instance.TemporyUnlockData.CoinCollectorEquip);
                    break;
                //GodsFiller
                case _Charms.GodsFiller:
                    RefreshIndexCharm(GameManager.Instance.PermenetUnlockData.GodsFillerUnlock, GameManager.Instance.TemporyUnlockData.GodsFillerEquip);
                    break;
                //LongSword
                case _Charms.LongSword:
                    RefreshIndexCharm(GameManager.Instance.PermenetUnlockData.LongSwordUnlock, GameManager.Instance.TemporyUnlockData.LongSwordEquip);
                    break;
                //QuickHeal
                case _Charms.QuickHeal:
                    RefreshIndexCharm(GameManager.Instance.PermenetUnlockData.QuickHealUnlock, GameManager.Instance.TemporyUnlockData.QuickHealEquip);
                    break;
                //ToxicDropper
                case _Charms.ToxicDropper:
                    RefreshIndexCharm(GameManager.Instance.PermenetUnlockData.ToxicDropperUnlock, GameManager.Instance.TemporyUnlockData.ToxicDropperEquip);
                    break;
            }
        }
    }

    void NocheRefresh()
    {
        //destroy
        for (int di = 0; di < _nochParent.transform.childCount; di++)
        {
            DestroyObject(_nochParent.transform.GetChild(di).gameObject);
        }

        Vector3 _nochoffset = Vector3.zero;
        for (int i = 0; i < GameManager.Instance.PermenetUnlockData.CharmEquipRooms; i++)
        {
            GameObject _nochobj = Instantiate(_nochObject,_nochParent.transform);
            _nochobj.GetComponent<RectTransform>().localPosition = _nochoffset;
            _nochobj.GetComponent<RectTransform>().localScale = Vector3.one;
            _nochobj.name = i.ToString();
            _nochoffset += new Vector3(80, 0, 0);
        }
    }

    void RefreshIndexCharm(bool _unlock,bool _equip)
    {
        if (_unlock)
        {
            Unlock(_button);

            //equip
            if (_equip)
            {
                if(_uiobj.transform.parent != _equipParent)
                {
                    _uiobj.transform.parent = _equipParent.transform;
                    _rtransform.localPosition = _equipOffset;
                    _equipOffset += new Vector3(80, 0, 0);
                }
                _numberOfequips += 1;
            }
            else
            {
                if(_uiobj.transform.parent != _uiobjParent)
                {
                    _uiobj.transform.parent = _uiobjParent.transform;
                    _rtransform.localPosition = Vector3.zero;
                }
            }
        }
        else
        {
            Lock(_button);

            if (_uiobj.transform.parent != _uiobjParent)
            {
                _uiobj.transform.parent = _uiobjParent.transform;
                _rtransform.localPosition = Vector3.zero;
            }
        }
    }


    void Unlock(Button _Button)
    {
        _Button.interactable = true;
    }

    void Lock(Button _Button)
    {
        _Button.interactable = false;
    }

    //PressButton
    public void CharmeClick(int _i)
    {
        switch (_charmsUidatas[_i]._CharmID)
        {
            //CoinCollector
            case _Charms.CoinCollector:
                if (GameManager.Instance.PermenetUnlockData.CoinCollectorUnlock)
                {
                    if(GameManager.Instance.TemporyUnlockData.CoinCollectorEquip)
                    {
                        GameManager.Instance.TemporyUnlockData.CoinCollectorEquip = !GameManager.Instance.TemporyUnlockData.CoinCollectorEquip;
                    }
                    else
                    {
                        if (GameManager.Instance.PermenetUnlockData.CharmEquipRooms > _numberOfequips)
                        {
                            GameManager.Instance.TemporyUnlockData.CoinCollectorEquip = !GameManager.Instance.TemporyUnlockData.CoinCollectorEquip;
                        }
                    }
                }
                break;
            //GodsFiller
            case _Charms.GodsFiller:
                if (GameManager.Instance.PermenetUnlockData.GodsFillerUnlock)
                {
                    if(GameManager.Instance.TemporyUnlockData.GodsFillerEquip)
                    {
                        GameManager.Instance.TemporyUnlockData.GodsFillerEquip = !GameManager.Instance.TemporyUnlockData.GodsFillerEquip;
                    }
                    else
                    {
                        if (GameManager.Instance.PermenetUnlockData.CharmEquipRooms > _numberOfequips)
                        {
                            GameManager.Instance.TemporyUnlockData.GodsFillerEquip = !GameManager.Instance.TemporyUnlockData.GodsFillerEquip;
                        }
                    }
                }
                break;
            //LongSword
            case _Charms.LongSword:
                if (GameManager.Instance.PermenetUnlockData.LongSwordUnlock)
                {
                    if (GameManager.Instance.TemporyUnlockData.LongSwordEquip)
                    {
                        GameManager.Instance.TemporyUnlockData.LongSwordEquip = !GameManager.Instance.TemporyUnlockData.LongSwordEquip;
                    }
                    else
                    {
                        if (GameManager.Instance.PermenetUnlockData.CharmEquipRooms > _numberOfequips)
                        {
                            GameManager.Instance.TemporyUnlockData.LongSwordEquip = !GameManager.Instance.TemporyUnlockData.LongSwordEquip;
                        }
                    }
                }
                break;
            //QuickHeal
            case _Charms.QuickHeal:
                if (GameManager.Instance.PermenetUnlockData.QuickHealUnlock)
                {
                    if (GameManager.Instance.TemporyUnlockData.QuickHealEquip)
                    {
                        GameManager.Instance.TemporyUnlockData.QuickHealEquip = !GameManager.Instance.TemporyUnlockData.QuickHealEquip;
                    }
                    else
                    {
                        if (GameManager.Instance.PermenetUnlockData.CharmEquipRooms > _numberOfequips)
                        {
                            GameManager.Instance.TemporyUnlockData.QuickHealEquip = !GameManager.Instance.TemporyUnlockData.QuickHealEquip;
                        }
                    }

                }
                break;
            //ToxicDropper
            case _Charms.ToxicDropper:
                if (GameManager.Instance.PermenetUnlockData.ToxicDropperUnlock)
                {
                    if (GameManager.Instance.TemporyUnlockData.ToxicDropperEquip)
                    {
                        GameManager.Instance.TemporyUnlockData.ToxicDropperEquip = !GameManager.Instance.TemporyUnlockData.ToxicDropperEquip;
                    }
                    else
                    {
                        if (GameManager.Instance.PermenetUnlockData.CharmEquipRooms > _numberOfequips)
                        {
                            GameManager.Instance.TemporyUnlockData.ToxicDropperEquip = !GameManager.Instance.TemporyUnlockData.ToxicDropperEquip;
                        }
                    }

                }
                break;
        }

        ReFreshCharms();
    }
}
