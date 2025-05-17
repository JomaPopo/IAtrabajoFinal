using UnityEngine;

public class HungerSystemLobo : HungerSystem
{
    [Header("Config Lobo")]
    [SerializeField] private float _hungerDecreaseOnEat = 70f;
    public bool IsStarving => _currentHunger >= 90f;

    public override void SatisfyHunger(float amount)
    {
        _currentHunger = Mathf.Max(_currentHunger - _hungerDecreaseOnEat, 0f);
        LogHunger("Lobo");
    }

    // Llamado desde ActionWolfEat al matar al pollo
    public void OnEatPrey()
    {
        SatisfyHunger(_hungerDecreaseOnEat);
    }
}