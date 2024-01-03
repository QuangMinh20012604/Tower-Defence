using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEditor;
using UnityEngine.UI;
using static UnityEngine.GraphicsBuffer;

public class TurretSlomo : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private LayerMask enemyMask;
    [SerializeField] private GameObject upgradeUI;
    [SerializeField] private Button upgradeButton;
    [SerializeField] private Image _ImageRange;
    [SerializeField] private Text costUpgrade;
    [SerializeField] private Text rangeUpgrade;
    [SerializeField] private Text apsUpgrade;
    [SerializeField] private Text slowspeed;
    [SerializeField] private Text timeFreeze;
    [SerializeField] private Text upgradeOrMax;

    [Header("Attribute")]
    [SerializeField] private float targetingRange = 2f;
    [SerializeField] private float aps = 0.5f; //attacks per second
    [SerializeField] private float freezeTime = 1f;
    [SerializeField] private float slowSpeed = 0.2f;
    [SerializeField] private int baseUpgradeCost = 75;
    [SerializeField] private float perCostUpgrade = 0.7f;
    [SerializeField] private float perApsUpgrade = 0.15f;
    [SerializeField] private float perRangeUpgrade = 0.1f;
    [SerializeField] private float perSlowSpeedUpgrade = 0.25f;
    [SerializeField] private float perFreezeTimeUpgrade = 0.25f;

    private float apsBase;
    private float targetingRangeBase;
    private float slowSpeedBase;
    private float speedEnemy;
    private float freezeTimer;

    private float timeUntilFire;
    private int upgradeCostNext;

    private int level = 1;
    private int levelMaxUpgrade = 6;

    private float widthScale;
    private float heightScale;
    private float scaleRange = 0.023f;

    private void Start()
    {
        widthScale = targetingRange;
        heightScale = targetingRange;
        apsBase = aps;
        targetingRangeBase = targetingRange;
        freezeTimer = freezeTime;
        slowSpeedBase = slowSpeed; 
        upgradeCostNext = baseUpgradeCost;
        baseUpgradeCost = CalculateCost();
        

        speedEnemy = 1f - slowSpeed;

        SetRange();
        TextUpgrade();

        upgradeButton.onClick.AddListener(UpgradeTurretSlowmo);
    }

    private void Update()
    {
        timeUntilFire += Time.deltaTime;

        if (timeUntilFire >= (1f / aps))
        {

            FreezeEnemies();
            timeUntilFire = 0f;
        }
    }

    private void FreezeEnemies()
    {
        RaycastHit2D[] hits = Physics2D.CircleCastAll(transform.position, targetingRange, (Vector2)transform.position, 0f, enemyMask);

        if (hits.Length > 0)
        {
            for(int i = 0; i < hits.Length; i++)
            {
                RaycastHit2D hit = hits[i];

                EnemyMovement em = hit.transform.GetComponent<EnemyMovement>();
                em.UpdateSpeed(speedEnemy);

                StartCoroutine(ResetEnemySpeed(em));
            }
        }
    }

    private IEnumerator ResetEnemySpeed(EnemyMovement em)
    {
        yield return new WaitForSeconds(freezeTime);

        em.resetSpeed();
    }

    public void OpenUpgradeUI()
    {
        upgradeUI.SetActive(true);
    }

    public void CloseUpgradeUI()
    {
        upgradeUI.SetActive(false);
        UIManager.main.SetHoveringState(false);

    }

    public void UpgradeTurretSlowmo()
    {
        if (baseUpgradeCost > LevelManager.main.currency) return;

        LevelManager.main.SpendCurrency(CalculateCost());

        if (level < levelMaxUpgrade)
        {
            level++;
        }
        else
        {
            level = levelMaxUpgrade;
            LevelManager.main.currency += baseUpgradeCost;
        }

        baseUpgradeCost = CalculateCost();
        aps = CalculateAPS();
        targetingRange = CalculateRange();
        freezeTime = CalculateFreezeTime();
        slowSpeed = CalculateSlowSpeed();
        speedEnemy = 1 - CalculateSlowSpeed();

        SetRange();
        TextUpgrade();

        CloseUpgradeUI();


    }

    public int CalculateCost()
    {
        return Mathf.RoundToInt(upgradeCostNext * (1f + level * perCostUpgrade));

    }

    private float CalculateAPS()
    {
        return apsBase * (1f + level * perApsUpgrade);
    }
    private float CalculateFreezeTime()
    {
        return freezeTimer * (1f + level * perFreezeTimeUpgrade);
    }

    private float CalculateRange()
    {
        return targetingRangeBase * (1f + level * perRangeUpgrade);
    }

    private float CalculateSlowSpeed()
    {
        return slowSpeedBase * (1f + level * perSlowSpeedUpgrade);
    }

    private void SetRange()
    {
        widthScale = targetingRange;
        heightScale = targetingRange;
        _ImageRange.rectTransform.localScale = new Vector2(widthScale, heightScale) * scaleRange;
    }

    private void TextUpgrade()
    {

        if (level < levelMaxUpgrade)
        {
            upgradeOrMax.text = "UPGRADE";
            costUpgrade.text = baseUpgradeCost.ToString() + "$";
            apsUpgrade.text = aps.ToString();
            rangeUpgrade.text = targetingRange.ToString();
            timeFreeze.text = freezeTime.ToString();
            slowspeed.text = (CalculateSlowSpeed() * 100).ToString() + "%";
        }
        else
        {
            upgradeOrMax.text = "MAX";
            costUpgrade.text = "MAX";
            apsUpgrade.text = aps.ToString();
        }
    }


    private void OnDrawGizmosSelected()
    {
        Handles.color = Color.cyan;
        Handles.DrawWireDisc(transform.position, transform.forward, targetingRange);
    }
}
