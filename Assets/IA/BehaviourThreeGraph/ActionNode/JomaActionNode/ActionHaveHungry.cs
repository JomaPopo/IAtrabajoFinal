using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviorDesigner.Runtime.Tasks;

[TaskCategory("MyAI/Hunger")]
public class ActionHaveHungry : ActionNode
{
    

    public override TaskStatus OnUpdate()
    {
        if (_IACharacterVehiculo == null)
            return TaskStatus.Failure;

        // Versión optimizada con caché del componente
        var hungerSystem = _IACharacterVehiculo.HungerSystem;

        if (hungerSystem != null && hungerSystem.IsHungry)
        {
            return TaskStatus.Success;
        }

        return TaskStatus.Failure;
    }
}