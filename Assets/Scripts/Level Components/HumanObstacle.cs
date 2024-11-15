using UnityEngine;

public class HumanObstacle : MonoBehaviour
{
    [SerializeField] private int obstacleType; // Тип препятствия
    [SerializeField] private int reward;       // Награда/штраф за столкновение
    private bool hasTriggered = false;         // Флаг, чтобы избежать повторного взаимодействия

    [SerializeField] private Animator animator;
    [SerializeField] private Collider obstacleCollider;

    private void OnTriggerEnter(Collider other)
    {
        if (hasTriggered) return; // Если уже сработал, игнорируем
        hasTriggered = true;      // Устанавливаем флаг, чтобы больше не реагировать

        // Вызываем метод обработки столкновения
        PlayerController.ObstacleImpact(obstacleType);
        ScoreManager.Instance.AddScore(reward);

        // Воспроизводим анимацию (если аниматор есть)
        if (animator != null)
        {
            animator.SetTrigger("Impact"); // Триггер для анимации столкновения
        }

        // Отключаем коллайдер, чтобы больше не взаимодействовать
        obstacleCollider.enabled = false;
    }
}
