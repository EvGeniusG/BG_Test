using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerUIControl : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    private bool isPressing = false; // Флаг активного нажатия
    private Vector2 previousMousePosition; // Предыдущая позиция мыши

    private bool isControlActivated = false;
    public void Init(){
        isControlActivated = false;
        StartCoroutine(ChangingState());
    }

    IEnumerator ChangingState(){
        yield return new WaitUntil(() => isControlActivated);
        GameManager.Instance.SetState(new GameState());
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        isControlActivated = true;
        isPressing = true;
        previousMousePosition = Input.mousePosition; // Сохраняем стартовую позицию мыши
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        isPressing = false; // Останавливаем движение
        PlayerController.MovePlayer(0);
    }

    void FixedUpdate()
    {
        // Пока активное нажатие, обновляем движение
        if (isPressing)
        {
            Vector2 currentMousePosition = Input.mousePosition;
            float deltaX = currentMousePosition.x - previousMousePosition.x; // Изменение по X
            previousMousePosition = currentMousePosition; // Обновляем предыдущую позицию
            
            PlayerController.MovePlayer(deltaX); // Передаем изменение в контроллер
        }
    }
}
