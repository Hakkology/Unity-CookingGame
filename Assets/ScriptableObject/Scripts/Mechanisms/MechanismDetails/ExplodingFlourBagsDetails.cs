using UnityEngine;

[CreateAssetMenu(fileName = "ExplodingFlourBagsDetails", menuName = "Mechanisms/ExplodingFlourBagsDetails", order = 3)]
public class ExplodingFlourBagsDetails : MechanismDetails
{
    [Tooltip("Force applied to the player when triggering the exploding bags.")]
    public float pushForce = 5.0f; 
}
