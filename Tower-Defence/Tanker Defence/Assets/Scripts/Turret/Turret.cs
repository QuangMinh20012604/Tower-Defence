using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using TMPro;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class Turret : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Transform turretRotationPoint;
    [SerializeField] private LayerMask enemyMask;
    [SerializeField] private GameObject bulletPreFab;
    [SerializeField] private Transform firingPoint;
    [SerializeField] private GameObject upgradeUI;
    [SerializeField] private Button upgradeButton;
    [SerializeField] private Image _ImageRange;
    [SerializeField] private Text costUpgrade;
    [SerializeField] private Text rangeUpgrade;
    [SerializeField] private Text bpsUpgrade;
    [SerializeField] private Text upgradeOrMax;


    [Header("Attribute")]
    [SerializeField] private float targetingRange = 3.5f;
    [SerializeField] private float rotationSpeed = 200f;
    [SerializeField] private float bps = 1f;    //Bullets Per Second
    [SerializeField] private int baseUpgradeCost = 100;
    [SerializeField] private float perCostUpgrade = 0.7f;
    [SerializeField] private float perBpsUpgrade = 0.15f;
    [SerializeField] private float perRangeUpgrade = 0.1f;

    private float bpsBase;
    private float targetingRangeBase;
 
    private Transform target;  
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
        bpsBase = bps;
        targetingRangeBase = targetingRange;

        upgradeCostNext = baseUpgradeCost;
        baseUpgradeCost = CalculateCost();



        SetRange();
        TextUpgrade();

        upgradeButton.onClick.AddListener(UpgradeTurret);
    }

    private void Update()
    {
        if (target == null)
        {
            FindTarget();
            return;
        }

        RotateTowardsTarget();

        if (!CheckTargetIsInRange())
        {
            target = null;
        }
        else
        {
            timeUntilFire += Time.deltaTime;

            if (timeUntilFire > (1f / bps))
            {
                Shoot();
                timeUntilFire = 0f;
            }
        }
    }

    private void Shoot()
    {
        GameObject bulletObj = Instantiate(bulletPreFab, firingPoint.position, Quaternion.identity);
        Bullet bulletScript = bulletObj.GetComponent<Bullet>();
        bulletScript.SetTarget(target);
    }

    private void FindTarget()
    {
        RaycastHit2D[] hits = Physics2D.CircleCastAll(transform.position, targetingRange, (Vector2)transform.position, 0f, enemyMask);

        if (hits.Length > 0)
        {
            target = hits[0].transform;
        }
    }

    private bool CheckTargetIsInRange()
    {
        return Vector2.Distance(target.position, transform.position) <= targetingRange;
    }

    private void RotateTowardsTarget()
    { 
        /*
        
        if (EnemyMovement.main.currentEnemyTarget == null)
        {
            return;
        }
        Vector3 targetPos = EnemyMovement.main.currentEnemyTarget.transform.position - transform.position;
        float angle = Vector3.SignedAngle(transform.up, targetPos, transform.forward);
        Quaternion targetRotation = Quaternion.Euler(new Vector3(0f, 0f, angle));
        turretRotationPoint.rotation = Quaternion.RotateTowards(turretRotationPoint.rotation, targetRotation, rotationSpeed * Time.deltaTime);
        */
        float angle = Mathf.Atan2(target.position.y - transform.position.y, target.position.x - transform.position.x) * Mathf.Rad2Deg - 90f;

        Quaternion targetRotation = Quaternion.Euler(new Vector3(0f, 0f, angle));
        turretRotationPoint.rotation = Quaternion.RotateTowards(turretRotationPoint.rotation, targetRotation, rotationSpeed * Time.deltaTime);
        
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

    public void UpgradeTurret()
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
        bps = CalculateBPS();
        targetingRange = CalculateRange();

        SetRange();
        TextUpgrade();

        CloseUpgradeUI();

    }

    private int CalculateCost()
    {
        return Mathf.RoundToInt(upgradeCostNext * (1f + level * perCostUpgrade));
        
    }

    private float CalculateBPS()
    {
        return bpsBase * (1f + level * perBpsUpgrade);
    }

    private float CalculateRange()
    {
        return targetingRangeBase * (1f + level * perRangeUpgrade);
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
            bpsUpgrade.text = bps.ToString();
            rangeUpgrade.text = targetingRange.ToString();
        }
        else
        {
            upgradeOrMax.text = "MAX";
            costUpgrade.text = "MAX";
        }
        
    }  

    
    private void OnDrawGizmosSelected()
    {
        Handles.color = Color.cyan;
        Handles.DrawWireDisc(transform.position, transform.forward, targetingRange);
    }
}
