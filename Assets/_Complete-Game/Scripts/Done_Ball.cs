using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class Done_Ball : MonoBehaviour {
    public float BallSpeed = 18f;//如果速度过快，可以减小该值，但是可能受到重力影响，不能弹到上方，可以适当减小重力值，如：速度设置为12，Unity中Physics2D的重力值应为-4.45
    int num = 0;

    // Use this for initialization
    void Start()
    {



    }

    // Update is called once per frame
    void Update()
    {

        if (Input.anyKey && num == 0)//num控制小球是不是第一次离开横板
        {
            GetComponent<Rigidbody2D>().linearVelocity = Vector2.up * BallSpeed;
            num++;
        }


        if (transform.position.y < -8)//小球掉落后重载场景
        {
            SceneManager.LoadScene("Done_Break Out");
        }
    }

    /// <summary>
    /// 发球的碰撞触发器
    /// </summary>
    /// <param name="col"></param>
    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.name == "racket" && num == 1)
        {
            float x = HitFactor(transform.position,
                                col.transform.position, col.collider.bounds.size.x
                                );

            Vector2 dir = new Vector2(x, 1).normalized;
            GetComponent<Rigidbody2D>().linearVelocity = dir * BallSpeed;
        }


    }

    /// <summary>
    /// 球与板接触位置与反弹方向的公式
    /// </summary>
    /// <param name="ballPos"></param>
    /// <param name="racketPos"></param>
    /// <param name="racketWidth"></param>
    /// <returns></returns>
    float HitFactor(Vector2 ballPos, Vector2 racketPos, float racketWidth)
    {
        return (ballPos.x - racketPos.x) / racketWidth;
    }
}
