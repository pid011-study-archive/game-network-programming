using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private NetworkManager _networkManager;

    // Start is called before the first frame update
    private void Start()
    {
        _networkManager = GameObject.Find("Network Manager").GetComponent<NetworkManager>();
    }

    // Update is called once per frame
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftArrow)) _networkManager.SendData(-1);

        if (Input.GetKeyDown(KeyCode.RightArrow)) _networkManager.SendData(1);
    }

    public void TranslateCat(string txt)
    {
        if (txt == "-1")
            transform.Translate(-3, 0, 0);
        else if (txt == "1")
            transform.Translate(3, 0, 0);
        else
            Debug.Log(txt);
    }
}
