using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicBullet : MonoBehaviour
{
    Rigidbody2D _rb;
	private void Start()
	{
        _rb = GetComponent<Rigidbody2D>();
	}

	private void Update()
	{
        float _dir = GameManager.Instance._Player.GetComponent<Player_Controler>().visualObject.localScale.x;
        _rb.velocity = new Vector2((_dir * 5) + _rb.velocity.x, 0);
	}
}
