using UnityEngine;

public class IACharacterActionLobo : IACharacterActions
{
    [Header("Ref Lobo")]
    public HungerSystemLobo hungerSystemLobo;

    private void Awake()
    {
        hungerSystemLobo = GetComponent<HungerSystemLobo>();
    }

    public void AtacarAccion(Health saludPollo)
    {
        if (saludPollo != null && !saludPollo.IsDead)
        {
            saludPollo.Damage(25, GetComponent<Health>());

            if (saludPollo.IsDead && hungerSystemLobo != null)
            {
                hungerSystemLobo.OnEatPrey();
            }
        }
    }

    public void Aullido()
    {
        // Lógica para aullido
        Debug.Log("Auuuuu!");
    }
}