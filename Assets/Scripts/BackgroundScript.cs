using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BackgroundScript : MonoBehaviour{

    public float timeToNext;

    public Sprite[] imgList;

    private float timer;

    void Start() {
        int n = Random.Range(0, imgList.Length);

        transform.GetChild(0).GetComponent<Image>().sprite = imgList[n];

        timer = 0;
    }
    
    void Update(){
        if (timer > timeToNext) {
            int n = Random.Range(0, imgList.Length);

            transform.GetChild(0).GetComponent<Image>().sprite = imgList[n];

            timer = 0;
        }
        else
            timer += Time.deltaTime;
    }
}
