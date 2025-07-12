using System.IO;
using System.Text;
using UnityEngine;
using Random = UnityEngine.Random;


public class LevelLoader : MonoBehaviour
{
    public GameObject brickPrefab;
    [HideInInspector]
    public int brickCount;


    private void Start()
    {
        string levelName = GetRandomLevelName();
        print($"LevelLoader: {levelName}");
        LoadLevel(levelName);
    }

    // 随机获取地图
    public string GetRandomLevelName()
    {
        int level = Random.Range(1, 6);
        return $"Assets/Levels/level_{level}.txt";
    }

    // 加载地图
    public void LoadLevel(string levelName)
    {
        using StreamReader reader = new StreamReader(levelName, Encoding.Default);
        string line = reader.ReadLine();
        float posX = -5f;
        float posY = 5.8f;
        while (!string.IsNullOrEmpty(line))
        {
            char[] chars = line.ToCharArray();
            foreach (char ch in chars)
            {
                if (ch == 'X')
                {
                    posX += 0.87f;
                    continue;
                }

                Vector2 brickPos = new Vector2(posX, posY);
                GameObject brickObj = Instantiate(brickPrefab, brickPos, Quaternion.identity);
                brickObj.GetComponent<BoxCollider2D>().size = new Vector2(0.8f, 0.4f);
                var (color, hits) = GetBrickInfo(brickObj, ch);
                brickObj.GetComponent<Brick>().color = color;
                brickObj.GetComponent<Brick>().hitsRequired = hits;
                
                brickCount++;

                posX += 0.87f;
            }

            posX = -5.5f;
            posY -= 0.45f;
            line = reader.ReadLine();
        }
    }

    private (string, int) GetBrickInfo(GameObject brick, char ch)
    {
        switch (ch)
        {
            case 'B': return ("blue", 3);
            case 'G': return ("green", 2);
            case 'P': return ("pink", 1);
            case 'R': return ("red", 5);
            case 'Y': return ("yellow", 4);
            default: Destroy(brick);
                throw new UnityException($"未知字符{ch}");
        }
    }
}
