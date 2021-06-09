using UnityEngine;
using UnityEngine.UI;

public class GameDirector : MonoBehaviour
{
    private GameObject _hpGauge;

    private void Start()
    {
        _hpGauge = GameObject.Find("HP Gauge");
    }

    public void DecreaseHp()
    {
        _hpGauge.GetComponent<Image>().fillAmount -= 0.1f;
    }
}
