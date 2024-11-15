public class LoseState : State
{
    public override void Enter()
    {
        UIManager.Instance.ShowLoseMenu();
        PlayerController.SetLoseMode();
    }

    public override void Exit()
    {
        UIManager.Instance.HideLoseMenu();
    }
}