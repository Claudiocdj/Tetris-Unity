using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PieceColorScript : MonoBehaviour{

    public Sprite[] sprites;
    
    public Sprite GetSprite(int level) {
        if (level > 0 && level < sprites.Length)
            return sprites[level-1];

        else
            return sprites[Random.Range(0, sprites.Length)];
    }
}
