using System.Collections;
using System.Collections.Generic;
using UnityEngine;

enum TriggerID 
{
    EnemyAttack,PlayerSword,playerMagic,EnemyGroundCheck,EnemyFrontGroundCheck,EnemyFrontPlayerCheck,EnemyWallCheck,
    UnlockMap,UnlockInventory
}

public class DamageTrigger : MonoBehaviour 
{
    [SerializeField]
    TriggerID ID;

    [SerializeField]
    Enemybehaviour _enemyBehaviour;

    [SerializeField]
    GameObject _createObject;
    [SerializeField]
    GameObject _createObjectPosition;

    [SerializeField]
    bool _FollowTransform;
    [SerializeField]
    Transform _Target;


	private void Update()
	{
        if(_FollowTransform)
        {
            transform.position = _Target.transform.position;
        }
	}

	//TrigerEnter
	private void OnTriggerEnter2D(Collider2D _hitCollider)
	{
        //EnemyAttack
        if(ID == TriggerID.EnemyAttack)
        {
            _enemyBehaviour.AttackMethod(_hitCollider);
        }
        //PlayerSword
        if(ID == TriggerID.PlayerSword)
        {
            if(_hitCollider.tag == "Enemy")
            {
                Enemybehaviour _hitEnemy = _hitCollider.GetComponent<Enemybehaviour>();
                GameManager.Instance.PlayerSwordAttack(_hitEnemy);

                GameObject _newobj = Instantiate(_createObject);
                _newobj.transform.position = _createObjectPosition.transform.position;
            }
        }
        //PlayerMagic
        if (ID == TriggerID.playerMagic)
        {
            if (_hitCollider.tag == "Enemy")
            {
                Enemybehaviour _hitEnemy = _hitCollider.GetComponent<Enemybehaviour>();
                GameManager.Instance.PlayerMagicAttack(_hitEnemy);
            }
        }
        //EnemyGroundCheck
        if (ID == TriggerID.EnemyGroundCheck)
        {
            if (_hitCollider.tag == "Ground")
            {
                _enemyBehaviour._InGround = true;
            }
        }

        //EnemyFrontGroundCheck
        if (ID == TriggerID.EnemyFrontGroundCheck)
        {
            if (_hitCollider.tag == "Ground")
            {
                _enemyBehaviour._DetectFrontGround = true;
            }
        }

        //EnemyFrontPlayerCheck
        if (ID == TriggerID.EnemyFrontPlayerCheck)
        {
            if (_hitCollider.tag == "Player")
            {
                _enemyBehaviour._DetectFrontPlayer = true;
            }
        }

        //EnemyFrontWallCheck
        if (ID == TriggerID.EnemyWallCheck)
        {
            if (_hitCollider.tag == "Ground")
            {
                _enemyBehaviour._DetectFrontWall = true;
            }
        }

        //Unlock____________________________________________________________________________________
        //UnlockMap
        if(ID == TriggerID.UnlockMap)
        {
            if(_hitCollider.tag == "Player")
            {
                GameManager.Instance.PermenetUnlockData.Map = true;
                GameManager.Instance.PermenetUnlockDataSave();
            }
        }

        //UnlockInventory
        if (ID == TriggerID.UnlockInventory)
        {
            if (_hitCollider.tag == "Player")
            {
                GameManager.Instance.PermenetUnlockData.Inventory = true;
                GameManager.Instance.PermenetUnlockDataSave();
            }
        }
	}

    //TrigerExit
    private void OnTriggerExit2D(Collider2D _hitCollider)
	{
        //EnemyGround
        if (ID == TriggerID.EnemyGroundCheck)
        {
            if (_hitCollider.tag == "Ground")
            {
                _enemyBehaviour._InGround = false;
            }
        }

        //EnemyGround
        if (ID == TriggerID.EnemyFrontGroundCheck)
        {
            if (_hitCollider.tag == "Ground")
            {
                _enemyBehaviour._DetectFrontGround = false;
            }
        }

        //EnemyFrontPlayerCheck
        if (ID == TriggerID.EnemyFrontPlayerCheck)
        {
            if (_hitCollider.tag == "Player")
            {
                _enemyBehaviour._DetectFrontPlayer = false;
            }
        }

        //EnemyWall
        if (ID == TriggerID.EnemyWallCheck)
        {
            if (_hitCollider.tag == "Ground")
            {
                _enemyBehaviour._DetectFrontWall = false;
            }
        }
	}

}
