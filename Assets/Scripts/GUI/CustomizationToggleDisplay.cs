using UnityEngine;
using UnityEngine.UI;

public class CustomizationToggleDisplay : MonoBehaviour
{
    public GameObject[] gameObjects;

    void Start()
    {
        Toggle[] toggles = GetComponentsInChildren<Toggle>();
        for (int i = 0; i < toggles.Length; i++)
        {
            int index = i;
            toggles[i].onValueChanged.AddListener((isOn) => {
                if (isOn) ActivateGameObject(index);
            });
        }
    }

    void ActivateGameObject(int index)
    {
        for (int i = 0; i < gameObjects.Length; i++)
        {
            gameObjects[i].SetActive(i == index);
        }
    }
}
