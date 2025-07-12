using UnityEngine;


public class BrickController : MonoBehaviour
{
    [Header("道具预制体")]
    public GameObject upgradePrefab;
    [Header("生成道具的概率")]
    [Range(0f, 1.0f)]
    public float upgradeProbability = 0.8f;
    
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
            
            if (Random.value < upgradeProbability) // 道具生成概率
            {
                // 在小球位置生成道具
                Instantiate(upgradePrefab, new Vector3(other.transform.position.x, other.transform.position.y, 0),
                    Quaternion.identity);
            }
        }
    }
}
