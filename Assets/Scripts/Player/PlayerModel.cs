using UniRx;
using UnityEngine;

[CreateAssetMenu()] // Добавить раздел и название
public class PlayerModel : ScriptableObject
{
    [SerializeField] int InitialScore = 40;
    public ReactiveProperty<PlayerMode> mode = new ReactiveProperty<PlayerMode>(PlayerMode.Idle);
    // Реактивные переменные
    public float Speed; // Скорость движения вперед
    public float SideSpeed; // Боковая скорость

    public float SideRadius; // Лимит бокового перемещения

    public ReactiveProperty<PlayerStatus> Status { get; private set; } = new ReactiveProperty<PlayerStatus>(PlayerStatus.Homeless); // Статус игрока

    // Уровни для смены статуса
    public int[] statusThresholds = { 0, 30, 60, 90, 120, 150}; // Уровни очков, которые соответствуют статусам

    public ReactiveProperty<int> PlayerDirection {get; private set;} = new ReactiveProperty<int>(0);

    public ReactiveCommand<int> OnObstacleHit = new ReactiveCommand<int>();

    public void Init()
    {
        mode.Value = PlayerMode.Idle;
        // Подписка на изменение очков, чтобы автоматически обновлять статус
        ScoreManager.Instance.CurrentScore
            .Subscribe(UpdateStatus)
            .AddTo(new CompositeDisposable()); // Очищает подписки при завершении
        ScoreManager.Instance.SetScore(InitialScore);
        PlayerDirection.Value = 0;
    }

    // Обновление статуса в зависимости от очков
    private void UpdateStatus(int currentScore)
    {
        for (int i = statusThresholds.Length - 1; i >= 0; i--)
        {
            if (currentScore >= statusThresholds[i])
            {
                Status.Value = (PlayerStatus)i;
                return;
            }
        }
    }
}

// Enum для статусов игрока
public enum PlayerStatus
{
    Homeless,  // Бомж
    Poor,      // Бедный
    Middle,    // Средний
    Rich,      // Богатый
    Millionaire // Миллионер
}

public enum PlayerMode{
    Idle,
    Playing,
    Win,
    Lose
}