using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{

    public Transform[] m_SpawnPoints;
    public GameObject m_enemyPrefab;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame

     void OnEnable()
    {
        EnemyScript.OnEnemyKilled += SpawnNewEnemy;
    }
    public void SpawnNewEnemy()
    { 
        Instantiate(m_enemyPrefab, m_SpawnPoints[0].transform.position, Quaternion.identity);
    }
}
