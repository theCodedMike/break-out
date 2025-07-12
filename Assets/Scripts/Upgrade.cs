using UnityEngine;
using Random = UnityEngine.Random;


public class Upgrade : MonoBehaviour
{
    public Sprite[] sprites;
    [HideInInspector]
    public string upgradeName;

    private Ball _ball;

    private Vector3 _pos;
    
    
    private void Start()
    {
        Sprite icon = sprites[Random.Range(0, sprites.Length)];
        upgradeName = icon.name;
        GetComponent<SpriteRenderer>().sprite = icon;
        _ball = GameObject.Find("ball").GetComponent<Ball>();
    }

    private void Update()
    {
        //transform.position = new Vector3(transform.position.x, transform.position.y - 0.05f, 0);
        _pos = transform.position;
        _pos.y -= 0.05f;
        transform.position = _pos;
        
        if (transform.position.y <= _ball.endY)
            Destroy(gameObject);
    }
    
}
