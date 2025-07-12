using UnityEngine;


public class BrickController : MonoBehaviour
{
    private LevelLoader _levelLoader;
    private Brick _brick;
    
    private void Start()
    {
        _brick = GetComponent<Brick>();
        string spriteFileName = $"Sprites/block_{_brick.color}";
        GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>(spriteFileName);
        _levelLoader = GameObject.Find("Main Camera").GetComponent<LevelLoader>();
    }

    // 小球与砖块的碰撞检测
    private void OnCollisionEnter2D(Collision2D other)
    {
        _brick.hitsRequired -= 1;
        if (_brick.hitsRequired == 0)
        {
            _levelLoader.brickCount--;
            Destroy(gameObject);
        }
    }
}
