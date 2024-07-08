using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class Upgrades : MonoBehaviour
{
    public GetItem upgradeableItems;
    public UpgradeMenu upgradeMenu;
    private bool isItemSelected;

    public Sprite[] itemsSprites;
    public string[] itemDescriptions;

    public PolyphemusScript polyphemusScript;
    public RaSunScript raSunScript;

    public GameObject[] itemsToIncrease;

    public AnubisScript anubisScript;
    public ThunderScript thunderScript;
    public RaDamageScript[] raDamageScripts;

    public Text[] itemUpgradesNumber;

    private int contAnubis = 0;
    private int contRa = 0;
    private int contThunder = 0;
    private int contBoots = 0;
    private int contPoly = 0;
    private int contSun = 0;

    public Text[] itemDescriptionText;

    private AudioSource audioSource;
    public AudioClip upgradeSound;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();

        // Descripciones de los ítems en español
        itemDescriptions = new string[]
        {
            "Aumenta el daño de Anubis en 1 punto",
            "Aumenta el daño de Ra en 1 punto",
            "Aumenta el daño del impacto en 1 punto y del daño residual en 0.2 puntos",
            "Aumenta la velocidad en un 5%",
            "Aumenta el área de los objetos en un 5%",
            "Aumenta la regeneración de vida en un 5%"
        };
    }

    // Selecciona las mejoras aleatoriamente
    public void selectUpgrade()
    {
        HashSet<int> selectedIndices = new HashSet<int>();

        while (selectedIndices.Count < 3)
        {
            int randomIndex = Random.Range(0, itemsSprites.Length);
            selectedIndices.Add(randomIndex);
        }

        List<int> indicesList = new List<int>(selectedIndices);

        // Asigna ítems y sus efectos a los botones de mejora
        for (int i = 0; i < 3; i++)
        {
            int spriteIndex = indicesList[i];
            Sprite randomSprite = itemsSprites[spriteIndex];
            upgradeMenu.upgradeButtons[i].image.sprite = randomSprite;
            itemDescriptionText[i].text = itemDescriptions[spriteIndex];

            upgradeMenu.upgradeButtons[i].onClick.RemoveAllListeners();
            if (randomSprite == itemsSprites[0])
            {
                // Mejora para Anubis
                upgradeMenu.upgradeButtons[i].onClick.AddListener(() =>
                {
                    anubisScript.damage += 1f;
                    contAnubis += 1;
                    itemUpgradesNumber[0].text = contAnubis.ToString();
                    PlayUpgradeSound();
                    upgradeMenu.CloseUpgradeMenu();
                });
            }
            else if (randomSprite == itemsSprites[1])
            {
                // Mejora para Ra
                upgradeMenu.upgradeButtons[i].onClick.AddListener(() =>
                {
                    foreach (var raDamageScript in raDamageScripts)
                    {
                        raDamageScript.damage += 1f;
                    }

                    contRa += 1;
                    itemUpgradesNumber[1].text = contRa.ToString();
                    PlayUpgradeSound();
                    upgradeMenu.CloseUpgradeMenu();
                });
            }
            else if (randomSprite == itemsSprites[2])
            {
                // Mejora para Thunder
                upgradeMenu.upgradeButtons[i].onClick.AddListener(() =>
                {
                    thunderScript.initialDamageAmount += 1f;
                    thunderScript.areaDamageAmount += 0.2f;
                    contThunder += 1;
                    itemUpgradesNumber[2].text = contThunder.ToString();
                    PlayUpgradeSound();
                    upgradeMenu.CloseUpgradeMenu();
                });
            }
            else if (randomSprite == itemsSprites[3])
            {
                // Mejora para las Botas
                upgradeMenu.upgradeButtons[i].onClick.AddListener(() =>
                {
                    upgradeableItems.GetComponent<BootsScript>().increaseSpeed();
                    contBoots += 1;
                    itemUpgradesNumber[3].text = contBoots.ToString();
                    PlayUpgradeSound();
                    upgradeMenu.CloseUpgradeMenu();
                });
            }
            else if (randomSprite == itemsSprites[4])
            {
                // Mejora para Polyphemus
                upgradeMenu.upgradeButtons[i].onClick.AddListener(() =>
                {
                    polyphemusScript.IncreaseItemScale(itemsToIncrease);
                    contPoly += 1;
                    itemUpgradesNumber[4].text = contPoly.ToString();
                    PlayUpgradeSound();
                    upgradeMenu.CloseUpgradeMenu();
                });
            }
            else if (randomSprite == itemsSprites[5])
            {
                // Mejora para Ra Sun
                upgradeMenu.upgradeButtons[i].onClick.AddListener(() =>
                {
                    raSunScript.regeneration *= 1.05f;
                    contSun += 1;
                    itemUpgradesNumber[5].text = contSun.ToString();
                    PlayUpgradeSound();
                    upgradeMenu.CloseUpgradeMenu();
                });
            }
        }
    }

    // Reproduce el sonido de mejora
    void PlayUpgradeSound()
    {
        audioSource.PlayOneShot(upgradeSound);
    }
}
