using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Player_Controler : MonoBehaviour 
{

    Rigidbody2D rb;
    public Animator Animate;
    AudioSource walksource;

    public float movespeed;
    public float jumpspeed;

    public Transform visualObject;
    public Transform groundcheck;
    public float radius;
    public LayerMask WhatisLayer;
    public bool isGround;
    public Transform wallCheck;
    public bool isWall;


    //OnEnable
	private void OnEnable()
	{
        StartCoroutine(WaitforGamemanagerAwake());
	}
    //OnDisabale
	private void OnDisable()
	{
        GameManager.Instance.CallActionPlayer -= ActionChooser;
	}
    //WaitForGamManager
    IEnumerator WaitforGamemanagerAwake()
    {
        yield return new WaitUntil(() => GameManager.Instance);
        GameManager.Instance.CallActionPlayer += ActionChooser;
    }

	void Start () 
    {
        rb = GetComponent<Rigidbody2D>();
        walksource = GetComponent<AudioSource>();
	}

    public bool _PlayerMovement = true;
	private void FixedUpdate()
	{

        //PlayerMove---------------------------------------------------
        if(_PlayerMovement)
        {
            rb.velocity = new Vector2(GameManager.Instance.HorizontalAxisC * movespeed, rb.velocity.y) + _hitForceVelocity;
            //MoveAnimationBool
            if (GameManager.Instance.HorizontalAxisC < 0 | GameManager.Instance.HorizontalAxisC > 0)
            {
                Animate.SetBool("Run", true);
            }
            else
            {
                Animate.SetBool("Run", false);
            }

            //PlayerTurn
            if (GameManager.Instance.HorizontalAxisC > 0)
            {
                visualObject.transform.localScale = new Vector3(1, 1, 1);
            }
            if (GameManager.Instance.HorizontalAxisC < 0)
            {
                visualObject.transform.localScale = new Vector3(-1, 1, 1);
            }
        }


        isGround = Physics2D.OverlapCircle(groundcheck.transform.position, radius, WhatisLayer);
        isWall = Physics2D.OverlapCircle(wallCheck.transform.position, 0.3f, WhatisLayer);

        //walksound
        WalkSound();

        //Jump
        Jump();

        //Fallingacc
        fallingAcc();

        //waitbuttonhold
        waitButtonHold();

        //locakFaling
        Dash();
	}

    //WalkSound
    void WalkSound()
    {
        
        if(isGround && Animate.GetBool("Run"))
        {
            walksource.enabled = true;
        }
        else
        {
            walksource.enabled = false;
        }
    }

    //PlayerForceBack
    Coroutine _plyerForceBackCoroutine;
    Vector2 _hitForceVelocity;
    public void ForceBack(GameObject _enemyObj)
    {
        Vector2 _dir = (transform.position - _enemyObj.transform.position).normalized;
        if(_plyerForceBackCoroutine != null)
        {
            StopCoroutine(_plyerForceBackCoroutine);
        }
        _plyerForceBackCoroutine = StartCoroutine(IForceBack(_dir));
    }
    IEnumerator IForceBack(Vector2 _dir)
    {
        _hitForceVelocity = _hitForceVelocity + _dir * 10;
        yield return new WaitForSeconds(0.05f);
        _hitForceVelocity = Vector2.zero;
    }

    //CallAction
    public void ActionChooser(int _button)
    {
        //Button A
        if(_button == 0)
        {
            if (GameManager.Instance.ButtonMap[_button].ButtonPress)
            {
                if (_healormagicCoroutine != null)
                {
                    StopCoroutine(_healormagicCoroutine);
                }
                _healormagicCoroutine = StartCoroutine(IhealOrMagic(_button));
            }
        }
        //Button B
        if (_button == 1)
        {
            SwardSwing(_button);
        }
        //Button X
        if (_button == 2)
        {
            IjumpVar = StartCoroutine(IJump(_button));
        }
        //Button Y
        if(_button == 3)
        {
            Dash(_button);
        }
    }
   
    //healOrMagic
    Coroutine _healormagicCoroutine;
    IEnumerator IhealOrMagic(int _buttonIndex)
    {
        _buttonHold = false;
        _waitStart = true;
        _Holdtime = 0;
        yield return new WaitUntil(() => GameManager.Instance.ButtonMap[_buttonIndex].ButtonPress == false | _buttonHold);
        _waitStart = false;
        //IfHold
        if(_buttonHold)
        {
            Heal(_buttonIndex);
        }
        else
        {
            //IfPressReales
            Magic(_buttonIndex);
        }
    }
    bool _buttonHold;
    bool _waitStart;
    float _Holdtime;
    void waitButtonHold()
    {
        if(_buttonHold == false && _waitStart)
        {
            if(_Holdtime < 0.5f)
            {
                _Holdtime += Time.deltaTime;
            }
            else
            {
                _buttonHold = true;
            }
        }
    }

    //Dash----------------------------------------
    Coroutine _playerDashCoroutine;
    void Dash(int _buttonIndex)
    {
        if (GameManager.Instance.ButtonMap[_buttonIndex].ButtonPress)
        {
            if (_playerDashCoroutine != null)
            {
                StopCoroutine(_playerDashCoroutine);
            }
            StartCoroutine(IDash());
        }
    }
    IEnumerator IDash()
    {
        /*
        _FallingLockDash = true;
        rb.isKinematic = true;
        rb.velocity = new Vector2(0, 0);
        yield return new WaitUntil(() => rb.isKinematic == true && rb.velocity == Vector2.zero && _FallingLockDash == true);
        Animate.CrossFade("Dash", 0);
        _dashFoeceVelocity = _dashFoeceVelocity + new Vector2(visualObject.localScale.x, 0) * 10;
        yield return new WaitForSeconds(0.15f);
        _dashFoeceVelocity = Vector2.zero;
        _FallingLockDash = false;
        rb.isKinematic = false;*/

        _PlayerMovement = false;
        rb.isKinematic = true;
        _dashCountdown = 0;
        _DashRunning = true;
        Animate.CrossFade("Dash", 0);
        yield return new WaitUntil(() => _DashRunning == false);
        rb.isKinematic = false;
        _PlayerMovement = true;
    }
    public float _dashCountdown;
    void Dash()
    {
        if(_DashRunning)
        {
            if(isWall)
            {
                _DashRunning = false;
            }
            else
            {
                rb.velocity = new Vector2(visualObject.transform.localScale.x * 15, 0);

                _dashCountdown += Time.deltaTime;
                if(_dashCountdown > 0.3f)
                {
                    _DashRunning = false;
                }
            }
        }
    }

    //Heal-------------------------------------------
    void Heal(int _buttonIndex)
    {
        if (GameManager.Instance.PermenetUnlockData.MaxHealthValue > GameManager.Instance.TemporyUnlockData.PlayerHealthValue && GameManager.Instance.TemporyUnlockData.FocusStorageValue >= 2)
        {
            Animate.CrossFade("Heal", 0);
        }
    }

    //Magic-----------------------------------------
    void Magic(int _button)
    {
        if (GameManager.Instance.TemporyUnlockData.FocusStorageValue >= 2)
        {
            Animate.CrossFade("Magic", 0);
            GameManager.Instance.TemporyUnlockData.FocusStorageValue -= 2;
        }
    }

    //SwardSwing---------------------------------------
    void SwardSwing(int _buttonIndex)
    {
        if(GameManager.Instance.ButtonMap[_buttonIndex].ButtonPress)
        {
            Animate.SetTrigger("Sword");
        }
    }

    //NewJump----------------------------------------------
    Coroutine IjumpVar;
    bool _jumpBoolIjump;
    bool _fallingBoolIjump;
    float _jumpspeedIjump;
    public float _tangIjump;
    IEnumerator IJump(int _buttonIndex)
    {
        if (/*!isWall&& */isGround && GameManager.Instance.ButtonMap[_buttonIndex].ButtonPress)
        {
            _tangIjump = 0;
            _jumpBoolIjump = true;
            yield return new WaitUntil(() => GameManager.Instance.ButtonMap[_buttonIndex].ButtonPress == false | _tangIjump == 1f | rb.velocity.y < 0);
            _jumpBoolIjump = false;
            _fallingAcc = false;
            _tangIjump = 0;
            _fallingBoolIjump = true;
            Animate.SetTrigger("Landing");
            yield return new WaitUntil(() => isGround);
            _fallingBoolIjump = false;
            _fallingAcc = true;
            Animate.SetBool("Jump", false);
            yield return new WaitUntil(() => GameManager.Instance.ButtonMap[_buttonIndex].ButtonPress == false);
        }
    }
    void Jump()
    {
        //GoUp
        if (_jumpBoolIjump)
        {
            _tangIjump = Mathf.Clamp(_tangIjump + 0.025f, 0, 1);
            _jumpspeedIjump = Mathf.Lerp(jumpspeed, 0, _tangIjump);
            rb.velocity = new Vector2(rb.velocity.x,_jumpspeedIjump);
            Animate.SetBool("Jump", true);
            Animate.CrossFade("Jump", 0f);
        }
        //WhenGodown
        if (_fallingBoolIjump && _DashRunning == false)
        {
            _tangIjump = Mathf.Clamp(_tangIjump + 0.025f, 0, 1);
            _jumpspeedIjump = Mathf.Lerp(_jumpspeedIjump, jumpspeed * -1, _tangIjump);
            rb.velocity = new Vector2(rb.velocity.x, _jumpspeedIjump);
        }

    }

    /*
    //NewWallJump---------------------------------------
    bool _IWallJumpStated;
    float flip = 1;
    IEnumerator IWallJump(int _buttonIndex)
    {
        if(!_IWallJumpStated)
        {
            yield return new WaitUntil(() => !isGround && isWall);
            _fallingAcc = false;
            _fallingBoolIjump = false;
            _jumpBoolIjump = false;
            _PlayerMovement = false;
            _IWallJumpStated = true;
            rb.velocity = new Vector2(rb.velocity.x, 0);
            yield return new WaitUntil(() => isGround);
            _IWallJumpStated = false;
            _PlayerMovement = true;
        }
        else
        {
            if(GameManager.Instance.ButtonMap[_buttonIndex].ButtonPress && isWall)
            {
                if(GameManager.Instance.HorizontalAxisC == 0)
                {
                    flip = flip * -1;
                    visualObject.transform.localScale = new Vector3(flip, 1, 1);
                    rb.velocity = new Vector2(6 * flip, 6);
                }
                else
                {
                    if (visualObject.transform.localScale.x == GameManager.Instance.HorizontalAxisC)
                    {
                        rb.velocity = new Vector2(6 * GameManager.Instance.HorizontalAxisC, 6);
                    }
                    else
                    {
                        _PlayerMovement = true;
                        _IWallJumpStated = false;
                    }
                }
            }
            else
            {
                if (visualObject.transform.localScale.x != GameManager.Instance.HorizontalAxisC && GameManager.Instance.HorizontalAxisC != 0)
                {
                    _PlayerMovement = true;
                    _IWallJumpStated = false;
                }
            }

            if(!isWall && !isGround && visualObject.transform.localScale.x == GameManager.Instance.HorizontalAxisC)
            {
                _PlayerMovement = true;
                _IWallJumpStated = false;
            }
        }
    }
    */
    //FallingAcc-----------------------------------------
    bool _DashRunning = false;
    bool _fallingAcc = true;
    bool groundHit;
    void fallingAcc()
    {
        if (_fallingAcc && _DashRunning == false)
        {
            if (rb.velocity.y < 0)
            {
                rb.velocity = new Vector2(rb.velocity.x, Mathf.Clamp(rb.velocity.y - 1, -11, 0));
            }
        }

        if(groundHit && isGround)
        {
            Animate.SetTrigger("GroundHit");
            groundHit = false;
        }
        else if(!isGround && !groundHit)
        {
            groundHit = true;
        }
    }
}
