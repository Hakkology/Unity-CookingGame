using UnityEngine;

public class ToolBehaviour : MonoBehaviour, ICollectible
{
    public Tool toolData;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            CollectItem();
        }
    }

    public void CollectItem()
    {
        if (!LevelManager.InstructionHandler.IsToolCollected(toolData))
        {
            LevelManager.InstructionHandler.MarkToolAsCollected(toolData);
            Debug.Log(toolData.toolName + " collected!");
        }
    }
}
