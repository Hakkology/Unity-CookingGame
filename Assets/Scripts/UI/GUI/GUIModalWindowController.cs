using System.Runtime.CompilerServices;
using UnityEngine;

public class GUIModalWindowController : MonoBehaviour {
    public void HideErrorPanel() => this.gameObject.SetActive(false);
}