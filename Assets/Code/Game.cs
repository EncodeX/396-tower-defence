using UnityEngine;

public class Game : MonoBehaviour {
    public static Game Ctx;
    
    public BaseHealthBar BaseHealthBar;
    public static EnemyManager enemyManager;

    private void Start() {
        Ctx = this;
        BaseHealthBar = GameObject.Find("BaseHealthBar").GetComponent<BaseHealthBar>();
        enemyManager = GameObject.Find("Spawner").GetComponent<EnemyManager>();
    }
}