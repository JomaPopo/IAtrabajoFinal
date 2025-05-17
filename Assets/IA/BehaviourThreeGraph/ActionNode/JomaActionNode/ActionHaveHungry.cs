using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviorDesigner.Runtime.Tasks;

[TaskCategory("MyAI/Hunger")]
public class ActionHaveHungry : ActionNode
{
    public override void OnStart()
    {
        base.OnStart();
    }

    public override TaskStatus OnUpdate()
    {
        // Verifica si el personaje tiene hambre (basado en un sistema de hambre)
        if (_IACharacterVehiculo != null && _IACharacterVehiculo.HungerSystem != null)
        {
            if (_IACharacterVehiculo.HungerSystem.IsHungry)
                return TaskStatus.Success;
        }

        return TaskStatus.Failure;
    }
}