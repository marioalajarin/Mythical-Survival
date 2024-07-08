using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerRPG : MonoBehaviour
{
    public Text textoLvl; // Texto del nivel del jugador
    
    public int currentExperience; // Experiencia actual del jugador

    public float currentHealthPoints; // Puntos de salud actuales del jugador

    public const int maxExperience = 100; // Experiencia máxima para subir de nivel

    public const float minHealthPoints = 100; // Puntos de salud mínimos antes de morir

    public Animator animator; // Referencia al Animator del jugador

    private PlayerController pc; // Referencia al controlador del jugador

    public static int lvlCount = 0; // Contador de nivel del jugador

    public ExperienceBarScript expBar; // Barra de experiencia

    public HealthPointsBarScript healthPointsBar; // Barra de puntos de salud

    public UpgradeMenu upgradeMenu; // Menú de mejoras

    public Upgrades upgradeItemSelection; // Selección de ítems de mejora
    
    // Start is called before the first frame update
    void Start()
    {
        textoLvl.text = "Nivel 0"; // Inicializa el texto de nivel
        currentExperience = 0; // Inicializa la experiencia actual
        pc = GetComponent<PlayerController>(); // Obtiene el componente PlayerController
    }

    // Update is called once per frame
    void Update()
    {
        expBar.SetExperience(currentExperience); // Actualiza la barra de experiencia
        if (currentExperience >= maxExperience)
        {
            int expAfterReset = currentExperience - maxExperience; // Calcula la experiencia después de reiniciar
            expBar.SetExperience(expAfterReset); // Actualiza la barra de experiencia
            currentExperience = expAfterReset; // Actualiza la experiencia actual
            lvlCount = lvlCount + 1; // Incrementa el nivel
            textoLvl.text = "Nivel " + lvlCount.ToString(); // Actualiza el texto del nivel
            upgradeMenu.OpenUpgradeMenu(); // Abre el menú de mejoras
            upgradeItemSelection.selectUpgrade(); // Selecciona una mejora
        }
        
        healthPointsBar.setHealthPoints(currentHealthPoints); // Actualiza la barra de puntos de salud
        if (currentHealthPoints >= minHealthPoints)
        {
            pc.speed = 0; // Detiene al jugador
            animator.SetBool("isDead", true); // Activa la animación de muerte
            
            Invoke("LoadSceneDelay", 3f); // Carga la escena de derrota después de un retraso
        }
    }

    void LoadSceneDelay()
    {
        SceneManager.LoadScene("LoseScene"); // Carga la escena de derrota
    }
}
