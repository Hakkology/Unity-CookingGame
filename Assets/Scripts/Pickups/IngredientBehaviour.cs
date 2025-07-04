using System.Collections;
using DG.Tweening;
using UnityEngine;

public class IngredientBehaviour : MonoBehaviour, ICollectible, IQuestible
{
    public Ingredient ingredientData;
    private new Renderer renderer;
    private GameObject instantiatedIngredient;
    private float initialY; 
    private void Awake()
    {
        renderer = GetComponent<Renderer>();
        if (ingredientData != null && ingredientData.ingredientObject != null)
        {
            instantiatedIngredient = Instantiate(ingredientData.ingredientObject, transform.position, transform.rotation, transform);
            initialY = instantiatedIngredient.transform.position.y;
            if (renderer != null) {
                renderer.enabled = false; 
            }
            StartAnimations();
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            CollectItem();
        }
    }
    private void StartAnimations()
    {
        instantiatedIngredient.transform.DORotate(new Vector3(0, 360, 0), 3f, RotateMode.LocalAxisAdd).SetLoops(-1, LoopType.Restart).SetEase(Ease.Linear);
        instantiatedIngredient.transform.DOMoveY(initialY + 1f, 2f).SetLoops(-1, LoopType.Yoyo).SetEase(Ease.InOutSine);
    }
    public void CollectItem()
    {
        if (ingredientData.ingredientName == "Spice")
        {
            LevelManager.InstructionHandler.MarkIngredientAsCollected(ingredientData);
            Debug.Log("Spice collected!");
            StopAllAnimations();
            StartCoroutine(DestroyAfterFastSpin());
            return;
        }

        Instruction instruction = LevelManager.InstructionHandler.FindInstructionByIngredient(ingredientData);

        // Check if the instruction exists and if the tool is required and not collected
        if (instruction != null)
        {
            if (instruction.tool != null && !LevelManager.InstructionHandler.IsToolCollected(instruction.tool))
            {
                // Log and show message panel that the required tool needs to be collected
                Debug.Log("You need to collect the required tool first!");
                LevelManager.ModalWindowManager.ShowMessagePanel(instruction.tool);
            }
            else if (LevelManager.InstructionHandler.IsToolCollected(instruction.tool) || instruction.tool == null)
            {
                // If the tool is not required or is already collected, proceed
                if (!LevelManager.InstructionHandler.IsIngredientCollected(ingredientData))
                {
                    LevelManager.InstructionHandler.MarkIngredientAsCollected(ingredientData);
                    Debug.Log(ingredientData.ingredientName + " collected!");
                    LevelManager.SoundManager.PlaySound(ingredientData.ingredientSound);
                    UpdateQuest();
                    StopAllAnimations();
                    StartCoroutine(DestroyAfterFastSpin());
                }
            }
        }
        else
        {
            // Log and maybe show a different error if no instruction exists for the ingredient
            Debug.Log("No instructions found for this ingredient.");
        }
    }


    public void UpdateQuest()
    {
        if (ingredientData.ingredientName != "Spice")
        {
            LevelManager.InstructionHandler.UpdateQuestStatus(ingredientData);
        }
    }
    private void StopAllAnimations()
    {
        DOTween.Kill(instantiatedIngredient.transform); 
    }

    private IEnumerator DestroyAfterFastSpin()
    {
        instantiatedIngredient.transform.DORotate(new Vector3(0, 3600, 0), 1f, RotateMode.LocalAxisAdd).SetEase(Ease.InOutQuad);
        yield return new WaitForSeconds(1f);
        Instantiate(ingredientData.pickupParticleSystem, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
}
