using UnityEngine;

[System.Serializable]
public class TowerData
{
    public GameObject towerPrefab;
    public int cost;
    public GameObject towerUpgradePrefab;
    public int upgradeCost;
    public TowerType type;
}

public enum TowerType
{
    type1,
    type2,
    type3
}
