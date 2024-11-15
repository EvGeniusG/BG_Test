using System.Collections;
using UnityEngine;

public class PlayerController : MonoBehaviour{
    private static PlayerController Instance;
    
    [SerializeField] private PlayerModel playerModel;

    [SerializeField] private Transform playerTransform;
    [SerializeField] private float obstacleStopTime = 0.3f;
    
    private Transform previousCheckpoint, currentCheckpoint;

    private float currentCheckpointProgress = 0;
    private float distanceBetweenCheckpoints = 0;

    private bool isStoppedByObstacle = false;

    

    void Awake(){
        Instance = this;
    }

    void Start(){
        SetCheckpoints();
        playerModel.Init();
    }

    void FixedUpdate()
    {
        if(Instance.playerModel.mode.Value != PlayerMode.Playing || isStoppedByObstacle) return;

        // Увеличиваем прогресс
        currentCheckpointProgress += playerModel.Speed * Time.fixedDeltaTime;

        if (currentCheckpointProgress >= distanceBetweenCheckpoints)
        {
            RouteManager.Instance.CheckpointIsPassed();
            SetCheckpoints();
        }
        SetPlayerPositionOnWay(currentCheckpointProgress / distanceBetweenCheckpoints);
    }

    void SetPlayerPositionOnWay(float progress)
    {
        transform.position = Vector3.Lerp(previousCheckpoint.position, currentCheckpoint.position, progress);
        transform.eulerAngles = new Vector3(
            0,
            Mathf.LerpAngle(previousCheckpoint.eulerAngles.y, currentCheckpoint.eulerAngles.y, progress),
            0
        );
    }


    public static void MovePlayer(float distance){
        if(Instance.playerModel.mode.Value != PlayerMode.Playing) return;
        if(distance > 0.1f || distance < -0.1f){
            Instance.playerModel.PlayerDirection.Value = (int)Mathf.Sign(distance);
        }
        else{
            Instance.playerModel.PlayerDirection.Value = 0;
        }
        

        float sidePosition = Instance.playerTransform.localPosition.x + distance * Instance.playerModel.SideSpeed;
        sidePosition = Mathf.Clamp(sidePosition, -Instance.playerModel.SideRadius, Instance.playerModel.SideRadius);
        Instance.playerTransform.localPosition = new Vector3(sidePosition, 0, 0);
    }
    



    void SetCheckpoints(){
        previousCheckpoint = RouteManager.Instance.GetPreviousCheckpoint();
        currentCheckpoint = RouteManager.Instance.GetCurrentCheckpoint();
        currentCheckpointProgress = 0;
        distanceBetweenCheckpoints = Vector3.Distance(previousCheckpoint.position, currentCheckpoint.position);
    }

    public static PlayerModel GetPlayerModel() => Instance.playerModel;

    public static void ActivatePlayer(){
        Instance.playerModel.mode.Value = PlayerMode.Playing;
    }

    public static void SetWinMode(){
        Instance.playerModel.mode.Value = PlayerMode.Win;
    }

    public static void SetLoseMode(){
        Instance.playerModel.mode.Value = PlayerMode.Lose;
    }

    public static void ObstacleImpact(int obstacleType){
        Instance.playerModel.OnObstacleHit.Execute(obstacleType);
        Instance.ObstacleStop();
    }

    private void ObstacleStop(){
        StartCoroutine(ObstacleStopRoutine());
    }

    IEnumerator ObstacleStopRoutine(){
        isStoppedByObstacle = true;
        yield return new WaitForSeconds(obstacleStopTime);
        isStoppedByObstacle = false;
    }

}