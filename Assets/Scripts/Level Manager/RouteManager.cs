using System.Linq;
using UnityEngine;

public class RouteManager : MonoBehaviour{

    public static RouteManager Instance { get; private set;}
    
    [SerializeField] private Transform defaultCheckpoint;

    private Transform[] Route = new Transform[0];

    int currentCheckpointIndex = 0;

    void Awake(){
        Instance = this;
    }

    public void SetRoute(Transform[] route){
        Route = route;
        currentCheckpointIndex = 0;
    }
    
    public void CheckpointIsPassed(){
        currentCheckpointIndex++;
    }

    public Transform GetCurrentCheckpoint(){
        if(currentCheckpointIndex >= Route.Length){
            return Route.LastOrDefault();
        }
        return Route[currentCheckpointIndex];
    }
    public Transform GetPreviousCheckpoint(){
        if(currentCheckpointIndex <= 0){
            return defaultCheckpoint;
        }
        return Route[currentCheckpointIndex - 1];
    }
}