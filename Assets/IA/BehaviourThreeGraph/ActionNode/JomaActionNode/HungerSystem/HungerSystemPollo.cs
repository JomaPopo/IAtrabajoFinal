using UnityEngine;

public class HungerSystemPollo : HungerSystem
{
    [Header("Config Pollo")]
    [SerializeField] private float _hungerDecreaseOnEat = 40f;

    public override void SatisfyHunger(float amount)
    {
        _currentHunger = Mathf.Max(_currentHunger - _hungerDecreaseOnEat, 0f);
        LogHunger("Pollo");
    }

    // OnTriggerEnter llamará a este método al tocar comida
    public void OnEatFood()
    {
        SatisfyHunger(_hungerDecreaseOnEat);
    }
}