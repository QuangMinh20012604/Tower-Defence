using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plot : MonoBehaviour
{
    [Header("References")]
    public SpriteRenderer sr;
    [SerializeField] private Color hoverColor;

    private GameObject towerObj;
    private Turret turret;
    private TurretSlomo turretSlomo;
    private Color startColor;

    private void Start()
    {
        startColor = sr.color;
    }

    private void OnMouseEnter()
    {
        if (MenuUI.GameIsPause == false)
        {
            sr.color = hoverColor;
        }
        else return;

    }
    private void OnMouseExit()
    {
        if (MenuUI.GameIsPause == false)
        {
            sr.color = startColor;
        }
        else return;

    }

    private void OnMouseDown()
    {
        if (MenuUI.GameIsPause == false)
        {
            if (UIManager.main.IsHoveringUI()) return;

            if (towerObj != null)
            {
                if (towerObj.tag == "Turret")
                {
                    turret.OpenUpgradeUI();
                }
                else if (towerObj.tag == "SlowmoTurret")
                {
                    turretSlomo.OpenUpgradeUI();
                }
                return;
            }

            Tower towerToBuild = BuildManager.main.GetSelectedTower();

            if (towerToBuild.cost > LevelManager.main.currency)
            {
                Debug.Log("You can't afford this tower");
                DotweenInGame.main.SpawnDontEnoughCost();
                return;
            }

            LevelManager.main.SpendCurrency(towerToBuild.cost);

            towerObj = Instantiate(towerToBuild.prefab, transform.position, Quaternion.identity);
            if (towerObj.tag == "Turret")
            {
                turret = towerObj.GetComponent<Turret>();
            }
            else if (towerObj.tag == "SlowmoTurret")
            {
                turretSlomo = towerObj.GetComponent<TurretSlomo>();
            }
        }
        else return;
        
    }
}
