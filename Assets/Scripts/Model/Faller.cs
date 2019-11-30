using Tetris.Model;
using UnityEngine;
using Zenject;

public class Faller : MonoBehaviour
{
    private TetrisGrid tetrisGrid;
    public float fallRate = 1f;
    private float tickTimer;

    [Inject]
    public void Construct(TetrisGrid tetrisGrid)
    {
        this.tetrisGrid = tetrisGrid;
    }

    void Update()
    {
        tickTimer += Time.deltaTime;
        if (tickTimer >= fallRate)
        {
            tickTimer -= fallRate;
            tetrisGrid.ApplyGravity();
        }
    }
}