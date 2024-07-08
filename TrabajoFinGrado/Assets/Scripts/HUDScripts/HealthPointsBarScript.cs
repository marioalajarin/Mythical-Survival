using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthPointsBarScript : MonoBehaviour
{
    public Slider slider;

    public void setHealthPoints(float healthPoints)
    {
        slider.value = healthPoints;
    }
}
