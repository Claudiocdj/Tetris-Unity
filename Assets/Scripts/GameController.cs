using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour {
    
    public GameObject[] pieces;
    

    public int width;
    public int length;
    private Vector2 instantiatePos;
    private Vector2 nextPiecePos;

    public float timer;
    public float timerAcc;
    public float timeFactorPerLevel;

    public KeyCode leftInput;
    public KeyCode rightInput;
    public KeyCode speedInput;
    public KeyCode rotateInput;

    public GameObject score;
    public GameObject gameOver;

    private float currentTime;
    private bool[,] grid;
    private int nextPiece = -1;
    private GameObject nextPieceObj;
    private ScoreScript ss;
    

    //debug
    //public GameObject blockDebug;
    //public GameObject[,] goGrid;

    public float CurrentTime { get { return currentTime; } }

    public int CurrentLevel { get { return ss.Level; } }
    
    private void Start() {
        grid = new bool[width, length];

        ss = score.GetComponent<ScoreScript>();

        //goGrid = new GameObject[width, length];

        //InstDebug();

        float x = gameObject.transform.position.x + width / 2;
        float y = gameObject.transform.position.y + length - 2;

        instantiatePos = new Vector2(x, y);

        nextPiecePos = ss.nextPiece.transform.position;

        InstantiatePiece();
    }

    private void Update() {
        //DebugGrid();
    }
    /*
    private void InstDebug() {
        for (int i = 0; i < length; i++) {
            for (int j = 0; j < width; j++) {
                goGrid[j,i] = (Instantiate(blockDebug, new Vector3(2f + j, -10f + i, transform.position.z), Quaternion.identity));
            }
        }
    }

    private void DebugGrid() {

        for (int i = 0; i < length; i++) {
            for (int j = 0; j < width; j++) {
                if (grid[j, i])
                    goGrid[j, i].gameObject.SetActive(true);
                else
                    goGrid[j, i].gameObject.SetActive(false);
            }
        }
    }
    */
    public void SetGrid(Vector3 val) {
        int x = (int)(val.x - transform.position.x);
        int y = (int)(val.y - transform.position.y);

        grid[x, y] = true;
    }

    public bool PosContains(Vector3 val) {
        int x = (int)(val.x - transform.position.x);
        int y = (int)(val.y - transform.position.y);

        if (grid[x, y]) return true;

        else return false;
    }

    public void NewPiece() {
        StartCoroutine(CheckLines());

        InstantiatePiece();
    }

    private IEnumerator CheckLines() {
        int blocksOnTheLine, destroyedLines = 0;

        for(int i = 0; i < length; i++) {
            yield return new WaitForFixedUpdate();
            blocksOnTheLine = 0;

            for (int j = 0; j < width; j++)
                if (grid[j, i]) blocksOnTheLine++;

            if (blocksOnTheLine == 0) break;

            else if (blocksOnTheLine == width) {
                StartCoroutine(DestroyLine(i));
                destroyedLines++;
            }
            
            else if(destroyedLines > 0)
                StartCoroutine(DownLine(i, destroyedLines));
        }

        if (destroyedLines == 4) StartCoroutine(TetrisEffect());

        ss.AddLines(destroyedLines);

        //DebugGrid();
    }

    private IEnumerator DestroyLine(int line) {
        RaycastHit2D[] cols = LinecastLine(line);

        //Debug.Log("cols.Length: " + cols.Length);

        for (int i = 0; i < width; i++)
            grid[i, line] = false;
        
        for(int i = width/2, j = width / 2 - 1; i <= width && j >= 0; i++, j--) {
                Destroy(cols[i].transform.gameObject);
                Destroy(cols[j].transform.gameObject);
                yield return new WaitForSeconds(.05f);
        }
    }

    private IEnumerator DownLine(int line, int downFactor) {
        yield return new WaitForSeconds(.2f);

        for (int i = 0; i < width; i++) {
            if (grid[i, line]) {
                grid[i, line] = false;

                grid[i, line - downFactor] = true;
            }
        }

        RaycastHit2D[] cols = LinecastLine(line);

        foreach (var col in cols)
            col.collider.gameObject.transform.position += new Vector3(0f,-1f*downFactor,0f);
    }

    private RaycastHit2D[] LinecastLine(int line) {
        float x = transform.position.x;
        float y = transform.position.y;

        Vector2 start = new Vector2(x - 0.5f, y + line + .5f);

        Vector2 end = new Vector2(x + 10.5f, y + line + .5f);

        Debug.DrawLine(start, end, Color.green, 10f);

        return Physics2D.LinecastAll(start, end);
    }

    private void InstantiatePiece() {
        if (ss.Level > 20)
            currentTime = 20 * timeFactorPerLevel;

        else
            currentTime = timer - (ss.Level - 1) * timeFactorPerLevel;

        if (nextPiece < 0 || nextPiece > pieces.Length)
            nextPiece = Random.Range(0, pieces.Length);

        GameObject obj = Instantiate(pieces[nextPiece], instantiatePos, Quaternion.identity);

        obj.transform.parent = gameObject.transform;

        nextPiece = Random.Range(0, pieces.Length);

        if (nextPieceObj != null)
            Destroy(nextPieceObj);

        nextPieceObj = Instantiate(pieces[nextPiece], nextPiecePos, Quaternion.identity);

        nextPieceObj.transform.parent = gameObject.transform;

        nextPieceObj.GetComponent<PieceScript>().isStaticPiece = true;
        
        nextPieceObj.name = "NextPiece";
    }

    private IEnumerator TetrisEffect() {

        SpriteRenderer sr = GetComponent<SpriteRenderer>();

        for (int i = 0; i < 10; i++) {
            sr.color = Color.white;
            yield return new WaitForSeconds(.01f);
            sr.color = Color.black;
            yield return new WaitForSeconds(.01f);
        }
    }

    public void GameOver() {
        gameOver.SetActive(true);
    }
}
