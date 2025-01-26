using UnityEngine;

public class BossAttackController : MonoBehaviour
{
    public EnemySpawner enemySpawner; // R�f�rence au spawner d'ennemis
    public Animator bossAnimator; // R�f�rence � l'Animator du boss

    void Start()
    {
        if (enemySpawner == null)
        {
            Debug.LogWarning("EnemySpawner non assign� dans BossAttackController !");
        }

        if (bossAnimator == null)
        {
            Debug.LogWarning("Animator du boss non assign� !");
        }
    }

    public void Attack()
    {
        if (bossAnimator != null)
        {
            // D�clenche l'animation d'attaque
            bossAnimator.SetTrigger("Attack");
        }
    }

    // M�thode appel�e via Animation Event pour synchroniser le spawn avec l'animation
    public void SpawnEnemyDuringAttack()
    {
        if (enemySpawner != null)
        {
            enemySpawner.SpawnEnemy();
        }
        else
        {
            Debug.LogWarning("EnemySpawner non assign� !");
        }
    }
}




