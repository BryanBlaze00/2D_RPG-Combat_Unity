using System.Collections;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    private enum EnemyState
    {
        Idle, Roaming, Chase, Attack
    }

    private EnemyState currentState;
    private EnemyPathfinding enemyPathfinding;

    private void Awake() {
        enemyPathfinding = GetComponent<EnemyPathfinding>();
        currentState = EnemyState.Roaming;
    }

    private void Start() {
        StartCoroutine(RoamingRoutine());
    }

    private IEnumerator RoamingRoutine() {
        while (currentState == EnemyState.Roaming)
        {
            Vector2 roamingPosition = GetRoamingPosition();
            enemyPathfinding.MoveTo(roamingPosition);
            yield return new WaitForSeconds(2f);
        }
    }

    private Vector2 GetRoamingPosition() {
        return new Vector2(Random.Range(-10f, 10f), Random.Range(-10f, 10f)).normalized;
    }
}
