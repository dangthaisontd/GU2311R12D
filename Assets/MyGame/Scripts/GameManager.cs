using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private int sumCoin;
    void Start()
    {
        AudioManager.Instance.BackGroundMusic();
        EventManagerGame.onHealth.AddListener(UpdateHealth);
        EventManagerGame.onCoin.AddListener(UpdateCoin);
        sumCoin = PlayerPrefs.GetInt("Coin", 10);
        Invoke("SendInfor", 0.5f);
    }

    // Update is called once per frame

    void UpdateHealth(int health)
    {
        if (health < 0)
        {
            FinishGame();
        }
    }
    void UpdateCoin(int coin)
    {
        sumCoin += coin;
        PlayerPrefs.SetInt("Coin", sumCoin);
        EventManagerGame.onSumCoin?.Invoke(sumCoin);
    }
    void SendInfor()
    {
        EventManagerGame.onSumCoin?.Invoke(sumCoin);
    }

    void FinishGame()
    {
        SceneManager.LoadScene("GameOver");
    }
    private void OnDisable()
    {
        EventManagerGame.onHealth.RemoveListener(UpdateHealth);
        EventManagerGame.onCoin.RemoveListener(UpdateCoin);
    }
}
