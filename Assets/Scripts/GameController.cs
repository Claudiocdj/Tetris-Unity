using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour {
    
    public GameObject[] pieces;

    public int width;
    public int length;
    public Vector2 instantiatePos;

    public float timer;
    public float timerAcc;

    private bool[,] grid;

    private void Start() {
        grid = new bool[width, length];

        newPiece();
    }

    private void Update() {
        CheckLines();
    }

    public void SetGrid(Vector3 val) {
        int x = (int)(val.x - transform.position.x);
        int y = (int)(val.y - transform.position.y);

        grid[x, y] = true;
    }

    public void newPiece() {
        int n = Random.Range(0,pieces.Length);

        n = 4;

        GameObject obj = Instantiate(pieces[n], instantiatePos, Quaternion.identity);

        obj.transform.parent = gameObject.transform;
    }

    public bool PosContains(Vector3 val) {
        int x = (int)(val.x - transform.position.x);
        int y = (int)(val.y - transform.position.y);
        
        if (grid[x, y]) return true;

        else return false;
    }

    public void CheckLines() {
        int j, i = 0, count;
        
        while( i < length ) {

            count = 0;

            for(j = 0; j < width; j++) 
                if (grid[j, i]) count++;

            if (count == 0)
                return;

            if (count == 10) {
                RemoveLine(i);
                return;
            }

            else i++;
        }
    }

    private RaycastHit2D[] LinecastLine(int line) {
        float x = transform.position.x;
        float y = transform.position.y;

        Vector2 start = new Vector2(x + .5f, y + line + .5f);

        Vector2 end = new Vector2(x + 9.5f, y + line + .5f);

        //Debug.DrawLine(start, end, Color.green, 1f);

        return Physics2D.LinecastAll(start, end);
    }

    private void RemoveLine(int line) {
        RaycastHit2D[] cols = LinecastLine(line);

        foreach (var col in cols)
            Destroy(col.collider.gameObject);

        for (int i = line + 1; i < length; i++)
            if (!DownLine(i)) break;
    }

    private bool DownLine(int line) {
        for (int i = 0; i < width; i++)
            grid[i, line-1] = grid[i, line];

        RaycastHit2D[] cols = LinecastLine(line);

        if (cols.Length == 0) return false;

        foreach (var col in cols)
            col.collider.gameObject.transform.position += Vector3.down;

        return true;
    }

    /*
    public GameObject oneBlockPrefab;
    public GameObject[] blocks;
    public readonly float delay = 0.3f;
    public readonly float accelerate = 0.01f;

    public List<Vector3> blockList = new List<Vector3>();
    private float timer;
    private float gameDelay;
    private GameObject block;
    private Vector3[] pos = new Vector3[4];
    private Vector3[] newPos = new Vector3[4];

    private void Start() {
        timer = 0;

        gameDelay = delay;

        InstantiateNewBlock();
    }

    private void Update() {
        timer += Time.deltaTime;

        CheckMovement();

        //ao se passar um tempo o bloco cai ou interage com os blocos ja posicionados
        if (timer > gameDelay) {
            timer = 0f;

            if (CanMove(Vector3.down))
                block.transform.position = block.transform.position + Vector3.down;

            else {
                gameDelay = delay;

                for (int i = 0; i < 4; i++) {
                    blockList.Add(pos[i]);

                    Instantiate(oneBlockPrefab, pos[i], Quaternion.identity);
                }

                blockList.Sort((a, b) => b.y.CompareTo(a.y));

                Destroy(block);
                
                InstantiateNewBlock();
            }

            
        }

        if(blockList.Count > 0) CheckLines();
    }

    //Controla a movimentacao horizontal do conjunto
    private void MovementX(int dir) {
        if(CanMove(new Vector3(dir, 0f, 0f)))
            block.transform.position = block.transform.position + new Vector3(dir,0f,0f);
    }

    //Checa se o conjunto pode realizar a movimentacao pretendida
    private bool CanMove(Vector3 movement) {
        //Debug.Log(pos[0] + "/" + pos[1] + "/" + pos[2] + "/" + pos[3]);

        for (int i = 0; i < 4; i++)
            newPos[i] = pos[i] + movement;

        for(int i = 0; i < 4; i++) {
            if (newPos[i].x < 0 || newPos[i].x > 9)
                return false;

            if (newPos[i].y < -19)
                return false;

            if (blockList.Contains(newPos[i]))
                return false;
        }

        SetPos();

        return true;
    }

    private bool CanMove() {
        //Debug.Log(newPos[0] + "/" + newPos[1] + "/" + newPos[2] + "/" + newPos[3]);

        for (int i = 0; i < 4; i++) {
            if (newPos[i].x < 0 || newPos[i].x > 9)
                return false;

            if (newPos[i].y < -19)
                return false;

            if (blockList.Contains(newPos[i]))
                return false;
        }

        SetPos();

        return true;
    }

    //Atualiza a variavel com a posicao atual dos blocos em movimento
    private void SetPos() {

        for (int i = 0; i < 4; i++)
            pos[i] = newPos[i];
    }

    //Instancia um novo bloco no jogo
    private void InstantiateNewBlock() {
        block = Instantiate(blocks[Random.Range(0, 7)], new Vector3(5f, -1f, 0f), Quaternion.identity);

        for (int i = 0; i < 4; i++)
            newPos[i] = block.transform.GetChild(i).transform.position;

        SetPos();

        //GameOver
        if (!CanMove())
            Debug.Log("PERDEU");
    }

    private void CheckLines() {
        RaycastHit2D[] colliders;

        int lineCheck = -19;

        while (lineCheck <= blockList[0].y) {
            
            Vector2 start = new Vector2( 0, lineCheck - 0.5f);
            Vector2 end   = new Vector2(10, lineCheck - 0.5f);

            colliders = Physics2D.LinecastAll(start, end);
            

            if (colliders.Length == 10) {
                

                foreach(var col in colliders) {
                    Destroy(col.collider.gameObject);

                    blockList.Remove(col.collider.gameObject.transform.position);
                }

                blockList.Sort((a, b) => b.y.CompareTo(a.y));

                UpdateScreen(lineCheck);

                return;
            }

            else lineCheck++;
        }
    }

    private void UpdateScreen(int lineCheck) {
        RaycastHit2D[] colliders;

        lineCheck++;

        while (lineCheck <= blockList[0].y) {
            
            Vector2 start = new Vector2(0, lineCheck - 0.5f);
            Vector2 end = new Vector2(10, lineCheck - 0.5f);

            colliders = Physics2D.LinecastAll(start, end);
            
            foreach (var col in colliders) {
                
                blockList.Remove(col.collider.gameObject.transform.position);

                col.collider.gameObject.transform.position += Vector3.down;

                blockList.Add(col.collider.gameObject.transform.position);
            }

            blockList.Sort((a, b) => b.y.CompareTo(a.y));

            lineCheck++;
        }
    }
    */
}
