using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class UpgradeMenu : MonoBehaviour
{
    public GameObject upgradeMenu;

    public List<Button> upgradeButtons;
    void Start()
    {
        Time.timeScale = 1;
        upgradeMenu.SetActive(false);
    }

    public void OpenUpgradeMenu()
    {
        Time.timeScale = 0f;
        upgradeMenu.SetActive(true);
    }
    
    public void CloseUpgradeMenu()
    {
        Time.timeScale = 1f;
        upgradeMenu.SetActive(false);
    }
    
}
