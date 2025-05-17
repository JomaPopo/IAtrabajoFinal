using UnityEngine;

[RequireComponent(typeof(HungerSystemLobo))]
public class IACharacterVehiculoLobo : IACharacterVehiculo
{
    [Header("Config Lobo")]
    public float velocidadPersecucion = 10f;

    public override void LoadComponent()
    {
        base.LoadComponent();
        tipoAnimal = TipoAnimal.Lobo;
    }

    public void Cazar()
    {
        if (HungerSystem != null)
        {
            ((HungerSystemLobo)HungerSystem).OnEatPrey();
        }
    }

    public override void MoveToEnemy()
    {
        if (AIEye.ViewEnemy == null) return;

        agent.speed = velocidadPersecucion;
        base.MoveToEnemy();
    }
}