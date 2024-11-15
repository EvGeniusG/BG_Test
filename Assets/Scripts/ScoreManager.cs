using UniRx;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager Instance { get; private set; }

    // Реактивное свойство очков
    public ReactiveProperty<int> CurrentScore { get; private set; } = new ReactiveProperty<int>(0);

    public ReactiveProperty<int> CurrentMultiplier { get; private set; } = new ReactiveProperty<int>(1);

    // Команды для начисления очков (положительного и отрицательного)
    public ReactiveCommand<int> OnPositiveScoreAdded { get; private set; } = new ReactiveCommand<int>();
    public ReactiveCommand<int> OnNegativeScoreAdded { get; private set; } = new ReactiveCommand<int>();

    void Awake()
    {
        Instance = this;
    }

    public void SetScore(int score){
        CurrentScore.Value = score;
    }

    // Метод для добавления очков
    public void AddScore(int score)
    {
        CurrentScore.Value += score;

        if (score > 0)
        {
            OnPositiveScoreAdded.Execute(score);
        }
        else if (score < 0)
        {
            OnNegativeScoreAdded.Execute(score);
        }
    }

    public void SetMultiplier(int multiplier){
        CurrentMultiplier.Value = multiplier;
    }

    // Метод для сброса очков
    public void ResetScore()
    {
        CurrentScore.Value = 0;
    }
}
