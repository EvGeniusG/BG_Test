using UniRx;
using UnityEngine;

public class MoneyManager : MonoBehaviour
{
    public static MoneyManager Instance { get; private set; }

    // Реактивная переменная денег
    public ReactiveProperty<int> CurrentMoney { get; private set; } = new ReactiveProperty<int>(0);

    private const string MoneyKey = "PlayerMoney"; // Ключ для сохранения в PlayerPrefs

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject); // Сохраняем объект между сценами

        // Загружаем деньги из PlayerPrefs
        LoadMoney();
    }

    /// <summary>
    /// Добавляет указанную сумму денег.
    /// </summary>
    /// <param name="amount">Сумма для добавления</param>
    public void AddMoney(int amount)
    {
        if (amount < 0)
        {
            Debug.LogWarning("Попытка добавить отрицательную сумму денег!");
            return;
        }

        CurrentMoney.Value += amount;

        // Сохраняем изменения в PlayerPrefs
        SaveMoney();
    }

    /// <summary>
    /// Пытается потратить указанную сумму денег.
    /// </summary>
    /// <param name="amount">Сумма для траты</param>
    /// <returns>True, если деньги успешно потрачены, иначе False</returns>
    public bool SpendMoney(int amount)
    {
        if (amount <= 0)
        {
            Debug.LogWarning("Попытка потратить некорректную сумму денег!");
            return false;
        }

        if (CurrentMoney.Value >= amount)
        {
            CurrentMoney.Value -= amount;

            // Сохраняем изменения в PlayerPrefs
            SaveMoney();

            return true;
        }

        Debug.LogWarning("Недостаточно денег для траты!");
        return false;
    }

    /// <summary>
    /// Загружает деньги из PlayerPrefs.
    /// </summary>
    private void LoadMoney()
    {
        if (PlayerPrefs.HasKey(MoneyKey))
        {
            CurrentMoney.Value = PlayerPrefs.GetInt(MoneyKey);
        }
        else
        {
            CurrentMoney.Value = 0; // Если денег нет в PlayerPrefs, начинаем с 0
        }
    }

    /// <summary>
    /// Сохраняет деньги в PlayerPrefs.
    /// </summary>
    private void SaveMoney()
    {
        PlayerPrefs.SetInt(MoneyKey, CurrentMoney.Value);
        PlayerPrefs.Save();
    }
}
