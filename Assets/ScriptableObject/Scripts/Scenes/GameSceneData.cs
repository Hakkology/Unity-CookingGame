using UnityEngine;

public enum Kitchen{
    Turkish,
    Greek,
    Chinese,
    French,
    Indian,
    Italian,
    Mexican,
    Default
}

[CreateAssetMenu(fileName = "Game Scene Data", menuName = "Game Scene Data", order = 1)]
public class GameSceneData : SceneData
{
    public Instruction[] instructions; 
    public Vector3 playerSpawnPosition;
    public float playerRespawnYPosition;
    public Kitchen kitchenType;
}