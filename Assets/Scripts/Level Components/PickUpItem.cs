using UnityEngine;

public class PickUpItem : MonoBehaviour{

    [SerializeField] int Amount;

    void OnTriggerEnter(Collider other){
        ScoreManager.Instance.AddScore(Amount);
        gameObject.SetActive(false);
    }
}