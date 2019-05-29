using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Tilemaps;
using Cinemachine;
using System.IO;

//SaveClasse---------------------------
public enum _Charms
{
    CoinCollector,
    LongSword,
    QuickHeal,
    ToxicDropper,
    GodsFiller
}
[System.Serializable]
public class TemporyDataClass
{
    public string LastSaveRoomSceneName;
    public string LastBenchName;
    public int PlayerHealthValue;
    public int FocusStorageValue;
    public int PlayerCash;

    //CharmEqup
    [Header("CharmsEquip")]
    public bool CoinCollectorEquip;
    public bool LongSwordEquip;
    public bool QuickHealEquip;
    public bool ToxicDropperEquip;
    public bool GodsFillerEquip;

    public void DefaultData()
    {
        LastSaveRoomSceneName = "A (0)";
        LastBenchName = "Bench1";
        PlayerHealthValue = 4;
        FocusStorageValue = 0;
        PlayerCash = 0;

        CoinCollectorEquip = false;
        LongSwordEquip = false;
        QuickHealEquip = false;
        ToxicDropperEquip = false;
        GodsFillerEquip = false;
    }
}

public enum UpgradeLavels
{
    Lv01,
    Lv02,
    Lv03,
    Lv04,
    Lv05
}
[System.Serializable]
public class PermenetDataClass
{
    //Inventory
    public int MaxHealthValue;
    public UpgradeLavels SwordLavel;
    public UpgradeLavels MagicLevel;
    public UpgradeLavels FocusStorageLevel;
    public bool Map;
    public bool Inventory;

    //Charms
    [Header("CharmsUnlock")]
    public int CharmEquipRooms;
    public bool CoinCollectorUnlock;
    public bool LongSwordUnlock;
    public bool QuickHealUnlock;
    public bool ToxicDropperUnlock;
    public bool GodsFillerUnlock;

    //Map
    public bool RegionA;
    public bool RegionB;

    public void DefaultData()
    {
        MaxHealthValue = 4;
        SwordLavel = UpgradeLavels.Lv01;
        MagicLevel = UpgradeLavels.Lv01;
        FocusStorageLevel = UpgradeLavels.Lv01;
        Map = false;
        Inventory = false;

        CharmEquipRooms = 1;
        CoinCollectorUnlock = false;
        LongSwordUnlock = false;
        QuickHealUnlock = false;
        ToxicDropperUnlock = false;
        GodsFillerUnlock = false;

        RegionA = true;
        RegionB = false;
    }

    //SwordLevel
    public int GetSwordValue()
    {
        int _return = 0;
        switch (SwordLavel)
        {
            case UpgradeLavels.Lv01:
                _return = 10;
                break;
            case UpgradeLavels.Lv02:
                _return = 15;
                break;
            case UpgradeLavels.Lv03:
                _return = 20;
                break;
            case UpgradeLavels.Lv04:
                _return = 25;
                break;
            case UpgradeLavels.Lv05:
                _return = 30;
                break;
        }
        return _return;
    }

    //MagicLevel
    public int GetMagicValue()
    {
        int _return = 0;
        switch (MagicLevel)
        {
            case UpgradeLavels.Lv01:
                _return = 15;
                break;
            case UpgradeLavels.Lv02:
                _return = 20;
                break;
            case UpgradeLavels.Lv03:
                _return = 25;
                break;
            case UpgradeLavels.Lv04:
                _return = 30;
                break;
            case UpgradeLavels.Lv05:
                _return = 35;
                break;
        }
        return _return;
    }

    //MaxFocusStorageLevel
    public int GetMaxFocusStorageValue()
    {
        int _return = 0;
        switch (FocusStorageLevel)
        {
            case UpgradeLavels.Lv01:
                _return = 4;
                break;
            case UpgradeLavels.Lv02:
                _return = 5;
                break;
            case UpgradeLavels.Lv03:
                _return = 6;
                break;
            case UpgradeLavels.Lv04:
                _return = 7;
                break;
            case UpgradeLavels.Lv05:
                _return = 8;
                break;
        }
        return _return;
    }
}
//-------------------------------------
public enum EnemyType
{
    Acers,
    Nolans
}
[System.Serializable]
public class EnemyPrefab
{
    public EnemyType _Catagory;
    public GameObject _EnemyPrefab;
}
//-------------------------------------
public enum UIPanelState
{
    ControlPanel,
    MapPanel,
    InventoryPanel,
    EmptyMap
}
//-------------------------------------

