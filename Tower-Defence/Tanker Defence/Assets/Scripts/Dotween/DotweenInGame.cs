using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class DotweenInGame : MonoBehaviour
{
    public static DotweenInGame main;

    [Header("SpawnDontEnoughCost")]
    [SerializeField] private Transform _spawnImg;
    [SerializeField] private float _spawnIn = 0.1f;
    [SerializeField] private float _spawnOut = 0.1f;

    private void Awake()
    {
        main = this;
    }
    public void SpawnDontEnoughCost()
    {
        _spawnImg.DOMoveY(800, _spawnIn);
        _spawnImg.DOMoveY(1200, _spawnOut).SetDelay(1.2f);
    }
}
