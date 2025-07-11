using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Done_BlockController : MonoBehaviour
{
    public GameObject upgradePrefab;
    // Use this for initialization
    void Start()
    {

        string spriteFileName = "sprites/block_" + GetComponent<Done_Block>().color;//获取颜色名称

        this.GetComponent<SpriteRenderer>().sprite = Resources.Load<UnityEngine.Sprite>(spriteFileName);//贴图

    }

    /// <summary>
    /// 球与砖块的碰撞检测
    /// </summary>
    /// <param name="col"></param>
    void OnCollisionEnter2D(Collision2D col)
    {
        GameObject go = GameObject.Find("Main Camera");
        Done_LevelLoader levelLoader = go.GetComponent<Done_LevelLoader>();
        gameObject.GetComponent<Done_Block>().hits_required -= 1;

        if (gameObject.GetComponent<Done_Block>().hits_required == 0)
        {
            Destroy(gameObject);
            levelLoader.block_count--;
            if (Random.value < 0.10)//生成概率
            {
                Instantiate(upgradePrefab,
                            new Vector3(
                                col.gameObject.transform.position.x,
                                col.gameObject.transform.position.y,
                                0),
                            Quaternion.identity);

            }
        }
        
    }
}
