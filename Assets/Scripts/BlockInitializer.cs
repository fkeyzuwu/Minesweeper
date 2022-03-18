using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class BlockInitializer : MonoBehaviour
{
    public static BlockInitializer instance;

    [SerializeField] private int bombCount => GameData.bombCount;
    [SerializeField] private int rowCount => GameData.rowCount;
    [SerializeField] private int colCount => GameData.colCount;
    private Block[,] blocks;

    [SerializeField] private int revealedBlockCount = 0;
    [SerializeField] private int blockCount { get => rowCount * colCount; }
    [SerializeField] private int neededRevealedBlocksToWin { get => blockCount - bombCount;}

    [SerializeField] private GridLayoutGroup grid;
    [SerializeField] private GameObject blockPrefab;
    [SerializeField] private Sprite [] numberSprites;
    [SerializeField] private Sprite unrevealedSprite;
    [SerializeField] private Sprite bombSprite;
    [SerializeField] private Sprite flagSprite;

    [SerializeField] private GameObject restartGameButton;
    [SerializeField] private TextMeshProUGUI endGameText;
    [SerializeField] private Image endGameImage;

    public bool isInitialized = false;

    void Start()
    {
        if (instance == null)
        {
            instance = this;
        }

        blocks = new Block[rowCount, colCount];
        grid.constraintCount = colCount;

        for (int row = 0; row < rowCount; row++)
        {
            for (int col = 0; col < colCount; col++)
            {
                GameObject currentBlock = Instantiate(blockPrefab, transform);
                currentBlock.name = $"Block({row}, {col})";
                blocks[row, col] = currentBlock.GetComponent<Block>();
                blocks[row, col].row = row;
                blocks[row, col].col = col;
            }
        }
    }

    public void ResetGame()
    {
        for(int row = 0; row < rowCount; row++)
        {
            for (int col = 0; col < colCount; col++)
            {
                blocks[row, col].isBomb = false;
                blocks[row, col].isFlagged = false;
                blocks[row, col].isRevealed = false;
                blocks[row, col].blockImage.sprite = unrevealedSprite;
                blocks[row, col].bombsAround = 0;
                blocks[row, col].blockContentImage.sprite = numberSprites[0];
                blocks[row, col].SetBlockContent(false);
            }
        }

        revealedBlockCount = 0;
        isInitialized = false;
    }

    public void InitiallizeBlocks(int blockRow, int blockCol)
    {
        isInitialized = true;

        int currentBombs = 0;

        int topBorder = blockRow + 1;
        int bottomBorder = blockRow - 1;
        int rightBorder = blockCol + 1;
        int leftBorder = blockCol - 1;

        if (blockRow == 0) topBorder -= 1;
        if (blockRow == rowCount - 1) bottomBorder += 1;
        if (blockCol == 0) leftBorder += 1;
        if (blockCol == colCount - 1) rightBorder -= 1;
       
        while (currentBombs < bombCount)
        {
            int randomNumberRow = Random.Range(0, rowCount);
            int randomNumberCol = Random.Range(0, colCount);

            bool isInRowBorder = randomNumberRow >= bottomBorder && randomNumberRow <= topBorder;
            bool isInColBorder = randomNumberCol >= leftBorder && randomNumberCol <= rightBorder;
            if (isInColBorder && isInRowBorder) continue;

            if (!blocks[randomNumberRow, randomNumberCol].isBomb)
            {
                Block bomb = blocks[randomNumberRow, randomNumberCol];
                bomb.isBomb = true;
                bomb.blockContentImage.sprite = bombSprite;

                int startRow = randomNumberRow - 1;
                int maxRow = randomNumberRow + 1;
                int startCol = randomNumberCol - 1;
                int maxCol = randomNumberCol + 1;

                if (randomNumberRow == 0) startRow += 1;
                if (randomNumberRow == rowCount - 1) maxRow -= 1;
                if (randomNumberCol == 0) startCol += 1;
                if (randomNumberCol == colCount - 1) maxCol -= 1;

                for (int row = startRow; row <= maxRow; row++)
                {
                    for (int col = startCol; col <= maxCol; col++)
                    {
                        blocks[row, col].bombsAround++;
                    }
                }

                currentBombs++;
            }
        }

        for (int i = 0; i < rowCount; i++)
        {
            for (int j = 0; j < colCount; j++)
            {
                if (!blocks[i, j].isBomb)
                {
                    blocks[i, j].blockContentImage.sprite = numberSprites[blocks[i, j].bombsAround];
                }
            }
        }
    }

    public void RevealBlocks3x3(int blockRow, int blockCol)
    {
        int startRow = blockRow - 1;
        int maxRow = blockRow + 1;
        int startCol = blockCol - 1;
        int maxCol = blockCol + 1;

        if (blockRow == 0) startRow += 1;
        if (blockRow == rowCount - 1) maxRow -= 1;
        if (blockCol == 0) startCol += 1;
        if (blockCol == colCount - 1) maxCol -= 1;

        for (int row = startRow; row <= maxRow; row++)
        {
            for (int col = startCol; col <= maxCol; col++)
            {
                if (blocks[row, col].isFlagged || blocks[row, col].isRevealed) continue;

                if (blocks[row, col].isBomb)
                {
                    GameOver(false);
                    return;
                }

                if (blocks[row,col].bombsAround == 0)
                {
                    RevealEmptyBlocks(row, col);
                    continue;
                }

                RevealBlock(row, col);
            }
        }
    }

    public bool CheckFlagsCount(int blockRow, int blockCol)
    {
        bool equalFlags = false;
        int flagCount = 0;
        int startRow = blockRow - 1;
        int maxRow = blockRow + 1;
        int startCol = blockCol - 1;
        int maxCol = blockCol + 1;

        if (blockRow == 0) startRow += 1;
        if (blockRow == rowCount - 1) maxRow -= 1;
        if (blockCol == 0) startCol += 1;
        if (blockCol == colCount - 1) maxCol -= 1;

        for (int row = startRow; row <= maxRow; row++)
        {
            for (int col = startCol; col <= maxCol; col++)
            {
                if (blocks[row, col].isFlagged) flagCount++;
            }
        }

        if (blocks[blockRow, blockCol].bombsAround == flagCount) equalFlags = true;

        return equalFlags;
    }
    public void RevealBlock(int blockRow, int blockCol)
    {
        blocks[blockRow, blockCol].SetBlockContent(true);
        revealedBlockCount++;
        if (revealedBlockCount == neededRevealedBlocksToWin)
        {
            GameOver(true);
        }
    }

    public void RevealEmptyBlocks(int row, int col)
    {
        if (blocks[row, col].isRevealed) return;
        RevealBlock(row, col);
        if (blocks[row, col].bombsAround > 0) return;

        int startRow = row - 1;
        int maxRow = row + 1;
        int startCol = col - 1;
        int maxCol = col + 1;

        if (row == 0) startRow += 1;
        if (row == rowCount - 1) maxRow -= 1;
        if (col == 0) startCol += 1;
        if (col == colCount - 1) maxCol -= 1;

        for(int i = startRow; i<=maxRow;i++)
        {
            for (int j = startCol; j <= maxCol; j++)
            {
                RevealEmptyBlocks(i, j);
            }
        }
    }

    public void FlagBlock(int row, int col)
    {
        Block block = blocks[row, col];
        block.isFlagged = !block.isFlagged;
        block.blockImage.sprite = block.isFlagged ? flagSprite : unrevealedSprite;
    }

    public void RevealAllBlocks()
    {
        for (int row = 0; row < rowCount; row++)
        {
            for (int col = 0; col < colCount; col++)
            {
                blocks[row, col].SetBlockContent(true);
            }
        }
    }

    public void GameOver(bool win)
    {
        if (!win) RevealAllBlocks();
        restartGameButton.SetActive(true);
        endGameText.transform.parent.gameObject.SetActive(true);
        endGameText.text = win ? "You Won!" : "You Lost!";
        endGameImage.color = win ? Color.green : Color.red;
    }
}
