using UnityEngine;

public class GateObstacle : MonoBehaviour
{
    [SerializeField] private int reward; // Изменение очков при взаимодействии

    private bool isTriggered = false; // Чтобы реагировать только один раз

    private void OnTriggerEnter(Collider other)
    {
        if (isTriggered) return; // Если уже сработал, ничего не делаем

        isTriggered = true;

        // Изменяем очки
        ScoreManager.Instance.AddScore(reward);

        // Отключаем объект, чтобы не мешался
        gameObject.SetActive(false);
    }
}
