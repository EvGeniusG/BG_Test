using UnityEngine;

public class RetryButton : MonoBehaviour
{
    public void Tap(){
        GameManager.Instance.SetState(new MenuState());
    }
}
