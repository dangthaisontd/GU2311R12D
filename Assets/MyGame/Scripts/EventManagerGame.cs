using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class IntEvent : UnityEvent<int> { }
public class EventManagerGame : MonoBehaviour
{
    public static EventManagerGame _instance;
    public static EventManagerGame Instance
    {
        get { return _instance; }
        set { _instance = value; }
    }
    public static IntEvent onHealth;
    public static IntEvent onCoin;
    public static IntEvent onSumCoin;
    // Start is called before the first frame update
    void Start()
    {
        if (_instance == null)
        {
            _instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            DestroyImmediate(gameObject);
        }
        if (onHealth == null)
        {
            onHealth = new IntEvent();
        }
        if (onCoin == null)
        {
            onCoin = new IntEvent();
        }
        if (onSumCoin == null)
        {
            onSumCoin = new IntEvent();
        }
    }
}
