using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    [SerializeField] private int speed;
    [SerializeField] private int maxHealth;

    public int GetSpeed() => speed;

    public int GetMaxHealth() => maxHealth;

    private void Awake() => Load();

    private void Load()
    {
        var savedSpeed = PlayerPrefs.GetInt("Speed");
        if (savedSpeed != 0)
        {
            speed = savedSpeed;
        }

        var savedHealth = PlayerPrefs.GetInt("MaxHealth");
        if (savedHealth != 0)
        {
            maxHealth = savedHealth;
        }
    }

    private void Save()
    {
        PlayerPrefs.SetInt("Speed", speed);
        PlayerPrefs.SetInt("MaxHealth", maxHealth);
        PlayerPrefs.Save();
    }

    private void OnApplicationQuit() => Save();
}