using UnityEngine;

public class ArrowController : MonoBehaviour
{
    private GameObject _player;

    // Start is called before the first frame update
    private void Start()
    {
        _player = GameObject.Find("player");
    }

    // Update is called once per frame
    private void Update()
    {
        transform.Translate(0, -0.1f, 0);

        if (transform.position.y < -5.0f) Destroy(gameObject);

        var p1 = transform.position;
        var p2 = _player.transform.position;
        var dir = p1 - p2;
        var d = dir.magnitude;

        const float r1 = 0.5f;
        const float r2 = 1.0f;

        if (d < r1 + r2)
        {
            var director = GameObject.Find("Game Director");
            director.GetComponent<GameDirector>().DecreaseHp();

            Destroy(gameObject);
        }
    }
}
