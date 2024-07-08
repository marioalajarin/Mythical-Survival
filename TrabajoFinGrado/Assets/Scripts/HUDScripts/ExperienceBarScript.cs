using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ExperienceBarScript : MonoBehaviour
{
    public Slider slider;

    public void SetExperience(int experience)
    {
        slider.value = experience;
    }
}
