using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeneratorController : MonoBehaviour
{
    public GameObject enemy01;
    public GameObject enemy02;
    public GameObject enemy03;
    public float generatorTimer = 3f;
    public float generatorDelay = 0f;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    void CreateEnemy()
    {
        int generate = Random.Range(0, 2);
        int enemy = Random.Range(0, 5);


        if(generate == 0)
        {
            if (enemy == 0)
            {
                Instantiate(enemy01, transform.position, Quaternion.identity);
            }
            else if (enemy == 1 || enemy == 2)
            {
                Instantiate(enemy02, transform.position, Quaternion.identity);
            }
            else if (enemy == 3 || enemy == 4)
            {
                Instantiate(enemy03, transform.position, Quaternion.identity);
            }
        }
    }

    public void StartGenerator()
    {
        InvokeRepeating("CreateEnemy", generatorDelay, generatorTimer);
    }

    public void StopGenerator(bool clean = false)
    {
        CancelInvoke("CreateEnemy");
        if (clean)
        {
            Object[] allEnemies = GameObject.FindGameObjectsWithTag("Enemy");
            foreach (GameObject enemy in allEnemies)
            {
                Destroy(enemy);
            }
        }
    }
}
