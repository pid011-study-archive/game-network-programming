using UnityEngine;

public class ArrowGenerator : MonoBehaviour
{
    public GameObject arrowPrefab;

    private float _spawn = 1.0f;
    private float _delta;

    private void Update()
    {
        _delta += Time.deltaTime;
        if (_delta > _spawn)
        {
            _delta = 0f;
            var go = Instantiate(arrowPrefab);
            var posX = Random.Range(-6, 7);
            go.transform.position = new Vector3(posX, 7, 0);
        }
    }
}
