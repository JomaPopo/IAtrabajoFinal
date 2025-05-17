using UnityEngine;

public class IACharacterActionPollo : IACharacterActions
{
    [Header("Ref Pollo")]
    public HungerSystemPollo hungerSystemPollo;

    private void Awake()
    {
        hungerSystemPollo = GetComponent<HungerSystemPollo>();
    }

    public void ComerAccion(GameObject comida)
    {
        if (hungerSystemPollo != null)
        {
            hungerSystemPollo.OnEatFood();
            Destroy(comida);
        }
    }

    public void SonidoAlerta()
    {
        // L�gica para sonido de alerta
        Debug.Log("Pollo en alerta!");
    }
}