using UnityEngine;

public class Game : MonoBehaviour {
    public static Game Ctx;
    
    public BaseHealthBar BaseHealthBar;

    private void Start() {
        Ctx = this;
        BaseHealthBar = GameObject.Find("BaseHealthBar").GetComponent<BaseHealthBar>();
    }
}