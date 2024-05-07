using System;
using DG.Tweening;
using UnityEngine;

public class FinalIngredientBehaviour : MonoBehaviour
{
    private Tool toolData;
    private bool questCompletion = false;
    private GameObject instantiatedTool;
    private float initialY;
    private new Renderer renderer;

    private void Awake()
    {
        renderer = GetComponent<Renderer>();
        if (toolData != null && toolData.toolObject != null)
        {
            instantiatedTool = Instantiate(toolData.toolObject, transform.position, transform.rotation, transform);
            initialY = instantiatedTool.transform.position.y;
            if (renderer != null) {
                renderer.enabled = false; 
            }
            StartAnimations();
        }
    }
    private void StartAnimations()
    {
        instantiatedTool.transform.DORotate(new Vector3(0, 360, 0), 3f, RotateMode.LocalAxisAdd).SetLoops(-1, LoopType.Restart).SetEase(Ease.Linear);
        instantiatedTool.transform.DOMoveY(initialY + 1f, 2f).SetLoops(-1, LoopType.Yoyo).SetEase(Ease.InOutSine);
    }
    private void Start()
    {
        LevelManager.InstructionHandler.QuestsCompletion += HandleQuestCompletion;
    }

    private void OnDestroy()
    {
        LevelManager.InstructionHandler.QuestsCompletion -= HandleQuestCompletion;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && questCompletion)
        {
            ReachEnd();
        }
    }

    private void ReachEnd()
    {
        Debug.Log("Game End");
        LevelManager.InstructionHandler.CheckGameCompletion();
        Instantiate(toolData.pickupParticleSystem, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }

    private void HandleQuestCompletion()
    {
        questCompletion = true;
        Debug.Log("All quests complete. You can now finish the game!");
    }
}
