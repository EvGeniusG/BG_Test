using UnityEngine;

public class PlayerCreator : MonoBehaviour{
    public static PlayerCreator Instance { get; private set;}

    [SerializeField] PlayerController playerPrefab;

    public PlayerController Player {get; private set;}

    void Awake(){
        Instance = this;
    }

    public void CreatePlayer(Transform spawnPoint, Transform parent = null){
        if(Player != null){
            Destroy(Player.gameObject);
        }
        Player = Instantiate(playerPrefab, spawnPoint.position, spawnPoint.rotation);
    }
}