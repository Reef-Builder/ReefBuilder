using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class GameClipPlayer : MonoBehaviour
{

    public int gameId = -1;

    private int spriteIndex = 0;
    private Sprite[] sprites;

    private float framesPerSecond = 1;
    private float lastChanged = 0;

    private Image image;

    // Use this for initialization
    void Start()
    {
        var textures = SaveLoad.LoadSaveGameClip(gameId);
 
        image = GetComponent<Image>();

        if (textures == null)
        {
            return;
        }        

        sprites = textures.Where(tex => tex != null)
            .Select(tex =>
            Sprite.Create(tex, new Rect(0, 0, tex.width, tex.height), new Vector2(0.5f, 0.5f)))
            .ToArray();
        
        if(sprites != null && sprites.Length != 0)
        {
            GetComponentInChildren<Text>().text = "";
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (sprites == null || sprites.Length == 0)
        {
            return;
        }
        if (image.sprite == null || Time.time - lastChanged >= 1 / framesPerSecond)
        {
            image.color = Color.white;
            var newIndex = spriteIndex++ % sprites.Length;
            image.sprite = sprites[newIndex];

            lastChanged = Time.time;
        }
    }
}
