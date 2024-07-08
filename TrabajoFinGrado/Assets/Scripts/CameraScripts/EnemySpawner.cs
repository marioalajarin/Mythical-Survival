using UnityEngine;

namespace CameraScripts.EnemySpawner
{
    public class EnemySpawner : MonoBehaviour
    {
        [SerializeField] private GameObject[] someGameObject; // Array de GameObjects para enemigos
        public float spawnRate = 1f; // Frecuencia de aparición de enemigos
        private float timer; // Temporizador para controlar la aparición
        public bool canSpawn = true; // Indicador para permitir o no la aparición

        private Timer timerScript; // Referencia al script del temporizador

        void Start()
        {
            timerScript = FindObjectOfType<Timer>(); // Encontrar el script del temporizador en la escena
        }

        void Update()
        {
            if (timer < spawnRate)
            {
                timer += Time.deltaTime; // Incrementar el temporizador
            }
            else
            {
                if (canSpawn)
                {
                    SpawnRandom(); // Llamar a la función para generar enemigos
                    timer = 0; // Reiniciar el temporizador
                }
            }
        }

        public void SpawnRandom()
        {
            int elapsedMinutes = Mathf.FloorToInt(timerScript.timeCount / 60); // Calcular los minutos transcurridos
            int enemyType;

            if (elapsedMinutes < 4)
            {
                enemyType = elapsedMinutes % someGameObject.Length; // Seleccionar tipo de enemigo basado en el tiempo
            }
            else
            {
                enemyType = Random.Range(0, someGameObject.Length); // Seleccionar tipo de enemigo aleatoriamente
            }

            Vector2 dir = Random.insideUnitCircle.normalized; // Dirección aleatoria normalizada
            Vector3 position = Camera.main.ViewportToWorldPoint(new Vector3(0.8f * dir.x + 0.5f, 0.8f * dir.y + 0.5f, Camera.main.nearClipPlane)); // Calcular la posición en el mundo
            GameObject newEnemy = Instantiate(someGameObject[enemyType], position, Quaternion.identity); // Instanciar el nuevo enemigo
            AIChase aiChase = newEnemy.GetComponent<AIChase>();
            if (aiChase != null)
            {
                timerScript.AddEnemy(aiChase); // Añadir el nuevo enemigo a la lista del temporizador
            }
        }
    }
}
