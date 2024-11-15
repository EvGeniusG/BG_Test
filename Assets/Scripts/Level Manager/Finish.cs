using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Finish : MonoBehaviour
{
    [SerializeField] private int scoreMultiplier = 1; // Множитель очков
    [SerializeField] BoxCollider finishCollider;
#if UNITY_EDITOR
    private void OnDrawGizmos()
    {
        if(finishCollider == null) return;
        Gizmos.color = Color.green;
        Gizmos.DrawCube(transform.position + finishCollider.center, finishCollider.size);
    }
#endif


    private void OnTriggerEnter(Collider other){
        TriggerWinState();
    }

    private void TriggerWinState(){
        GameManager.Instance.SetState(new WinState(scoreMultiplier));
    }
}
