using UnityEngine;
using System.Collections;

public class Done_UpGrade : MonoBehaviour
{

    public Sprite[] upgradeSprites;
    public string upgradeName = "";

    // Use this for initialization
    void Start()
    {
        Sprite icon = upgradeSprites[Random.Range(0, upgradeSprites.Length)];//随机选择图片
        upgradeName = icon.ToString();//与图片对应的道具名字
        this.gameObject.GetComponent<SpriteRenderer>().sprite = icon;//贴图
    }

    ///
    // Update is called once per frame
    void Update()
    {
        //道具位置刷新
        this.gameObject.transform.position = new Vector3(this.gameObject.transform.position.x,
                                                            this.gameObject.transform.position.y - 0.05f,
                                                            0);
        //如果道具低于横板，则销毁
        if (gameObject.transform.position.y <= -8.0f)
            Destroy(this.gameObject);
    }
}