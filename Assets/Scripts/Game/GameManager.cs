using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set;}


    public State currentState {get; private set;}
    
    void Awake(){
        if(Instance == null){
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else{
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        Application.targetFrameRate = 60;
        SetState(new MenuState());
    }

    public void SetState(State newState)
    {
        currentState?.Exit();
        currentState = newState;
        currentState.Enter();
    }


}
