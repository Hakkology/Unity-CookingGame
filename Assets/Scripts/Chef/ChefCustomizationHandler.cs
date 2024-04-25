using UnityEngine;

public class ChefCustomizationHandler : MonoBehaviour
{

    [SerializeField] private Renderer chefBodyRenderer; 
    [SerializeField] private GameObject hatPlaceholder; 
    [SerializeField] private GameObject facialHairPlaceholder; 


    public int CurrentTextureIndex { get; private set; }
    public int CurrentHatIndex { get; private set; }
    public int CurrentFacialHairIndex { get; private set; }

    public void ChangeChefTexture(Texture newTexture, int textureIndex)
    {
        CurrentTextureIndex = textureIndex;
        if (chefBodyRenderer != null)
        {
            chefBodyRenderer.material.mainTexture = newTexture;
        }
    }

    public void ChangeHat(GameObject newHatPrefab, int hatIndex)
    {
        CurrentHatIndex = hatIndex;
        if (hatPlaceholder.transform.childCount > 0)
        {
            foreach (Transform child in hatPlaceholder.transform)
            {
                Destroy(child.gameObject);
            }
        }

        if (newHatPrefab != null)
        {
            Instantiate(newHatPrefab, hatPlaceholder.transform);
        }
    }

    public void ChangeFacialHair(GameObject newFacialHairPrefab, int facialHairIndex)
    {
        CurrentFacialHairIndex = facialHairIndex;
        if (facialHairPlaceholder.transform.childCount > 0)
        {
            foreach (Transform child in facialHairPlaceholder.transform)
            {
                Destroy(child.gameObject);
            }
        }

        if (newFacialHairPrefab != null)
        {
            Instantiate(newFacialHairPrefab, facialHairPlaceholder.transform);
        }
    }
}
