using TMPro;
using UniRx;
using UnityEngine;

public class PlayerView : MonoBehaviour
{
    [SerializeField] private PlayerModel playerModel; // Ссылка на модель игрока
    [SerializeField] private Animator animator;
    [SerializeField] private float scoreAnimationTreshold = 0.5f;
    [SerializeField] private TMP_Text scoreUpText, scoreDownText;
    [SerializeField] private GameObject[] playerSkins;

    private int _currentStatusSkin = 4;
    private int currentStatusSkin {
        get { return _currentStatusSkin; }
        set{
            _currentStatusSkin = Mathf.Clamp(value, 0, playerSkins.Length - 1);
        }
    }

    private readonly int ModeHash = Animator.StringToHash("Mode");
    private readonly int StatusHash = Animator.StringToHash("Status");
    private readonly int DirectionHash = Animator.StringToHash("Direction");
    private readonly int CollisionHash = Animator.StringToHash("Collision");
    private readonly int CollisionTriggerHash = Animator.StringToHash("CollisionTrigger");
    private readonly int MoneyUpHash = Animator.StringToHash("MoneyUp");
    private readonly int MoneyDownHash = Animator.StringToHash("MoneyDown");
    private readonly int StatusUpgradeHash = Animator.StringToHash("StatusUpgrade");

    void Start(){
        // Подписка на изменения режима игрока
        playerModel.mode
            .Subscribe(OnModeChanged)
            .AddTo(this);

        // Подписка на изменения статуса
        playerModel.Status
            .Subscribe(OnStatusChanged)
            .AddTo(this);
        
        // Подписка на изменения направления
        playerModel.PlayerDirection
            .Subscribe(OnDirectionChanged)
            .AddTo(this);

        // Подписка на команды столкновений
        playerModel.OnObstacleHit
            .Subscribe(OnObstacleCollision)
            .AddTo(this);

        // Подписка на изменения очков (для анимаций денег)
        ScoreManager.Instance.OnPositiveScoreAdded.Subscribe(
            amount => {
                OnScoreRecieved(amount);
            }
        ).AddTo(this);
        ScoreManager.Instance.OnNegativeScoreAdded.Subscribe(
            amount => {
                OnScoreLost(amount);
            }
        ).AddTo(this);
    }

    /// <summary>
    /// Изменение режима игрока
    /// </summary>
    private void OnModeChanged(PlayerMode newMode)
    {
        animator.SetInteger(ModeHash, (int)newMode);
    }

    /// <summary>
    /// Изменение статуса игрока
    /// </summary>
    private void OnStatusChanged(PlayerStatus newStatus)
    {
        // Если статус повысился, запускаем триггер "StatusUpgrade"
        if ((int)newStatus > currentStatusSkin)
        {
            animator.SetTrigger(StatusUpgradeHash);
        }
        animator.SetInteger(StatusHash, (int)newStatus);

        playerSkins[currentStatusSkin].SetActive(false);
        currentStatusSkin = (int)newStatus;
        playerSkins[currentStatusSkin].SetActive(true);

        
    }

    /// <summary>
    /// Изменение направления движения
    /// </summary>
    private void OnDirectionChanged(int direction)
    {
        animator.SetInteger(DirectionHash, direction);
    }

    /// <summary>
    /// Обработка столкновений
    /// </summary>
    private void OnObstacleCollision(int collisionIndex)
    {
        animator.SetInteger(CollisionHash, collisionIndex);
        animator.SetTrigger(CollisionTriggerHash);
    }

    private void OnScoreRecieved(int amount)
    {
        animator.SetTrigger(MoneyUpHash);
    }

    private void OnScoreLost(int amount){
        animator.SetTrigger(MoneyDownHash);
    }
}
