using UnityEngine;

public abstract class HungerSystem : MonoBehaviour
{
    [Header("Configuración Base")]
    [SerializeField] protected float _hungerMax = 100f;
    [SerializeField] protected float _hungerIncreaseRate = 0.5f;
    protected float _currentHunger;

    // Propiedades comunes
    public bool IsHungry => _currentHunger >= 60f;
    public float CurrentHungerNormalized => _currentHunger / _hungerMax;

    protected virtual void Update()
    {
        _currentHunger = Mathf.Min(_currentHunger + _hungerIncreaseRate * Time.deltaTime, _hungerMax);
    }

    // Método abstracto (cada hijo lo implementa a su manera)
    public abstract void SatisfyHunger(float amount);

    // Método común para debug
    protected void LogHunger(string agentName)
    {
        Debug.Log($"{agentName} - Hambre: {_currentHunger:F1}/{_hungerMax}");
    }
}