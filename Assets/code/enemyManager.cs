using UnityEngine;
using System.Collections;

public class enemyManager
{
    private Transform _holder;
    private Object _NormalEnemy = Resources.Load("Normal_Enemy");
    float game_time;

    void Start()
    {
        game_time = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        game_time += 1.0f;
        Vector3 starting_point = new Vector3(1.0f, 0.3f, 1.0f);
        if((game_time % 30.0f) == 0)
        {
            SpawnEnemy(starting_point);
        }
    }

    public void SpawnEnemy(Vector3 pos)
    {
        Quaternion rotation = new Quaternion();
        GameObject go = (GameObject)Object.Instantiate(_NormalEnemy, pos, rotation);
    }
}
