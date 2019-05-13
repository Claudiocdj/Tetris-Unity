using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PieceScript : MonoBehaviour {

    private GameController gc;
    private int lengthMap;
    private int widthMap;
    private Vector3 pivotMap = new Vector3();

    private GameObject[] block = new GameObject[4];

    private Vector3 initPos;
    private bool gameOver = false;
    public bool isStaticPiece = false;

    private float timeCount = 0f;
    private float timer;

    private PieceColorScript pcs;

    void Start() {
        gc = transform.root.GetComponent<GameController>();

        pcs = GetComponent<PieceColorScript>();

        initPos = gameObject.transform.position;
        
        lengthMap = gc.length;
        widthMap = gc.width;
        pivotMap = gc.gameObject.transform.position;

        timer = gc.CurrentTime;

        for (int i = 0; i < 4; i++) {
            transform.GetChild(i).gameObject.GetComponent<SpriteRenderer>().sprite = pcs.GetSprite(gc.CurrentLevel);

            block[i] = transform.GetChild(i).gameObject;
        }
    }

    void Update() {
        if (gameOver || isStaticPiece) return;

        if(!gameOver) InputCheck();

        if (timeCount >= timer) {
            timeCount = 0f;
            
            MoveY();
        }

        else
            timeCount += Time.deltaTime;
    }

    private void InputCheck() {

        if (Input.GetKeyDown(gc.rightInput) && CanMovePiece(Vector3.right))
            transform.position += Vector3.right;

        else if (Input.GetKeyDown(gc.leftInput) && CanMovePiece(Vector3.left))
            transform.position += Vector3.left;

        if (Input.GetKeyDown(gc.rotateInput)) RotateBlock();

        if (Input.GetKeyDown(gc.speedInput)) timer = gc.timerAcc;
    }

    private void RotateBlock() {
        float x, y;

        Vector3[] newPos = new Vector3[4];

        for(int i = 0; i < 4; i++) {
            x = block[i].transform.localPosition.y;

            y = block[i].transform.localPosition.x;

            newPos[i] = transform.position + new Vector3(x,-y, 0f);

            if (!CanMoveBlock(newPos[i]))
                return;
        }

        for (int i = 0; i < 4; i++)
            block[i].transform.position = newPos[i];
    }

    private bool CanMovePiece(Vector3 dir) {

        for (int i = 0; i < 4; i++) {
            Vector3 newpos = block[i].transform.position + dir;

            if (!CanMoveBlock(newpos)) return false;
        }

        return true;
    }

    private bool CanMoveBlock(Vector3 newpos) {
        if (newpos.x < pivotMap.x || newpos.x > pivotMap.x + widthMap - 1)
            return false;

        if (newpos.y > pivotMap.y + lengthMap - 1) return true;

        //if (newpos.y < pivotMap.y || newpos.y > pivotMap.y + lengthMap - 1)
        if (newpos.y < pivotMap.y)
            return false;
        
        if(gc.PosContains(newpos))
            return false;

        return true;
    }

    private void MoveY() {
        if (CanMovePiece(Vector3.down))
            transform.position += Vector3.down;

        else if(initPos == transform.position) {
            gc.GameOver();

            gameOver = true;
        }

        else {
            for (int i = 0; i < 4; i++) {
                block[i].transform.parent = null;

                gc.SetGrid(block[i].transform.position);

                block[i].GetComponent<BoxCollider2D>().enabled = true;
            }

            Destroy(gameObject);

            gc.NewPiece();
        }
    }
}
