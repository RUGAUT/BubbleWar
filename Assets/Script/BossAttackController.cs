using UnityEngine;

public class BossAttackController : MonoBehaviour
{
    public EnemySpawner enemySpawner; // Référence au spawner d'ennemis
    public Animator bossAnimator; // Référence à l'Animator du boss

    void Start()
    {
        if (enemySpawner == null)
        {
            Debug.LogWarning("EnemySpawner non assigné dans BossAttackController !");
        }

        if (bossAnimator == null)
        {
            Debug.LogWarning("Animator du boss non assigné !");
        }
    }

    public void Attack()
    {
        if (bossAnimator != null)
        {
            // Déclenche l'animation d'attaque
            bossAnimator.SetTrigger("Attack");
        }
    }

    // Méthode appelée via Animation Event pour synchroniser le spawn avec l'animation
    public void SpawnEnemyDuringAttack()
    {
        if (enemySpawner != null)
        {
            enemySpawner.SpawnEnemy();
        }
        else
        {
            Debug.LogWarning("EnemySpawner non assigné !");
        }
    }
}




