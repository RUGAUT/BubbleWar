using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public List<EnemyData> enemies; // Liste des ennemis avec leurs probabilités
    public Transform spawnArea; // Zone de spawn (un objet avec un RectTransform pour définir la zone)
    public float initialSpawnDelay = 2f; // Délai initial entre les apparitions
    public float spawnAcceleration = 0.95f; // Réduction progressive du délai
    public float minimumSpawnDelay = 0.5f; // Délai minimum entre apparitions
    public int initialEnemyCount = 1; // Nombre initial d'ennemis à spawn
    public int maxEnemyCount = 10; // Nombre maximum d'ennemis à spawn
    public float enemyCountIncreaseInterval = 10f; // Temps en secondes avant d'augmenter le nombre d'ennemis
    public Color gizmoColor = Color.green; // Couleur du Gizmo

    private float currentSpawnDelay; // Délai actuel entre apparitions
    private int currentEnemyCount; // Nombre d'ennemis actuellement spawnés à chaque itération

    void Start()
    {
        currentSpawnDelay = initialSpawnDelay;
        currentEnemyCount = initialEnemyCount;
        StartCoroutine(SpawnEnemies());
        StartCoroutine(IncreaseEnemyCountOverTime());
    }

    private IEnumerator SpawnEnemies()
    {
        while (true)
        {
            for (int i = 0; i < currentEnemyCount; i++)
            {
                SpawnEnemy();
            }

            yield return new WaitForSeconds(currentSpawnDelay);

            // Diminue progressivement le délai de spawn jusqu'au minimum autorisé
            currentSpawnDelay = Mathf.Max(currentSpawnDelay * spawnAcceleration, minimumSpawnDelay);
        }
    }

    private IEnumerator IncreaseEnemyCountOverTime()
    {
        while (currentEnemyCount < maxEnemyCount)
        {
            yield return new WaitForSeconds(enemyCountIncreaseInterval);

            // Augmente le nombre d'ennemis, sans dépasser le maximum autorisé
            currentEnemyCount++;
        }
    }

    public void SpawnEnemy()
    {
        if (spawnArea == null)
        {
            Debug.LogWarning("Zone de spawn non définie !");
            return;
        }

        if (enemies == null || enemies.Count == 0)
        {
            Debug.LogWarning("La liste des ennemis est vide !");
            return;
        }

        Vector2 randomPosition = GetRandomPositionInArea();
        GameObject selectedEnemyPrefab = SelectEnemy();

        if (selectedEnemyPrefab != null)
        {
            Instantiate(selectedEnemyPrefab, new Vector3(randomPosition.x, randomPosition.y, 0), Quaternion.identity);
        }
    }

    private GameObject SelectEnemy()
    {
        float totalChance = 0f;

        foreach (EnemyData enemy in enemies)
        {
            totalChance += enemy.spawnChance;
        }

        float randomValue = Random.Range(0f, totalChance);

        float cumulativeChance = 0f;
        foreach (EnemyData enemy in enemies)
        {
            cumulativeChance += enemy.spawnChance;
            if (randomValue <= cumulativeChance)
            {
                return enemy.enemyPrefab;
            }
        }

        return null;
    }

    private Vector2 GetRandomPositionInArea()
    {
        if (spawnArea.TryGetComponent<RectTransform>(out RectTransform rectTransform))
        {
            Vector2 size = rectTransform.rect.size;
            Vector2 position = rectTransform.position;

            Vector2 localSpawnPosition = new Vector2(
                Random.Range(-size.x / 2, size.x / 2),
                Random.Range(-size.y / 2, size.y / 2)
            );

            return position + localSpawnPosition;
        }

        Debug.LogWarning("Le spawnArea n'a pas de RectTransform !");
        return Vector2.zero;
    }

    private void OnDrawGizmos()
    {
        if (spawnArea != null && spawnArea.TryGetComponent<RectTransform>(out RectTransform rectTransform))
        {
            Gizmos.color = gizmoColor;

            Vector3 bottomLeft = spawnArea.TransformPoint(new Vector3(rectTransform.rect.xMin, rectTransform.rect.yMin, 0));
            Vector3 topLeft = spawnArea.TransformPoint(new Vector3(rectTransform.rect.xMin, rectTransform.rect.yMax, 0));
            Vector3 topRight = spawnArea.TransformPoint(new Vector3(rectTransform.rect.xMax, rectTransform.rect.yMax, 0));
            Vector3 bottomRight = spawnArea.TransformPoint(new Vector3(rectTransform.rect.xMax, rectTransform.rect.yMin, 0));

            Gizmos.DrawLine(bottomLeft, topLeft);
            Gizmos.DrawLine(topLeft, topRight);
            Gizmos.DrawLine(topRight, bottomRight);
            Gizmos.DrawLine(bottomRight, bottomLeft);
        }
    }
}









