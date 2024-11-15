using ButchersGames;
using TMPro;
using UnityEngine;

public class LevelNumberView : MonoBehaviour{
    [SerializeField] private TMP_Text levelText;
    [SerializeField] private bool previousNumber = false;

    void OnEnable(){

        int number = previousNumber ? LevelManager.CompleteLevelCount : LevelManager.CompleteLevelCount + 1;
        levelText.text = "Уровень " + number;
    }
}