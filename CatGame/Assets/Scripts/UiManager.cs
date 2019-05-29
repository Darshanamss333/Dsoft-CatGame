using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UiManager : MonoBehaviour
{
    [SerializeField]
    GameObject _healthUIParent;
    [SerializeField]
    GameObject _haelthUIPrefab;
    [SerializeField]
    GameObject _FocusBotle;
    [SerializeField]
    GameObject _ForcusAddParticleOBJ;
    [SerializeField]
    GameObject _ForcusEmptyParticleOBJ;
    [SerializeField]
    GameObject _FaceAddParticleOBJ;
    [SerializeField]
    Text _CoinsValueText;

	private void Start()
	{
        if(_Co != null)
        {
            StopCoroutine(WaitforLoadAwake());
        }
        _Co = StartCoroutine(WaitforLoadAwake());
	}

    //WaitForGamManager
    Coroutine _Co;
    public GameObject _controlObject;
    IEnumerator WaitforLoadAwake()
    {
        yield return new WaitUntil(() => GameManager.Instance);
        yield return new WaitUntil(() => _controlObject.activeInHierarchy == true);
        RefreshHealth();
    }

    //Refresh
    public void RefreshHealth()
    {
        if(_healthUIParent && _haelthUIPrefab)
        {
            //deleteOverHealth
            if (GameManager.Instance.PermenetUnlockData.MaxHealthValue < _healthUIParent.transform.childCount)
            {
                int _overCount = _healthUIParent.transform.childCount - GameManager.Instance.PermenetUnlockData.MaxHealthValue;
                for (int oi = 0; oi < _overCount; oi++)
                {
                    DestroyImmediate(_healthUIParent.transform.GetChild(_healthUIParent.transform.childCount - 1).gameObject);
                }
            }

            for (int ri = 0; ri < GameManager.Instance.PermenetUnlockData.MaxHealthValue; ri++)
            {
                if(!_healthUIParent.transform.Find(ri.ToString()))
                {
                    GameObject _newH = Instantiate(_haelthUIPrefab, _healthUIParent.transform);
                    _newH.name = ri.ToString();
                    RectTransform _tr = _newH.GetComponent<RectTransform>();
                    _tr.localPosition = new Vector3(0, 0, 0);
                    _tr.localPosition = new Vector3(ri * 50, 0, 0);
                }


                GameObject _H = _healthUIParent.transform.Find(ri.ToString()).gameObject;
                Animator _anim = _H.GetComponent<Animator>();

                if (ri < GameManager.Instance.TemporyUnlockData.PlayerHealthValue)
                {
                    _anim.SetBool("True", true);
                }
                else
                {
                    _anim.SetBool("True", false);
                }
            }

        }
    }

    //Update
    int _deltaCurrentHealth;
    int _deltaMaxHealth;
    Image _BotleFill;
    float _deltaCurruntForcusStorageValue;
    UIPanelState _deltauipanel;
	private void Update()
	{
        if(GameManager.Instance)
        {
            if(GameManager.Instance.TemporyUnlockData.PlayerHealthValue != _deltaCurrentHealth)
            {
                if (_Co != null)
                {
                    StopCoroutine(WaitforLoadAwake());
                }
                _Co = StartCoroutine(WaitforLoadAwake());
                _deltaCurrentHealth = GameManager.Instance.TemporyUnlockData.PlayerHealthValue;
            }

            if (GameManager.Instance.PermenetUnlockData.MaxHealthValue != _deltaMaxHealth)
            {
                if (_Co != null)
                {
                    StopCoroutine(WaitforLoadAwake());
                }
                _Co = StartCoroutine(WaitforLoadAwake());
                _deltaMaxHealth = GameManager.Instance.PermenetUnlockData.MaxHealthValue;
            }

            if(GameManager.Instance._CurrentUIPanelState != _deltauipanel)
            {
                if (_Co != null)
                {
                    StopCoroutine(WaitforLoadAwake());
                }
                _Co = StartCoroutine(WaitforLoadAwake());
                _deltauipanel = GameManager.Instance._CurrentUIPanelState;
            }

            if(_BotleFill)
            {
                _deltaCurruntForcusStorageValue = Mathf.InverseLerp(0, GameManager.Instance.PermenetUnlockData.GetMaxFocusStorageValue(), GameManager.Instance.TemporyUnlockData.FocusStorageValue);
                if(_deltaCurruntForcusStorageValue != _BotleFill.fillAmount)
                {
                    if(_deltaCurruntForcusStorageValue < _BotleFill.fillAmount)
                    {
                        CreateParticleObject(_ForcusEmptyParticleOBJ, _FocusBotle);
                    }
                    else
                    {
                        CreateParticleObject(_ForcusAddParticleOBJ, _FocusBotle);
                    }
                    _BotleFill.fillAmount = _deltaCurruntForcusStorageValue;
                }
            }
            else
            {
                _BotleFill = _FocusBotle.GetComponent<Image>();
            }


            //CoinsShow
            _CoinsValueText.text = GameManager.Instance.TemporyUnlockData.PlayerCash.ToString();

        }
	}

    //CreateObject
    void CreateParticleObject(GameObject _object,GameObject _parent)
    {
        GameObject _newPar = Instantiate(_object,_parent.transform);
        _newPar.transform.localPosition = Vector3.zero;
    }

}
