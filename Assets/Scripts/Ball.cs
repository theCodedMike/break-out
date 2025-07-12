using UnityEngine;
using UnityEngine.SceneManagement;


public class Ball : MonoBehaviour
{
    [Header("小球移动速度")]
    public float speed = 15;
    [Header("游戏结束的下降距离")]
    public float endY = -8;

    private int _num; // 控制小球是不是第1次离开横板
    private Rigidbody2D _rb; // 刚体
    
    
    private void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    private void Update()
    {
        if (Input.anyKey && _num == 0)
        {
            _rb.linearVelocity = Vector2.up * speed;
            _num++;
        }

        if (transform.position.y < endY)
        {
            SceneManager.LoadScene("Game");
        }
    }

    // 小球与横板接触位置与反弹方向的计算公式
    private float HitFactor(Vector2 ballPos, Vector2 racketPos, float racketWidth)
    {
        return (ballPos.x - racketPos.x) / racketWidth;
    }

    // 发球的碰撞触发器
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.name == "racket" && _num == 1)
        {
            float x = HitFactor(transform.position, other.transform.position, other.collider.bounds.size.x);
            Vector2 dir = new Vector2(x, 1).normalized;
            _rb.linearVelocity = dir * speed;
        }
    }
}
