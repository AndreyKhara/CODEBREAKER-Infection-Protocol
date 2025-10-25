using UnityEngine;

public class DummyTarget : MonoBehaviour, IHealth
{
    [SerializeField] private float maxHealth = 100f;
    [SerializeField] private float health = 100f;
    [SerializeField] private float respawnDelay = 2f;
    [SerializeField] private Renderer targetRenderer;
    [SerializeField] private Color damageColor = Color.red;
    [SerializeField] private Color normalColor = Color.white;

    private void Start()
    {
        if (targetRenderer == null)
            targetRenderer = GetComponent<Renderer>();
        health = maxHealth;
        UpdateColor(normalColor);
    }

    private void OnGUI()
    {
        // Показываем здоровье над объектом
        if (Camera.main == null)
            return;
        Vector3 screenPos = Camera.main.WorldToScreenPoint(transform.position + Vector3.up * 1.5f);
        if (screenPos.z > 0)
        {
            GUI.Label(new Rect(screenPos.x - 40, Screen.height - screenPos.y, 80, 20), $"HP: {health}/{maxHealth}");
        }
    }

    public void OnCollisionEnter(Collision collision)
    {
        Debug.Log($"DummyTarget: OnCollisionEnter с {collision.gameObject.name}");
    }

    public void OnTriggerEnter(Collider other)
    {
        Debug.Log($"DummyTarget: OnTriggerEnter с {other.gameObject.name}");
    }

    // Реализация IHealth для совместимости с Bullet
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
            Die();
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

    private void Die()
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
}
