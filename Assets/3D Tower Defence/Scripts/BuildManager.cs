using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class BuildManager : MonoBehaviour
{
    [SerializeField] TowerData tower01;
    [SerializeField] TowerData tower02;
    [SerializeField] TowerData tower03;

    // The tower we selected on the UI to get witch tower we want to build
    TowerData selectedTowerOnUI;

    // The base we selected in the scene to get witch base we want to build tower on
    TowerBase selectedTowerBase;

    // How many money we have when we start
    [SerializeField] int money = 1000;
    [SerializeField] Text moneyText;

    // Upgrades
    [SerializeField] GameObject upgradeCanvas;
    [SerializeField] Button upgradeButton;

    void ChangeMoney(int _change)
    {
        money += _change;
        moneyText.text = "$" + money;
    }

    private void Start()
    {
        // Set the default selected to the frist one
        selectedTowerOnUI = tower01;
    }

    void Update()
    {
        // When we left click
        if (Input.GetMouseButtonDown(0))
        {
            // When we not clicking the UI
            if (EventSystem.current.IsPointerOverGameObject() == false)
            {
                // Check where we click
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;

                // If we hit the base then return ture
                bool isCollider = Physics.Raycast(ray, out hit, 1000, LayerMask.GetMask("TowerBase"));
                if (isCollider)
                {
                    // Get the base we Clicked
                    TowerBase clickedTowerBase = hit.collider.GetComponent<TowerBase>();

                    // If there is no tower on the base and we have select a tower on the UI
                    if (selectedTowerOnUI != null && clickedTowerBase.towerGameObject == null)
                    {
                        // We have enough money to build the tower
                        if (money > selectedTowerOnUI.cost)
                        {
                            // We spend the money and build the tower
                            ChangeMoney(-selectedTowerOnUI.cost);
                            clickedTowerBase.BuildTower(selectedTowerOnUI);
                        }
                    }
                    // If there is a tower on the base
                    else if (clickedTowerBase.towerGameObject != null)
                    {
                        // If we have click the base two times
                        if (clickedTowerBase == selectedTowerBase && upgradeCanvas.activeInHierarchy)
                        {
                            // Close the Upgrade canvas
                            upgradeCanvas.SetActive(false);
                        }
                        // If we didnt click this base before then show upgrade UI
                        else
                        {
                            // Pass in the position of the base and bool that we have upgraded or not
                            ShowUpgradeUI(clickedTowerBase.transform.position, clickedTowerBase.isUpgraded);
                        }

                        // Shows we have clicked this base once
                        selectedTowerBase = clickedTowerBase;
                    }
                }
            }
        }
    }

    public void Tower01Selected(bool _selected)
    {
        if (_selected)
        {
            selectedTowerOnUI = tower01;
        }
    }

    public void Tower02Selected(bool _selected)
    {
        if (_selected)
        {
            selectedTowerOnUI = tower02;
        }
    }

    public void Tower03Selected(bool _selected)
    {
        if (_selected)
        {
            selectedTowerOnUI = tower03;
        }
    }

    void ShowUpgradeUI(Vector3 _basePosition, bool _isUpgraded = false)
    {
        // Open up the upgrade canvas
        upgradeCanvas.SetActive(true);

        // Set the canvas right on top of the towerbase
        upgradeCanvas.transform.position = _basePosition + Vector3.up;

        // when we  have upgraded the tower already, then set the upgrade button interactable to false, so we cant click it
        upgradeButton.interactable = !_isUpgraded;
    }

    public void OnUpgradeButtonDown()
    {
        // If we click the upgrade button and have enough money
        if (money >= selectedTowerBase.towerData.upgradeCost)
        {
            // Upgrade the tower and change the money
            ChangeMoney(-selectedTowerBase.towerData.upgradeCost);
            selectedTowerBase.UpgradeTower();
        }

        // Close the upgrade menu
        upgradeCanvas.SetActive(false);
    }

    public void OnDestoryButtonDown()
    {
        // If we click destory then destory the button and close the upgrade menu
        selectedTowerBase.DestoryTower();
        upgradeCanvas.SetActive(false);
    }
}
