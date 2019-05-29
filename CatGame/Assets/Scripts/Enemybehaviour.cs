using System.Collections;
using System.Collections.Generic;
using UnityEngine;

enum MainState
{
    Start,
    Idle,
    Attack,
    Dead
}

enum EnemyCatagory
{
    Normal
}

[RequireComponent(typeof(AudioSource))]
public class Enemybehaviour : MonoBehaviour 
{
    [SerializeField]
    EnemyCatagory _EnemyCatagory;

    [SerializeField]
    int HealthValue;
    int MaxHealth;
    [SerializeField]
    int ForcusValue;
    [SerializeField]
    int AttackValue;
    [SerializeField]
    int CoinsValue;


    [SerializeField]
    MainState _MainState;
    MainState _deltaMstate;

    public bool _InGround;
    public bool _DetectFrontGround;
    public bool _DetectFrontWall;
    public bool _DetectFrontPlayer;
    public Vector2 _CurrentVelocity;

    Rigidbody2D rb;
    Animator animat;
    Vector3 spawnPos;
    AudioSource audioSor;
    Material originalMat;

    void Start()
	{
        animat = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        spawnPos = transform.position;
        MaxHealth = HealthValue;
        audioSor = GetComponent<AudioSource>();
        originalMat = _mesh.material;
        rb.velocity = new Vector3(1, 0, 0);
	}

    void Update()
	{
        if(_deltaMstate != _MainState)
        {
            switch (_MainState)
            {
                //ifIdle
                case MainState.Idle:
                    animat.SetInteger("MainState", 0);
                    animat.CrossFade("START", 0);
                    _deltaMstate = _MainState;
                    break;


                //attack
                case MainState.Attack:
                    animat.SetInteger("MainState", 1);
                    animat.CrossFade("START", 0);
                    _deltaMstate = _MainState;
                    break;


                //dead
                case MainState.Dead:
                    animat.SetInteger("MainState", 2);
                    animat.CrossFade("START", 0);
                    _deltaMstate = _MainState;
                    break;
            }
        }
	}

	private void FixedUpdate()
	{
        rb.velocity = _hitForceVelocity + _CurrentVelocity;
	}

    //NormalaCatogory>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>
    //TurnCharacterWhenGroundFalse
    public void BehaviourUpdates(bool _isMoveTowerd,Vector2 _moveTowerdsPos,bool _IsVelocity,Vector2 _VelocityValue,bool _IsTurnFrontNoGround, bool _IsTurnFrontWallDetect,bool _IsPlayerFrontCheck,
                                 bool _IsPlayerDistanceCheck, float _MaxDistance)
    {
        //MoveTowerd
        if(_isMoveTowerd)
        {
            Vector3 _v3pos = Vector2.MoveTowards(transform.position, _moveTowerdsPos,0.1f);
            transform.position = new Vector3(_v3pos.x, _v3pos.y, 0);
        }

        //Velocity
        if(_IsVelocity)
        {
            _CurrentVelocity = new Vector2(transform.localScale.x * _VelocityValue.x, _VelocityValue.y);
        }

        //TurnWhenNoGround
        if(_IsTurnFrontNoGround)
        {
            if (_DetectFrontGround == false)
            {
                
                if (_flip != null)
                {
                    StopCoroutine(_flip);
                }
                StartCoroutine(Iflip());
            }
        }

        //TurnWhenWallDetect
        if (_IsTurnFrontWallDetect)
        {
            if (_DetectFrontWall == true)
            {

                if (_flip != null)
                {
                    StopCoroutine(_flip);
                }
                StartCoroutine(Iflip());
            }
        }

        //CheckPlayerFront
        if(_IsPlayerFrontCheck)
        {
            if(_DetectFrontPlayer)
            {
                if(_MainState != MainState.Attack && _MainState != MainState.Dead)
                {
                    _MainState = MainState.Attack;
                }
            }
        }

        //RangeChrck
        if(_IsPlayerDistanceCheck)
        {
            if (_MaxDistance < Vector2.Distance(GameManager.Instance._Player.transform.position, transform.position))
            {
                if(_MainState == MainState.Attack && _MainState != MainState.Dead)
                {
                    _MainState = MainState.Idle;
                }
            }

            if(_MaxDistance > Vector2.Distance(GameManager.Instance._Player.transform.position, transform.position))
            {
                if (_MainState == MainState.Idle && _MainState != MainState.Dead)
                {
                    _MainState = MainState.Attack;
                }
            }
        }

    }

