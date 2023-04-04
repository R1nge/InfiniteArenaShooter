using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private float maxHealth;

    public float GetSpeed() => speed;

    public void SetSpeed(float value)
    {
        speed = value;
        Save();
    }

    public float GetMaxHealth() => maxHealth;

    public void SetMaxHealth(float value)
    {
        maxHealth = value;
        Save();
    } 

    private void Awake() => Load();

    private void Load()
    {
        var savedSpeed = PlayerPrefs.GetFloat("Speed");
        if (savedSpeed != 0)
        {
            speed = savedSpeed;
        }

        var savedHealth = PlayerPrefs.GetFloat("MaxHealth");
        if (savedHealth != 0)
        {
            maxHealth = savedHealth;
        }
    }

    private void Save()
    {
        PlayerPrefs.SetFloat("Speed", speed);
        PlayerPrefs.SetFloat("MaxHealth", maxHealth);
        PlayerPrefs.Save();
    }

    private void OnApplicationQuit() => Save();
}