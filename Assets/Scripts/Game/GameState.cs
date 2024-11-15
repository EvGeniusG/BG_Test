using UniRx;
using UnityEngine.UIElements.Experimental;

public class GameState : State
{
    CompositeDisposable disposables = new CompositeDisposable();
    public override void Enter()
    {
        UIManager.Instance.ShowGameMenu();
        PlayerController.ActivatePlayer();
        ScoreManager.Instance.CurrentScore.Subscribe( value => {
            if(value <= 0){
                GameManager.Instance.SetState(new LoseState());
            }
        }).AddTo(disposables);
    }

    public override void Exit()
    {
        disposables.Clear();
        UIManager.Instance.HideGameMenu();
    }
}