    public void BehaviourStart(bool _IsDieCheck,bool _IsTurnToPlayer)
    {
                
        //DieCheck
        if(_IsDieCheck)
        {
            Die();
        }

        //TurnPlayer
        if (_IsTurnToPlayer)
        {
            Vector2 _dir = (GameManager.Instance._Player.transform.position - transform.position).normalized;
            if (_dir.x != transform.localScale.x)
            {
                if(_dir.x < 0)
                {
                    transform.localScale = new Vector3(-1, 1, 1);
                }
                if (_dir.x > 0)
                {
                    transform.localScale = new Vector3(1, 1, 1);
                }

            }
        }
    }

    Coroutine _flip;
    IEnumerator Iflip()
    {
        transform.localScale = new Vector3(transform.localScale.x * -1, 1, 1);
        yield return new WaitUntil(() => _DetectFrontGround == true);
    }

    //.......................................................................................................................................

    //StateBehviourWait---------------------------
    public void _WaitStateTime(float _wait,int _nextState)
    {
        if (_Wait != null)
        {
            StopCoroutine(IWait(_wait,_nextState));
        }
        StartCoroutine(IWait(_wait,_nextState));
    }

    Coroutine _Wait;
    IEnumerator IWait(float _StateTime , int _nextStateInt)
    {
        yield return new WaitForSeconds(_StateTime);
        animat.SetInteger("NextState", _nextStateInt);
    }

    //Die
    [SerializeField]
    GameObject _dieParticle;
    [SerializeField]
    GameObject _coinObject;
    void Die()
    {
        for (int i = 0; i < CoinsValue; i++)
        {
            GameObject _newobj = Instantiate(_coinObject);
            _newobj.transform.position = transform.position + _ParticleObjectOffset;
            _newobj.GetComponent<Rigidbody2D>().AddForce(new Vector2(UnityEngine.Random.Range(-300, 300), UnityEngine.Random.Range(1000, 2000)));
        }

        GameObject _newDieParticleobj = Instantiate(_dieParticle);
        _newDieParticleobj.transform.position = transform.position + _ParticleObjectOffset;

        Destroy(this.gameObject);
    }

	//Attack--------------------------
	public void AttackMethod(Collider2D _hitCollider)
    {
        if(_hitCollider.tag == "Player" && _MainState != MainState.Dead)
        {
            GameManager.Instance.PlayerDamage(AttackValue,this.gameObject);
        }
    }
    //Damage
    [SerializeField]
    GameObject _damageParticleObject;
    [SerializeField]
    GameObject _damageSoundObject;
    [SerializeField]
    Vector3 _ParticleObjectOffset;
    [SerializeField]
    SkinnedMeshRenderer _mesh;
    public void DamageMethod(int _Value)
    {
        if(_MainState != MainState.Dead)
        {
            if (_MainState != MainState.Attack)
            {
                _MainState = MainState.Attack;
            }

            HealthValue = Mathf.Clamp(HealthValue - _Value, 0, MaxHealth);
            if (HealthValue == 0)
            {
                _MainState = MainState.Dead;
            }

            //ForceBack
            Vector2 _dir = (transform.position - GameManager.Instance._Player.transform.position).normalized;
            if (_enemyForceBackCoroutine != null)
            {
                StopCoroutine(_enemyForceBackCoroutine);
            }
            _enemyForceBackCoroutine = StartCoroutine(IForceBack(_dir));

            //ParticleCreate
            GameObject _newPar = Instantiate(_damageParticleObject);
            _newPar.transform.position = transform.position + _ParticleObjectOffset;

            //SoundCreate
            GameObject _newSound = Instantiate(_damageSoundObject);
            _newSound.transform.position = transform.position;

            //FlashEffect
            StartCoroutine(IHitEffect());

            //AddForcus
            GameManager.Instance.ForcusAdd(ForcusValue);
        }

    }
    Vector2 _hitForceVelocity;
    Coroutine _enemyForceBackCoroutine;
    IEnumerator IForceBack(Vector2 _dir)
    {
        _hitForceVelocity = _hitForceVelocity + _dir * 10;
        yield return new WaitForSecondsRealtime(0.05f);
        _hitForceVelocity = Vector2.zero;
    }

    //hiteffect
    IEnumerator IHitEffect()
    {
        for (int i = 0; i < 3; i++)
        {
            _mesh.material = GameManager.Instance._DamageMat;
            yield return new WaitForSeconds(0.05f);
            _mesh.material = originalMat;
            yield return new WaitForSeconds(0.05f);
        }
    }

}
