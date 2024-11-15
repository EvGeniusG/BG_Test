using ButchersGames;

public class MenuState : State
{
    public override void Enter()
    {
        UIManager.Instance.ShowMainMenu();
        LevelManager.Default.SelectLevel(LevelManager.CompleteLevelCount);
        ScoreManager.Instance.ResetScore();
    }

    public override void Exit()
    {
        UIManager.Instance.HideMainMenu();
    }
}