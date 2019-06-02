using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GamePause : MonoBehaviour {

    public GameObject canvas;
    public GameObject P2;

    private PieceScript piece;
    private PieceScript pieceP2;

    void Update() {
        if (Input.GetKeyDown(KeyCode.Escape)) {
            piece = transform.GetChild(1).GetComponent<PieceScript>();

            if (P2 != null) {
                pieceP2 = P2.transform.GetChild(1).GetComponent<PieceScript>();

                if (piece.gameOver && pieceP2.gameOver)
                    return;
            }
            else if (piece.gameOver)
                return;

            if (canvas.activeInHierarchy) {
                piece.isStaticPiece = false;

                if(P2 != null)
                    pieceP2.isStaticPiece = false;

                canvas.SetActive(false);

                canvas.transform.Find("Text").GetComponent<Text>().text = "GAME OVER";
            }

            else {
                piece.isStaticPiece = true;

                if (P2 != null)
                    pieceP2.isStaticPiece = true;

                canvas.transform.Find("Text").GetComponent<Text>().text = "PAUSE";

                canvas.SetActive(true);
            }
        }
    }
}
