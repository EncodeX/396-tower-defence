using UnityEngine;
using System.Collections;

public class NewMonoBehaviour : MonoBehaviour
{
    private Transform _holder;
    private Object _NormalEnemy;

    public void enemyManager(Transform holder)
    {
        _holder = holder;
        _NormalEnemy = Resources.Load("Normal_Enemy");
    }

    public void ForceSpawn(Vector2 pos, Quaternion rotation, Vector2 velocity, float deathtime)
    {
        GameObject go = (GameObject)Object.Instantiate(_NormalEnemy, pos, rotation);
        Bullet bul = go.GetComponent<Bullet>();
        bul.Initialize(velocity, deathtime);
    }
}
