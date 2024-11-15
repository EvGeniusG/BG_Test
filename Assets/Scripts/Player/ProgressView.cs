using UniRx;
using UnityEngine;

public class ProgressView : MonoBehaviour
{
    [SerializeField] private PlayerModel playerModel; // Ссылка на PlayerModel

    [System.Serializable]
    public class ProgressBar
    {
        public GameObject parent;       // Родитель шкалы (для активации/деактивации)
        public Transform progressFill; // Трансформ шкалы (для изменения localScale.x)
    }

    [SerializeField] private ProgressBar[] progressBars; // Массив шкал для каждого статуса

    void Start()
    {
        // Подписываемся на изменение статуса игрока
        playerModel.Status
            .Subscribe(UpdateProgressBarVisibility)
            .AddTo(this);

        // Подписываемся на изменение режима игрока
        playerModel.mode
            .Subscribe(OnPlayerModeChanged)
            .AddTo(this);

        // Подписываемся на изменение очков для обновления прогресса текущей шкалы
        ScoreManager.Instance.CurrentScore
            .Subscribe(UpdateProgress)
            .AddTo(this);
    }

    private void UpdateProgressBarVisibility(PlayerStatus newStatus)
    {
        // Деактивируем все шкалы
        foreach (var bar in progressBars)
        {
            bar.parent.SetActive(false);
        }

        // Активируем шкалу, соответствующую текущему статусу
        int statusIndex = (int)newStatus;
        if (statusIndex >= 0 && statusIndex < progressBars.Length)
        {
            progressBars[statusIndex].parent.SetActive(true);
        }
    }

    private void UpdateProgress(int currentScore)
    {
        PlayerStatus currentStatus = playerModel.Status.Value;
        int statusIndex = (int)currentStatus;

        if (statusIndex >= 0 && statusIndex < playerModel.statusThresholds.Length - 1)
        {
            int lowerBound = playerModel.statusThresholds[statusIndex];
            int upperBound = playerModel.statusThresholds[statusIndex + 1];

            // Вычисляем прогресс в пределах текущего статуса
            float progress = Mathf.Clamp01((float)(currentScore - lowerBound) / (upperBound - lowerBound));

            // Обновляем шкалу прогресса
            if (statusIndex < progressBars.Length)
            {
                Vector3 scale = progressBars[statusIndex].progressFill.localScale;
                scale.x = progress;
                progressBars[statusIndex].progressFill.localScale = scale;
            }
        }
    }

    private void OnPlayerModeChanged(PlayerMode mode)
    {
        // Показываем/скрываем весь ProgressView в зависимости от режима
        gameObject.SetActive(mode == PlayerMode.Playing);
    }
}