[Serializable]
public class ControlButtonMap 
{
    public enum ButtonType { A, B, X, Y };

    public ButtonType Button;
    public bool ButtonPress;
}

public class GameManager : MonoBehaviour 
{

    public static GameManager Instance;

    //Singleton
	private void Awake()
	{
        if(Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(this.gameObject);

        SceneManager.sceneLoaded += SceneLoad;
	}

    public string CurrentRoomSceneName;
    public GameObject CameraColliderBoxObject;
    public LevelManager _LevelManager;
    public GameObject _Player;
    public List<EnemyPrefab> _GetEnemyPrefab;
    GameObject _cameraFollower;
    FadeCamera _fadeScript;

    //WhenSceneLoaded
    void SceneLoad(Scene _scene , LoadSceneMode _mode)
    {
        if(_scene.name == "LevelScene")
        {
            _LevelManager = GameObject.Find("LevelManager").GetComponent<LevelManager>();
            _Player = GameObject.Find("Player");
            _cameraFollower = GameObject.Find("CameraFollower");
            //GetFadeScript
            _fadeScript = GetComponent<FadeCamera>();

            //StarttofinishCall
            if(_startToFinish == null)
            {
                _startToFinish = StartCoroutine(IStartToFinish());
            }
            else
            {
                StopCoroutine(_startToFinish);
                _startToFinish = StartCoroutine(IStartToFinish());
            }
        }
    }


	private void Start()
	{
        //Check target PlatformOrEditor
        if(Application.platform == RuntimePlatform.IPhonePlayer | Application.platform == RuntimePlatform.Android)
        {
            isPlatformRun = true;
        }
        else
        {
            isPlatformRun = false;
        }
	}
	//---------------------------------------------------------------------------------------------------------------

	//SetUiPanelStateActive
	public UIPanelState _CurrentUIPanelState;

	private void Update()
	{
        //IfEditor
        if(!isPlatformRun)
        {
            HorizontalAxisC = Input.GetAxis("Horizontal");
            VerticalAxisC = Input.GetAxis("Vertical");


            //A
            if(Input.GetButtonDown("A"))
            {
                RightJoysticDown(0, true);
            }
            if(Input.GetButtonUp("A"))
            {
                RightJoysticDown(0, false);
            }           
            //B
            if (Input.GetButtonDown("B"))
            {
                RightJoysticDown(1, true);
            }
            if (Input.GetButtonUp("B"))
            {
                RightJoysticDown(1, false);
            }           
            //X
            if (Input.GetButtonDown("X"))
            {
                RightJoysticDown(2, true);
            }
            if (Input.GetButtonUp("X"))
            {
                RightJoysticDown(2, false);
            }           
            //Y
            if (Input.GetButtonDown("Y"))
            {
                RightJoysticDown(3, true);
            }
            if (Input.GetButtonUp("Y"))
            {
                RightJoysticDown(3, false);
            }
        }

        LeftJoysticDown();
	}

    //GameStartToFinish>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>
    Coroutine _startToFinish;
    public bool _LoadComplete;
    GameObject _deltaCameraCollider;
    IEnumerator IStartToFinish()
    {
        //LoadStart
        _LoadComplete = false;
        yield return new WaitUntil(() => Instance);
        _Player.SetActive(false);
        //LoadSaveData
        TemporyUnlockDataLoad();
        PermenetUnlockDataLoad();

        //LoadRoomScene
        SceneManager.LoadScene(TemporyUnlockData.LastSaveRoomSceneName, LoadSceneMode.Additive);

        //FindBench
        yield return new WaitUntil(() => _LevelManager._RoomBenchParent != null);
        GameObject _bench = _LevelManager._RoomBenchParent.transform.Find(TemporyUnlockData.LastBenchName).gameObject;
        yield return new WaitUntil(() => _bench != null);

        //SetCameraCollider
        yield return new WaitUntil(() => _deltaCameraCollider != CameraColliderBoxObject);
        _cameraFollower.GetComponent<CameraFollowerScript>().ChangeBlock(CameraColliderBoxObject.GetComponent<BoxCollider>());
        _deltaCameraCollider = CameraColliderBoxObject;

        _Player.transform.position = _bench.transform.position;
        _Player.SetActive(true);
        _fadeScript.FadeOut(1);
        //LoadComplete
        _LoadComplete = true;

        //WhenHealthZero
        yield return new WaitUntil(() => TemporyUnlockData.PlayerHealthValue == 0);
        _fadeScript.FadeIn(1);
        yield return new WaitUntil(() => _fadeScript._start == false);
        SceneManager.LoadScene("LevelScene", LoadSceneMode.Single);

        yield return null;
    }

    //Controls----------------------------------
    public List<ControlButtonMap> ButtonMap;
	public float HorizontalAxisC;
    public float VerticalAxisC;

    bool isPlatformRun;

    //PlatformHoriControl
    public void HorizontalAxisDown(float value)
    {
        if(isPlatformRun)
        {
            HorizontalAxisC = value;
        }
    }
    //PlatformVerticalControl
    public void VerticalAxisDown(float value)
    {
        if (isPlatformRun)
        {
            VerticalAxisC = value;
        }
    }

    bool _verticlePress;
    public event CallAction CallActionWhenLeftJoyDown;
    void LeftJoysticDown()
    {
        //up
        if(VerticalAxisC > 0 && _verticlePress == false)
        {
            CallActionWhenLeftJoyDown(0);
            _verticlePress = true;
        }
        if(VerticalAxisC == 0)
        {
            _verticlePress = false;
        }
    }

    public delegate void CallAction(int _button);
    public event CallAction CallActionPlayer;

    //RightJoysDown
    public void RightJoysticDown(int _button,bool _value)
    {
        ButtonMap[_button].ButtonPress = _value;
        CallActionPlayer(_button);
    }


    //PlayerEnterDoor
    public void PlayerEnterDoore(GameObject _currentParent, GameObject _nextParent, GameObject _nextDoore)
    {
        if(_plyerenterdoor != null)
        {
            StopCoroutine(_plyerenterdoor);
        }
        _plyerenterdoor = StartCoroutine(IPlayerEnterDoor(_currentParent ,_nextParent, _nextDoore));
    }

    Coroutine _plyerenterdoor;
    public bool _doorEnterProcesing = false;
    IEnumerator IPlayerEnterDoor(GameObject _currentParent, GameObject _nextParent,GameObject _nextDoore)
    {
        _doorEnterProcesing = true;
        //CemeraFade
        _fadeScript.FadeIn(0.3f);
        yield return new WaitUntil(() => _fadeScript._start == false);
        _Player.GetComponent<Rigidbody2D>().simulated = false;


        SceneManager.LoadScene(_nextParent.name, LoadSceneMode.Additive);
        CurrentRoomSceneName = _nextParent.name;

        _Player.transform.position = _nextDoore.transform.position;
        _cameraFollower.GetComponent<CameraFollowerScript>().ChangeBlock(_nextParent.GetComponent<BoxCollider>());

        SceneManager.UnloadSceneAsync(_currentParent.name);

        //SetCameraCollider
        yield return new WaitUntil(() => _deltaCameraCollider != CameraColliderBoxObject);
        _cameraFollower.GetComponent<CameraFollowerScript>().ChangeBlock(CameraColliderBoxObject.GetComponent<BoxCollider>());
        _deltaCameraCollider = CameraColliderBoxObject;

        yield return new WaitForSeconds(0.3f);
        _Player.GetComponent<Rigidbody2D>().simulated = true;

        //CemeraFade
        _fadeScript.FadeOut(0.3f);
        yield return new WaitUntil(() => _fadeScript._start == false);

        _doorEnterProcesing = false;
    }


    //PlayerSaveData-----------------------------------------------------------------
    public TemporyDataClass TemporyUnlockData;
    string TemporyFileName = "TemporyUnlock.json";
    public PermenetDataClass PermenetUnlockData;
    string PermenetFileName = "PermenetUnlock.json";

    //TemportyDataLoad
    public void TemporyUnlockDataLoad()
    {
        string _path = Application.persistentDataPath + "/" + TemporyFileName;
        if(File.Exists(_path))
        {
            string _data = File.ReadAllText(_path);
            TemporyUnlockData = JsonUtility.FromJson<TemporyDataClass>(_data);
        }
        else
        {
            TemporyUnlockData.DefaultData();
            string _data = JsonUtility.ToJson(TemporyUnlockData);
            File.WriteAllText(_path, _data);
        }
    }

    //TemportyDataSave
    public void TemporyUnlockDataSave()
    {
        string _path = Application.persistentDataPath + "/" + TemporyFileName;

        string _data = JsonUtility.ToJson(TemporyUnlockData);
        File.WriteAllText(_path, _data);
    }

    //PermenetDataLoad-------
    public void PermenetUnlockDataLoad()
    {
        string _path = Application.persistentDataPath + "/" + PermenetFileName;
        Debug.Log(_path);
        if (File.Exists(_path))
        {
            string _data = File.ReadAllText(_path);
            PermenetUnlockData = JsonUtility.FromJson<PermenetDataClass>(_data);
        }
        else
        {
            PermenetUnlockData.DefaultData();
            string _data = JsonUtility.ToJson(PermenetUnlockData);
            File.WriteAllText(_path, _data);
        }
    }

    //PermenetDataSave
    public void PermenetUnlockDataSave()
    {
        string _path = Application.persistentDataPath + "/" + PermenetFileName;

        string _data = JsonUtility.ToJson(PermenetUnlockData);
        File.WriteAllText(_path, _data);
    }
    //-----------------------------------------------------------------------


    public Material _DamageMat;
    public GameObject _PlayerDamageSound;
    public GameObject _PlayerDamageParticle;
    Coroutine _plyerForceBackCoroutine;
    //PlayerDamage
    public void PlayerDamage(int _amount,GameObject _enemyObj)
    {
        if(_damagePossible)
        {
            TemporyUnlockData.PlayerHealthValue = Mathf.Clamp(TemporyUnlockData.PlayerHealthValue - _amount, 0, PermenetUnlockData.MaxHealthValue);
            //ShakeCamera
            _cameraFollower.GetComponent<Animator>().CrossFade("Shake", 0);

            //SoundCreate
            GameObject _newSound = Instantiate(_PlayerDamageSound);
            _newSound.transform.position = transform.position;

            //ParticleCreat
            GameObject _newPar = Instantiate(_PlayerDamageParticle);
            _newPar.transform.position = _Player.transform.position;

            //ForceBack
            _Player.GetComponent<Player_Controler>().ForceBack(_enemyObj);

            //HitScaleTime
            if(_IdamagePossibleCo != null)
            {
                StopCoroutine(_IdamagePossibleCo);
            }
            _IdamagePossibleCo = StartCoroutine(IDamagePossible());
        }
    }

    bool _damagePossible = true;
    Coroutine _IdamagePossibleCo;
    IEnumerator IDamagePossible()
    {
        _damagePossible = false;
        yield return new WaitForSecondsRealtime(0.5f);
        _damagePossible = true;
    }

    //playerSwordAttack
    public void PlayerSwordAttack(Enemybehaviour _enemy)
    {
        _enemy.DamageMethod(PermenetUnlockData.GetSwordValue());

        //ShakeCamera
        _cameraFollower.GetComponent<Animator>().CrossFade("Shake", 0);
    }

    //playerMagicAttack
    public void PlayerMagicAttack(Enemybehaviour _enemy)
    {
        _enemy.DamageMethod(PermenetUnlockData.GetMagicValue());

        //ShakeCamera
        _cameraFollower.GetComponent<Animator>().CrossFade("Shake", 0);
    }

    //ForcusAdd
    public void ForcusAdd(int _amount)
    {
        TemporyUnlockData.FocusStorageValue = Mathf.Clamp(TemporyUnlockData.FocusStorageValue + _amount, 0, PermenetUnlockData.GetMaxFocusStorageValue());
    }

    //EnterToBench
    public void EnterToBench(GameObject _bench)
    {
        TemporyUnlockData.PlayerHealthValue = PermenetUnlockData.MaxHealthValue;
        TemporyUnlockData.LastSaveRoomSceneName = CurrentRoomSceneName;
        TemporyUnlockData.LastBenchName = _bench.name;
        TemporyUnlockDataSave();
    }

    //ShowMap
    public void ShowMap()
    {
        if(PermenetUnlockData.Map)
        {
            bool unlock = false;

            switch (_LevelManager._CurrentMapRegion)
            {
                case MapRegions.A:
                    unlock = PermenetUnlockData.RegionA;
                    break;
                case MapRegions.B:
                    unlock = PermenetUnlockData.RegionB;
                    break;
            }

            if(unlock)
            {
                _CurrentUIPanelState = UIPanelState.MapPanel;
            }
            else
            {
                _CurrentUIPanelState = UIPanelState.EmptyMap;
            }
        }
    }

    //ShowInventory
    public void ShowInventory()
    {
        if(PermenetUnlockData.Inventory)
        {
            _CurrentUIPanelState = UIPanelState.InventoryPanel;
        }
    }

    //ShowControls
    public void ShowControll()
    {
        _CurrentUIPanelState = UIPanelState.ControlPanel;
    }
}
