using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class ContinueButton : MonoBehaviour{
    public void Tap(){
        MoneyManager.Instance.AddMoney(ScoreManager.Instance.CurrentScore.Value);
        GameManager.Instance.SetState(new MenuState());
    }
}