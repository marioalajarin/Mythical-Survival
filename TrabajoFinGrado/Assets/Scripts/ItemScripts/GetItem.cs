using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class GetItem : MonoBehaviour
{
    public Sprite[] item; // Array de sprites de ítems
    public GameObject[] activeItemFrames; // Marcos para ítems activos
    public GameObject[] passiveItemFrames; // Marcos para ítems pasivos
    public GameObject[] itemsToActive; // Ítems que se pueden activar
    public Text moneyText; // Texto para mostrar el dinero

    private int moneyCounter; // Contador de dinero
    private List<Sprite> itemList; // Lista de sprites de ítems
    private HashSet<int> collectedItemIndices; // Conjunto para mantener los índices de los ítems recogidos
    private PlayerController pc; // Controlador del jugador
    private PlayerRPG prpg; // RPG del jugador
    private int activeItemCount = 0; // Contador de ítems activos

    private void Start()
    {
        //Inicialización y obtención de componentes.
        itemList = new List<Sprite>(item); 
        collectedItemIndices = new HashSet<int>(); 
        pc = GetComponent<PlayerController>(); 
        prpg = GetComponent<PlayerRPG>(); 

        // Inicializar los marcos de ítem activos sin mostrar el sprite
        foreach (var frame in activeItemFrames)
        {
            frame.GetComponent<SpriteRenderer>().sprite = null;
        }

        // Si hay al menos un marco de ítem activo y el ítem con índice 0 existe
        if (activeItemFrames.Length > 0 && itemList.Count > 0)
        {
            // Asignar el ítem con índice 0 al primer marco de ítem activo
            activeItemFrames[0].GetComponent<SpriteRenderer>().sprite = itemList[0];
            collectedItemIndices.Add(0);
            activeItemCount++;
        }

        InvokeRepeating("CallRegenerateHealth", 2f, 2f); // Invocar regeneración de salud cada 2 segundos
    }

    private void OnTriggerEnter2D(Collider2D itemTrigger)
    {
        // Asegurarse de que la colisión es con un ítem y el jugador lo está recogiendo
        if (itemTrigger.gameObject.CompareTag("Item"))
        {
            if (EnemyDropGemTest.boxCounter >= 20)
            {
                moneyCounter += 20; // Incrementar el contador de dinero
                moneyText.text = moneyCounter.ToString(); // Actualizar el texto del dinero
            }
            else
            {
                if (activeItemCount < activeItemFrames.Length)
                {
                    int randomIndex;
                    do
                    {
                        randomIndex = Random.Range(1, 3); // Considerar solo los índices 1 y 2
                    } while (collectedItemIndices.Contains(randomIndex)); // Verificar si ya se ha recogido este ítem

                    collectedItemIndices.Add(randomIndex); // Agregar el ítem al conjunto de ítems recogidos

                    // Manejar los ítems específicos
                    if ((randomIndex == 1 || randomIndex == 2))
                    {
                        if (!itemsToActive[1].activeSelf)
                        {
                            GameObject currentFrame = activeItemFrames[1];
                            if (currentFrame != null)
                            {
                                currentFrame.GetComponent<SpriteRenderer>().sprite = itemList[1];
                            }
                            itemsToActive[1].SetActive(true);
                        }

                        if (!itemsToActive[2].activeSelf)
                        {
                            GameObject currentFrame = activeItemFrames[2];
                            if (currentFrame != null)
                            {
                                currentFrame.GetComponent<SpriteRenderer>().sprite = itemList[2];
                            }
                            itemsToActive[2].SetActive(true);
                        }

                        activeItemCount++;
                    }
                }
            }
            Destroy(itemTrigger.gameObject); // Destruir el ítem después de recogerlo
        }
    }

    private void CallRegenerateHealth()
    {
        if (prpg.currentHealthPoints > 0)
        {
            prpg.currentHealthPoints = prpg.currentHealthPoints - 1; // Reducir los puntos de salud
        }
    }
}
