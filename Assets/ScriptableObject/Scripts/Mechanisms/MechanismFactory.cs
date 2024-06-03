using System;
using UnityEngine;

public class MechanismFactory
{
    public enum MechanismType
    {
        None,
        ContinuousFlames,
        PendulumFork,
        IceCreamLauncher,
        PinballSpoon,
        ExplosiveFlourBags,
        SpringJump
    }

    // if a property needs to be updated constantly, we add it to initialize function.
    // Otherwise, if its a static component, we can simply pass it in the constructor for each IMechanism.
    public static IMechanism CreateMechanism(MechanismDetails details, Transform selfTransform, BallHealthBehaviour playerHealth, MechanismTimedBehaviour timedBehaviour)
    {
        IMechanism mechanism = details.MechanismType switch
        {
            MechanismType.ContinuousFlames => new ContinuousFlames(),
            MechanismType.PendulumFork => new PendulumFork(timedBehaviour),
            MechanismType.IceCreamLauncher => new IceCreamLauncher(playerHealth, timedBehaviour),
            MechanismType.PinballSpoon => new PinballSpoon(timedBehaviour),
            MechanismType.ExplosiveFlourBags => new ExplodingFlourBags(timedBehaviour),
            MechanismType.SpringJump => new SpringJump(timedBehaviour),
            _ => throw new NotImplementedException("This mechanism type is not implemented.")
        };

        mechanism.InitializeMechanism(details, selfTransform);
        return mechanism;
    }
}
