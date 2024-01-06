using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class test : MonoBehaviour
{
    /*
    public static EnemySpawner Enemy;
    [SerializeField] Transform[] _enemy;

    void Update()
    {
        GetCurrentEnemyTarget();
        RotateTurretTarget();
    }

    private void GetCurrentEnemyTarget()
    {
        if (_enemy.count <= 0)
        {
            CurrentEnemyTarget = null;
        }
        CurrentEnemyTarget = _enemies[0];
    }

    private void RotateTurretTarget()
    {
        if (CurrentEnemyTarget == null)
        {
            return;
        }
        Vector3 targetPos = Enemy.main.CurrentEnemyTarget.transform.position - transform.position;
        float angle = Vector3.SignedAngle(transform.up, targetPos, transform.forward);
        transform.Rotate(0,0,angle);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Enemy")
        {
            Enemy enemy = other.GetComponent<EnemyMovement>();
            if(_enemies.Contains(enemy)) 
            {
                _Enemy.Remove(enemy);    
            }
        }
    }
    */
}
