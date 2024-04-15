using UnityEngine;
using System.Collections.Generic;

public class CousineHandler : MonoBehaviour
{
    public List<CousineBehaviour> allCousines;

    public void HandleCousineSelection(CousineBehaviour selectedCousine)
    {
        foreach (CousineBehaviour cousine in allCousines)
        {
            if (cousine != selectedCousine)
            {
                cousine.ResetScale();
            }
        }
        selectedCousine.EnlargeScale();
    }
}
