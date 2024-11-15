using UnityEngine;

public class FinishDoors : MonoBehaviour
{
    [SerializeField] private PlayerStatus requiredPlayerStatus = PlayerStatus.Middle; // Уровень, необходимый для открытия дверей
    [SerializeField] private Animator doorAnimator; // Аниматор дверей
    [SerializeField] private int scoreMultiplier = 1; // Множитель очков

    // Событие при вхождении в триггер
    private void OnTriggerEnter(Collider other){
        PlayerStatus playerStatus = PlayerController.GetPlayerModel().Status.Value;

        // Сравниваем статус игрока с требуемым
        if (playerStatus >= requiredPlayerStatus)
        {
            OpenDoors();
        }
        else
        {
            TriggerWinState();
        }
    }

    // Метод для открытия дверей с анимацией
    private void OpenDoors(){
        if (doorAnimator != null)
        {
            doorAnimator.SetTrigger("Open"); // Здесь предполагается, что в аниматоре есть триггер "Open"
        }
    }

    // Метод для активации состояния выигрыша
    private void TriggerWinState(){
        GameManager.Instance.SetState(new WinState(scoreMultiplier));
    }
}
