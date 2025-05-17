using UnityEngine;

[RequireComponent(typeof(HungerSystemPollo))]
public class IACharacterVehiculoPollo : IACharacterVehiculo
{
    [Header("Config Pollo")]
    public float velocidadHuir = 8f;

    public override void LoadComponent()
    {
        base.LoadComponent();
        tipoAnimal = TipoAnimal.Pollo;
    }

    public void Comer()
    {
        if (HungerSystem != null)
        {
            ((HungerSystemPollo)HungerSystem).OnEatFood();
        }
    }

    public override void MoveToEvadEnemy()
    {
        if (AIEye.ViewEnemy == null) return;

        Vector3 dir = (transform.position - AIEye.ViewEnemy.transform.position).normalized;
        Vector3 newPosition = transform.position + dir * 10f;
        agent.speed = velocidadHuir;
        MoveToPosition(newPosition);
    }
}