using UnityEngine;
using BehaviorDesigner.Runtime.Tasks;

[TaskCategory("MyAI/Hunger")]
public class ActionEat : ActionNodeVehicle
{
    [Header("Configuración")]
    public float rangoAccion = 2f;

    public override TaskStatus OnUpdate()
    {
        if (_IACharacterVehiculo.health.IsDead)
            return TaskStatus.Failure;

        // Usamos el UnitGame del componente Health
        switch (_IACharacterVehiculo.health._UnitGame)
        {
            case UnitGame.Zombie: // Asumo que Zombie es para el lobo
                return HandleLobo();
            case UnitGame.Soldier: // Asumo que Soldier es para el pollo
                return HandlePollo();
            default:
                return TaskStatus.Failure;
        }
    }

    private TaskStatus HandlePollo()
    {
        // Para el pollo (Soldier), buscamos comida
        var comida = _IACharacterVehiculo.AIEye.ViewEnemy; // Asumimos que usa ViewEnemy para comida
        if (comida != null && Vector3.Distance(transform.position, comida.transform.position) <= rangoAccion)
        {
            var hungerSystem = GetComponent<HungerSystemPollo>();
            if (hungerSystem != null)
            {
                hungerSystem.OnEatFood(); // Usamos el método específico del pollo
                GameObject.Destroy(comida);
                return TaskStatus.Success;
            }
        }
        return TaskStatus.Failure;
    }

    private TaskStatus HandleLobo()
    {
        // Para el lobo (Zombie), buscamos gallinas
        var objetivo = _IACharacterVehiculo.AIEye.ViewEnemy;
        if (objetivo != null && Vector3.Distance(transform.position, objetivo.transform.position) <= rangoAccion)
        {
            var health = objetivo.GetComponent<Health>();
            if (health != null && !health.IsDead)
            {
                health.Damage(25, _IACharacterVehiculo.health); // Usamos el Damage del sistema Health

                if (health.IsDead)
                {
                    var hungerSystem = GetComponent<HungerSystemLobo>();
                    if (hungerSystem != null)
                    {
                        hungerSystem.OnEatPrey(); // Usamos el método específico del lobo
                    }
                    return TaskStatus.Success;
                }
                return TaskStatus.Running;
            }
        }
        return TaskStatus.Failure;
    }
}