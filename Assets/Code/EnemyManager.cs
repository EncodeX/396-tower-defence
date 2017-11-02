using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;
using Object = UnityEngine.Object;



public class EnemyManager : MonoBehaviour
{

    private static Object _NormalEnemy;
    //public static enemyManager eManager;
    private const float SpawnTime = 3f;
    private const int MaxEnemyCount = 8;
    private float _lastspawn;
    private Transform _holder;
    public static Vector3 NormalEnemySpeed = new Vector3(-0.5f, 0f, -0.5f);
    public static float NormalEnemyHealthpoints = 100f;

    //public EnemyManager(Transform holder)
    //{
    //	_NormalEnemy = Resources.Load("Normal_Enemy");
    //    _holder = holder;
    //}

    internal void Start()
    {
        _NormalEnemy = Resources.Load("Normal_Enemy");
        _holder = transform;
    }

    internal void Update()
    {
        if ((Time.time - _lastspawn) < SpawnTime) return;
        _lastspawn = Time.time;
        Spawn();
    }

    private void Spawn()
    {
        if (_holder.childCount >= MaxEnemyCount) { return; }

        Vector3 pos = new Vector3(2f, 0.3f, 2f);
        Quaternion rotation = new Quaternion(0f, 0f, 0f, 0f);
        //Quaternion rotation = transform.rotation;
        ForceSpawn(pos, rotation, NormalEnemySpeed, "NormalEnemy", NormalEnemyHealthpoints);
    }

    public void ForceSpawn(Vector3 pos, Quaternion rotation, Vector3 speed, string type, float healthpoints)
    {
        //Debug.Log(pos);
        NormalEnemy enemy;
        GameObject GameObj;
        GameObj = (GameObject)Object.Instantiate(_NormalEnemy, pos, rotation);
        enemy = GameObj.GetComponent<NormalEnemy>();
        enemy.Initialize(speed, type, healthpoints);
    }



}




