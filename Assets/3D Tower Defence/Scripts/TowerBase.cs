using UnityEngine;
using UnityEngine.EventSystems;

public class TowerBase : MonoBehaviour
{
    // Tower that is on the base
    public GameObject towerGameObject;

    // Renderer of the base to change the color
    Renderer rend;

    // Color that the base original color
    Color originalColor;

    // Check if its the tower on the base upgraded or not
    public bool isUpgraded = false;

    // Get witch tower is
    public TowerData towerData;

    void Start()
    {
        // Get the renderer of the base and set the original color to its default color
        rend = GetComponent<Renderer>();
        originalColor = rend.material.color;
    }

    public void BuildTower(TowerData _towerData)
    {
        // If we build the tower then set this tower on this base is the tower we build
        this.towerData = _towerData;

        // We just build the tower so is not upgraded
        isUpgraded = false;

        // Spawn the tower on the base
        towerGameObject = GameObject.Instantiate(_towerData.towerPrefab, transform.position, Quaternion.identity);
    }

    public void OnBase()
    {
        // If we move our mouse on the base and is not on any UI then change the base's color to red
        if (towerGameObject == null && EventSystem.current.IsPointerOverGameObject() == false)
        {
            rend.material.color = Color.red;
        }
    }
 
    public void OutBase()
    {
        // If we move our mouse away then change the base's color back to its original
        rend.material.color = originalColor;
    }

    public void UpgradeTower()
    {
        // If the tower is upgraded then dont run the next line
        if (isUpgraded)
        {
            return;
        }

        // If the tower can be upgraded then remove the old tower and spawn the lv tower on it
        Destroy(towerGameObject);
        isUpgraded = true;
        towerGameObject = GameObject.Instantiate(towerData.towerUpgradePrefab, transform.position, Quaternion.identity);
    }

    public void DestoryTower()
    {
        // If we destory this tower then remove the tower and change everying to default
        Destroy(towerGameObject);
        isUpgraded = false;
        towerGameObject = null;
        towerData = null;
    }
}
