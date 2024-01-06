using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class DotweenScene : MonoBehaviour
{
    [Header("Turret")]
    [SerializeField] private Transform _turretImg;
    [SerializeField] private Transform _bulletImg;
    [SerializeField] private float _turretLength = 2f;
    [SerializeField] private float _turretRotate = 1f;
    [SerializeField] private float _bulletLength = 2f;

    [Header("Enemy")]
    [SerializeField] private Transform _enemyImg;
    [SerializeField] private float _enemyLength = 3.5f; 
    [SerializeField] private float _enemyLength2 = 2f;
    [SerializeField] private Transform _enemyImg2;
    [SerializeField] private float _enemyLength_2 = 4.5f;


    [Header("MenuSystem")]
    [SerializeField] private Transform _backgroundMenu;
    [SerializeField] private Transform _playMenu;
    [SerializeField] private Transform _settingMenu;
    [SerializeField] private Transform _quitMenu;
    [SerializeField] private float _bgLength = 1f; 
    [SerializeField] private float _playLength = 0.5f;
    [SerializeField] private float _settingLength = 0.5f;
    [SerializeField] private float _quitLength = 0.5f;

    [Header("NameGame")]
    [SerializeField] private Transform _nameTurret;
    [SerializeField] private Transform _nameDefense;
    [SerializeField] private float _turretname = 1f;
    [SerializeField] private float _defensename = 1f;



    void Start()
    {
        //----------Turret----------
        _turretImg.DOMove(new Vector3(-7, -0.5f, 0), _turretLength);
        _turretImg.DORotate(new Vector3(0, 0, -83), _turretRotate,RotateMode.WorldAxisAdd).SetDelay(2);

        _bulletImg.DOMove(new Vector3(-3, -1.7f, 0), _bulletLength).SetDelay(3f);

        //----------Enemy----------
        _enemyImg.DOMove(new Vector3(-1.5f, -2.1f, 0), _enemyLength);
        _enemyImg.DOMove(new Vector3(-0.5f, -2.6f, 0), _enemyLength2).SetDelay(4);

        _enemyImg2.DOMove(new Vector3(-5.5f, -3.5f, 0), _enemyLength_2);


        //----------MenuSystem----------
        _backgroundMenu.DOMoveX(5.5f, _bgLength).SetDelay(3.5f);
        _playMenu.DOMoveX(5.5f, _playLength).SetDelay(4);
        _settingMenu.DOMoveX(5.5f, _settingLength).SetDelay(4.2f);
        _quitMenu.DOMoveX(5.5f, _quitLength).SetDelay(4.4f);

        //----------NameGame----------
        _nameDefense.DOMove(new Vector3(-6.0f, 3, 0), _defensename).SetDelay(4);
        _nameTurret.DOMove(new Vector3(-6.0f, 3, 0), _turretname).SetDelay(4.5f);
        _nameDefense.DOMove(new Vector3(-2.5f, 2.5f, 0), _defensename).SetDelay(5.1f);
    }

}
