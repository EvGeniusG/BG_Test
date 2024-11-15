using TMPro;
using UniRx;
using UnityEngine;

public class MoneyView : MonoBehaviour
{
    [SerializeField] private TMP_Text moneyText;

    void Start()
    {
        // Подписываемся на изменения текущего количества денег
        MoneyManager.Instance.CurrentMoney
            .Subscribe(money =>
            {
                moneyText.text = $"{money}";
            })
            .AddTo(this);
    }
}
