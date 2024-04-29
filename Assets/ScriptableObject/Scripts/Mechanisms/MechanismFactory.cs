using System;
using UnityEngine;

public class MechanismFactory
{
    public enum MechanismType
    {
        ContinuousFlames,
        PendulumFork,
        LaunchedIceCream,
        RainOfKnives,
        Grease,
        PinballSpoon,
        ExplosingFlourBags,
        MagnetsInKitchen,
        MovingShelves,
        JumpingPotatoes,
        SteamWalls
    }

    public static IMechanism CreateMechanism(MechanismDetails details, MechanismType type, Transform selfTransform, Transform playerTransform = null)
    {
        IMechanism mechanism = type switch
        {
            MechanismType.ContinuousFlames => new ContinuousFlames(),
            MechanismType.PendulumFork => new PendulumFork(),
            MechanismType.LaunchedIceCream => new LaunchedIceCream(),
            MechanismType.RainOfKnives => new RainOfKnives(),
            MechanismType.Grease => new Grease(),
            MechanismType.PinballSpoon => new PinballSpoon(),
            MechanismType.ExplosingFlourBags => new ExplosingFlourBags(),
            MechanismType.MagnetsInKitchen => new MagnetsInKitchen(),
            MechanismType.MovingShelves => new MovingShelves(),
            MechanismType.JumpingPotatoes => new JumpingPotatoes(),
            MechanismType.SteamWalls => new SteamWalls(),
            _ => throw new NotImplementedException("This mechanism type is not implemented.")
        };

        mechanism.Initialize(details, selfTransform, playerTransform);
        mechanism.MechanismStart();
        return mechanism;
    }
}
