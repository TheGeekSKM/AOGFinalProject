using UnityEngine;

public class EnemyBrain : MonoBehaviour
{
    [SerializeField] int _enemyIndex = -1;
    public int EnemyIndex => _enemyIndex;

    public void SetEnemyIndex(int index)
    {
        _enemyIndex = index;
    }
}