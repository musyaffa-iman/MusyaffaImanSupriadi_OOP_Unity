using UnityEngine;
using UnityEngine.UIElements;

public class Scores : MonoBehaviour
{
    private Label healthLabel;
    private Label pointsLabel;
    private Label waveLabel;
    private Label enemiesLeftLabel;

    private HealthComponent playerHealthComponent;
    private CombatManager combatManager;

    private int points;

    private void OnEnable()
    {
        VisualElement root = GetComponent<UIDocument>().rootVisualElement;

        healthLabel = root.Q<Label>("Health");
        pointsLabel = root.Q<Label>("Points");
        waveLabel = root.Q<Label>("Wave");
        enemiesLeftLabel = root.Q<Label>("EnemiesLeft");
    }

    private void Start()
    {
        GameObject player = GameObject.FindWithTag("Player");
        if (player != null)
        {
            playerHealthComponent = player.GetComponent<HealthComponent>();
        }

        combatManager = FindObjectOfType<CombatManager>();
        UpdateUI();
    }

    private void Update()
    {
        UpdateUI();
    }

    private void UpdateUI()
    {
        if (playerHealthComponent != null)
        {
            healthLabel.text = "Health: " + playerHealthComponent.GetHealth();
        }

        pointsLabel.text = "Points: " + combatManager.GetPoints();

        if (combatManager != null)
        {
            waveLabel.text = "Wave: " + combatManager.GetCurrentWave();
        }

        enemiesLeftLabel.text = "Enemies Left: " + combatManager.GetTotalEnemies();
    }
}
