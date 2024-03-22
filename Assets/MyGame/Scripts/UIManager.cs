using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] TextMeshProUGUI _textHealth;
    [SerializeField] TextMeshProUGUI _textCoin;
     public Slider _healthSlider;
    void Start()
    {
        EventManagerGame.onHealth.AddListener(UpdateHealth);
        EventManagerGame.onSumCoin.AddListener(UpdateCoin);
    }
    void UpdateHealth(int health)
    {
        _textHealth.text = ("Health :" + health.ToString());
        _healthSlider.value = health;
    }
    void UpdateCoin(int coin)
    {
        _textCoin.text = ("Coin :" + coin.ToString());
    }
    private void OnDisable()
    {
        EventManagerGame.onHealth.RemoveListener(UpdateHealth);
        EventManagerGame.onSumCoin.RemoveListener(UpdateCoin);
    }
}
