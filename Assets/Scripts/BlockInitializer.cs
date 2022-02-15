using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BlockInitializer : MonoBehaviour
{
    [SerializeField] private int maxBombNumber = 15;
    [SerializeField] private int rowCount = 9;
    [SerializeField] private int colCount = 9;
    private Block[,] blocks;

    [SerializeField] private GridLayoutGroup grid;
    [SerializeField] private GameObject blockPrefab;
    [SerializeField] private Sprite[] numberSprites;
    [SerializeField] private Sprite bombSprite;
    void Start()
    {
        blocks = new Block[rowCount, colCount];
        grid.constraintCount = colCount;

        for(int row = 0; row < rowCount; row++)
        {
            for(int col = 0; col < colCount; col++)
            {
                GameObject currentBlock = Instantiate(blockPrefab, transform);
                currentBlock.name = $"Block({row}, {col})";
                blocks[row, col] = currentBlock.GetComponent<Block>();
            }
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.K))
        {
            InitiallizeBlocks();
        }
    }

    private void InitiallizeBlocks()
    {
        int currentBombs = 0;

        while(currentBombs <= maxBombNumber)
        {
            int randomNumberRow = Random.Range(0, rowCount);
            int randomNumberCol = Random.Range(0, colCount);

            if (!blocks[randomNumberRow, randomNumberCol].isBomb)
            {
                Block bomb = blocks[randomNumberRow, randomNumberCol];
                bomb.isBomb = true;
                bomb.blockImage.sprite = bombSprite;

                int startRow = randomNumberRow - 1;
                int maxRow = randomNumberRow + 1;
                int startCol = randomNumberCol - 1;
                int maxCol = randomNumberCol + 1;

                if (randomNumberRow == 0) startRow += 1;
                if (randomNumberRow == rowCount - 1) maxRow -= 1;
                if (randomNumberCol == 0) startCol += 1;
                if (randomNumberCol == colCount - 1) maxCol -= 1;

                for(int row = startRow; row <= maxRow; row++)
                {
                    for(int col = startCol; col <= maxCol; col++)
                    {
                        blocks[row, col].bombsAround++;
                    }
                }

                currentBombs++;
            }
        }

        for(int i = 0; i < rowCount; i++)
        {
            for (int j = 0; j < colCount; j++)
            {
                if(!blocks[i, j].isBomb)
                {
                    blocks[i, j].blockImage.sprite = numberSprites[blocks[i, j].bombsAround];
                }
            }
        }
    }
}
