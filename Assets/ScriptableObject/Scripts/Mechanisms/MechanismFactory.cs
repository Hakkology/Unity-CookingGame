using System;
using UnityEngine;

public class MechanismFactory
{
    public enum MechanismType
    {
        ContinuousFlames,
        PendulumFork,
        IceCreamLauncher,
        PinballSpoon,
        ExplosiveFlourBags,
        SpringJump
    }

    // if a property needs to be updated constantly, we add it to initialize function.
    // Otherwise, if its a static component, we can simply pass it in the constructor for each IMechanism.
    public static IMechanism CreateMechanism(MechanismDetails details, MechanismType type, Transform selfTransform, BallHealthBehaviour playerHealth, MechanismTimedBehaviour timedBehaviour, Rigidbody rigidbody)
    {
        IMechanism mechanism = type switch
        {
            MechanismType.ContinuousFlames => new ContinuousFlames(playerHealth, timedBehaviour, rigidbody),
            MechanismType.PendulumFork => new PendulumFork(playerHealth, timedBehaviour, rigidbody),
            MechanismType.IceCreamLauncher => new IceCreamLauncher(playerHealth, timedBehaviour, rigidbody),
            MechanismType.PinballSpoon => new PinballSpoon(timedBehaviour,rigidbody),
            MechanismType.ExplosiveFlourBags => new ExplodingFlourBags(playerHealth, timedBehaviour, rigidbody),
            MechanismType.SpringJump => new SpringJump(timedBehaviour, rigidbody),
            _ => throw new NotImplementedException("This mechanism type is not implemented.")
        };

        mechanism.InitializeMechanism(details, selfTransform);
        return mechanism;
    }
}
