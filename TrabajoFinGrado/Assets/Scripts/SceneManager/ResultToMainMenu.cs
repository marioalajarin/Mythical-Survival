using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ResultToMainMenu : MonoBehaviour
{
    public void RePlay()
    {
        SceneManager.LoadScene("MenuScene");
    }
}
