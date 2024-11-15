using UnityEngine;

namespace ButchersGames
{
    public class Level : MonoBehaviour
    {
        [SerializeField] private Transform playerSpawnPoint;

        public Transform PlayerSpawnPoint => playerSpawnPoint;

        [SerializeField] private Transform[] checkPoints;

        public Transform[] CheckPoints => checkPoints;


#if UNITY_EDITOR
        private void OnDrawGizmos()
        {
            if (playerSpawnPoint != null)
            {
                Gizmos.color = Color.magenta;
                var m = Gizmos.matrix;
                Gizmos.matrix = playerSpawnPoint.localToWorldMatrix;
                Gizmos.DrawSphere(Vector3.up * 0.5f + Vector3.forward, 0.5f);
                Gizmos.DrawCube(Vector3.up * 0.5f, Vector3.one);
                Gizmos.matrix = m;
            }
        }
#endif


        void Start(){
            PlayerCreator.Instance.CreatePlayer(playerSpawnPoint);
            RouteManager.Instance.SetRoute(checkPoints);
        }


    }
}