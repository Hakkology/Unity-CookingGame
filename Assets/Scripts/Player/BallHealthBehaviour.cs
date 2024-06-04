using UnityEngine;

public class BallHealthBehaviour : MonoBehaviour
{
    public int maxHealth = 100;
    private int currentHealth;

    void Start() 
    {
        currentHealth = maxHealth;
        UIController.HUD.UpdateHealth(currentHealth);
    } 

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            TakeDamage(20);
        }
    }

    // When player takes damage.
    public void TakeDamage(int damageAmount)
    {
        currentHealth -= damageAmount;
        LevelManager.SoundManager.PlaySound(SoundEffect.Damage);
        if (currentHealth < 0) currentHealth = 0;
        UIController.HUD.UpdateHealth(currentHealth);
        if (currentHealth <= 0) Die();
    }

    // When player heals.
    public void Heal(int healAmount)
    {
        currentHealth += healAmount;
        if (currentHealth > maxHealth) currentHealth = maxHealth;
        UIController.HUD.UpdateHealth(currentHealth);
    }

    // When player dies.
    private void Die()
    {
        Debug.Log("Player has died.");
    }
}
