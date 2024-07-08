using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PolyphemusScript : MonoBehaviour
{
    // Aumento del escalamiento
    private const float ScaleIncreaseFactor = 1.05f;

    public void IncreaseItemScale(GameObject[] itemsToIncrease)
    {
        foreach (GameObject item in itemsToIncrease)
        {
            Transform itemTransform = item.transform;

            Vector3 scale = itemTransform.localScale;
            scale *= ScaleIncreaseFactor;

            itemTransform.localScale = scale;
        }
    }
}
