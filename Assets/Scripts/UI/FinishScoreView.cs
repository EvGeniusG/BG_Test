using TMPro;
using UniRx;
using UnityEngine;

public class FinishScoreView : MonoBehaviour
{
    [SerializeField] private TMP_Text finalScoreText; // TMP_Text для отображения итогового счета

    void Start()
    {
        // Подписываемся на изменения CurrentScore и CurrentMultiplier
        Observable.CombineLatest(
                ScoreManager.Instance.CurrentScore,
                ScoreManager.Instance.CurrentMultiplier,
                (score, multiplier) => score * multiplier // Рассчитываем итоговый счет
            )
            .Subscribe(finalScore =>
            {
                finalScoreText.text = $"{finalScore}"; // Обновляем текст
            })
            .AddTo(this); // Привязываем подписку к жизненному циклу объекта
    }
}
