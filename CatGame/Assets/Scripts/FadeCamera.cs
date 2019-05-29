using UnityEngine;
using System.Collections;

public class FadeCamera : MonoBehaviour
{

    [SerializeField]
    Texture2D _texture;
    [SerializeField]
    float _alpha;


	private void Start()
	{
        if(_texture == null)
        {
            _texture = new Texture2D(1, 1);
        }
       
	}

    void OnGUI()
	{
        _texture.SetPixel(0, 0, new Color(0, 0, 0, _alpha));
        _texture.Apply();
        GUI.DrawTexture(new Rect(0, 0, Screen.width, Screen.height), _texture);
	}

    public void FadeIn(float _speedValue)
    {
        _in = true;
        _time = _speedValue;
        _alpha = 0;
        _start = true;
    }

    public void FadeOut(float _speedValue)
    {
        _in = false;
        _time = _speedValue;
        _alpha = 1;
        _start = true;
    }

    public bool _start;
    [SerializeField]
    bool _in;
    [SerializeField]
    float _time;
    [SerializeField]
    float _tang = 0;
	void Update()
	{
        if(_start)
        {
            if (_tang >= _time)
            {
                _tang = 0;
                _start = false;
            }
            else
            {
                _tang += Time.deltaTime;

                if(_in)
                {
                    _alpha = Mathf.InverseLerp(0, _time, _tang);
                }
                else
                {
                    _alpha = Mathf.InverseLerp(_time, 0, _tang);
                }
            }
        }
	}

    //FadeInFadeOut
    public void StartFadeInFadeOut(float _speed, float _waitTime)
    {
        if(_Co != null)
        {
            StopCoroutine(_Co);
        }
        _Co = StartCoroutine(IfadeinOut(_speed, _waitTime));
    }


    Coroutine _Co;
    IEnumerator IfadeinOut(float _speed, float _waitTime)
    {
        FadeIn(_speed);
        yield return new WaitUntil(() => _start == false);
        yield return new WaitForSeconds(_waitTime);
        FadeOut(_speed);
    }
}