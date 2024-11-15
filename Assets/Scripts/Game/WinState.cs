using ButchersGames;

public class WinState : State
{
    int scoreMultiplier = 1;
    public WinState(int scoreMultiplier = 1){
        this.scoreMultiplier = scoreMultiplier;
    }

    public override void Enter()
    {
        ScoreManager.Instance.SetMultiplier(scoreMultiplier);
        LevelManager.CompleteLevelCount++;
        UIManager.Instance.ShowWinMenu();
        PlayerController.SetWinMode();
    }

    public override void Exit()
    {
        UIManager.Instance.HideWinMenu();
    }
}