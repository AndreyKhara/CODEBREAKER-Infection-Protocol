using UnityEngine;
using CDB.Character;
public class DummyTarget : MonoBehaviour, IHealth
{   
    // TO DO : _value
    [SerializeField] private float maxHealth = 100f;
    [SerializeField] private float health = 100f;
    [SerializeField] private float respawnDelay = 2f;
    [SerializeField] private Renderer targetRenderer;
    [SerializeField] private Color damageColor = Color.red;
    [SerializeField] private Color normalColor = Color.white;


    public float MaxHealth
    {
        get => maxHealth;
        set => maxHealth = value;
    } 
    public float CurrentHealth
    {
        get => health;
        set => health = value;
    }
    private void Start()
    {
        if (targetRenderer == null)
            targetRenderer = GetComponent<Renderer>();
        health = maxHealth;
        UpdateColor(normalColor);
    }

    public void OnCollisionEnter(Collision collision)
    {
        Debug.Log($"DummyTarget: OnCollisionEnter с {collision.gameObject.name}");
    }

    public void TakeDamage(float amount)
    {
        ApplyDamage(amount);
    }

    private void ApplyDamage(float amount)
    {
        health -= amount;
        Debug.Log($"DummyTarget получил урон: {amount}, осталось здоровья: {health}");
        ShowDamage();
        if (health <= 0)
        {
            Death();
        }
    }

    private void ShowDamage()
    {
        if (targetRenderer != null)
        {
            UpdateColor(damageColor);
            CancelInvoke(nameof(ResetColor));
            Invoke(nameof(ResetColor), 0.2f);
        }
    }

    private void ResetColor()
    {
        UpdateColor(normalColor);
    }

    private void UpdateColor(Color color)
    {
        if (targetRenderer != null)
        {
            targetRenderer.material.color = color;
        }
    }

    public void Death()
    {
        Debug.Log("DummyTarget уничтожен! Восстановление через " + respawnDelay + " сек.");
        if (targetRenderer != null)
            targetRenderer.enabled = false;
        Invoke(nameof(Respawn), respawnDelay);
    }

    private void Respawn()
    {
        health = maxHealth;
        if (targetRenderer != null)
        {
            targetRenderer.enabled = true;
            UpdateColor(normalColor);
        }
        Debug.Log("DummyTarget восстановлен!");
    }
    public void Heal(float amount)
    {
        
    }
}
