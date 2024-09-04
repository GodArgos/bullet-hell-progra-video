using Unity.VisualScripting;
using UnityEngine;

public class Game_Gui : MonoBehaviour
{
    private bool showGUI = false;
    private string playerSpeedText;
    private string playerLifesText;
    private string enemySpawnTimeText;
    private string rotationSpeedText; // Grados por segundo
    private string explosionSpeedText;
    private string explosionRadiusText;
    private string explosionDurationText;
    private int kills = 0;
    private bool isGameOver = false;

    private void Start()
    {
        playerSpeedText = GameManager.Instance.playerSpeed.ToString();
        playerLifesText = GameManager.Instance.playerLifes.ToString();
        enemySpawnTimeText = GameManager.Instance.spawnTime.ToString();
        rotationSpeedText = GameManager.Instance.enemyRotation.ToString();
        explosionSpeedText = GameManager.Instance.explosionSpeed.ToString();
        explosionRadiusText = GameManager.Instance.explosionRadius.ToString();
        explosionDurationText = GameManager.Instance.explosionDuration.ToString();
    }

    void Update()
    {
        // Abrir/cerrar la GUI con F
        if (Input.GetKeyDown(KeyCode.F1))
        {
            showGUI = !showGUI;
        }
    }

    void OnGUI()
    {
        if (showGUI)
        {
            // Panel de Configuración
            GUI.Box(new Rect(10, 10, 270, 200), "Configuración del Juego");

            // Columna 1
            GUI.Label(new Rect(20, 40, 130, 20), "Velocidad Player:");
            playerSpeedText = GUI.TextField(new Rect(150, 40, 70, 20), playerSpeedText);
            float.TryParse(playerSpeedText, out float playerSpeed);
            GameManager.Instance.playerSpeed = playerSpeed;

            GUI.Label(new Rect(20, 70, 130, 20), "# de Vidas:");
            playerLifesText = GUI.TextField(new Rect(150, 70, 70, 20), playerLifesText);
            int.TryParse(playerLifesText, out int playerLifes);
            GameManager.Instance.playerLifes = playerLifes;

            GUI.Label(new Rect(20, 100, 130, 20), "Tiempo de Spawneo:");
            enemySpawnTimeText = GUI.TextField(new Rect(150, 100, 70, 20), enemySpawnTimeText);
            float.TryParse(enemySpawnTimeText, out float enemySpawnTime);
            GameManager.Instance.spawnTime = enemySpawnTime;

            GUI.Label(new Rect(20, 130, 130, 20), "Velocidad Rotación:");
            rotationSpeedText = GUI.TextField(new Rect(150, 130, 70, 20), rotationSpeedText);
            float.TryParse(rotationSpeedText, out float rotationSpeed);
            GameManager.Instance.enemyRotation = rotationSpeed;

            // Columna 2
            GUI.Label(new Rect(20, 160, 130, 20), "Velocidad Explosión:");
            explosionSpeedText = GUI.TextField(new Rect(150, 160, 70, 20), explosionSpeedText);
            float.TryParse(explosionSpeedText, out float explosionSpeed);
            GameManager.Instance.explosionSpeed = explosionSpeed;

            GUI.Label(new Rect(20, 190, 130, 20), "Radio Explosión:");  
            explosionRadiusText = GUI.TextField(new Rect(150, 190, 70, 20), explosionRadiusText);
            float.TryParse(explosionRadiusText, out float explosionRadius);
            GameManager.Instance.explosionRadius = explosionRadius;

            GUI.Label(new Rect(20, 220, 130, 20), "Duración Explosión:");
            explosionDurationText = GUI.TextField(new Rect(150, 220, 70, 20), explosionDurationText);
            float.TryParse(explosionDurationText, out float explosionDuration);
            GameManager.Instance.explosionDuration = explosionDuration;
        }

        // Mostrar el número de kills en la parte superior derecha
        GUI.Label(new Rect(Screen.width - 100, 10, 90, 20), "Kills: " + kills);

        // Si el juego ha terminado, muestra la pantalla final
        if (isGameOver)
        {
            OnGameOver();
        }
    }

    void OnEnemyKilled()
    {
        // Incrementar el número de kills
        kills++;
    }

    void OnGameOver()
    {
        // Muestra la pantalla de finalización del juego
        GUI.Box(new Rect(0, 0, Screen.width, Screen.height), "");
        GUI.Label(new Rect(Screen.width / 2 - 50, Screen.height / 2 - 100, 200, 30), $"Juego Terminado");
        GUI.Label(new Rect(Screen.width / 2 - 50, Screen.height / 2 - 50, 200, 30), $"Kills: {kills}");
        GUI.Label(new Rect(Screen.width / 2 - 50, Screen.height / 2 - 20, 200, 30), $"Duración del Juego: {Time.timeSinceLevelLoad / 60f:F2} minutos");
    }
}
