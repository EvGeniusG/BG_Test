using TMPro;
using UniRx;
using UnityEngine;

public class ScoreView : MonoBehaviour
{
    [SerializeField] private TMP_Text scoreText; // TMP_Text для отображения очков

    void Start()
    {
        // Подписываемся на изменения CurrentScore
        ScoreManager.Instance.CurrentScore
            .Subscribe(score =>
            {
                scoreText.text = $"{score}"; // Обновляем текст
            })
            .AddTo(this); // Добавляем подписку к жизненному циклу объекта
    }
}
