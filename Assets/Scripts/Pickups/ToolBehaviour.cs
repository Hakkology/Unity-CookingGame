using UnityEngine;
using DG.Tweening;
using System.Collections;  // Import DOTween namespace

public class ToolBehaviour : MonoBehaviour, ICollectible
{
    public Tool toolData;
    private new Renderer renderer;
    private GameObject instantiatedTool;
    private float initialY; 

    private void Awake()
    {
        renderer = GetComponent<Renderer>();
        if (toolData != null && toolData.toolObject != null)
        {
            instantiatedTool = Instantiate(toolData.toolObject, transform.position, transform.rotation);
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
            StopAllAnimations();
            StartCoroutine(DestroyAfterFastSpin());
        }
    }

    private void StopAllAnimations()
    {
        DOTween.Kill(instantiatedTool.transform); 
    }

    private IEnumerator DestroyAfterFastSpin()
    {
        instantiatedTool.transform.DORotate(new Vector3(0, 3600, 0), 1f, RotateMode.LocalAxisAdd).SetEase(Ease.InOutQuad);
        yield return new WaitForSeconds(1f); 
        Destroy(instantiatedTool);
        Destroy(gameObject);
    }
}
