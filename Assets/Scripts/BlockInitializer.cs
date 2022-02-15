using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BlockInitializer : MonoBehaviour
{
    [SerializeField] private int maxBombNumber = 15;
    private bool isInitiallized = false;
    [SerializeField] private GameObject blockPrefab;
    private GameObject[,] blocks = new GameObject[9, 9];
    [SerializeField] private Sprite[] numberSprites;
    [SerializeField] private Sprite bombSprite;
    void Start()
    {
        for(int row = 0; row < 9; row++)
        {
            for(int col = 0; col < 9; col++)
            {
                GameObject currentBlock = Instantiate(blockPrefab, transform);
                currentBlock.name = $"Block({row}, {col})";
                blocks[row, col] = currentBlock;
            }
        }

        isInitiallized = true;
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
            int randomNumberRow = Random.Range(0, 9);
            int randomNumberCol = Random.Range(0, 9);

            if (!blocks[randomNumberRow, randomNumberCol].CompareTag("Bomb"))
            {
                GameObject bomb = blocks[randomNumberRow, randomNumberCol];
                bomb.tag = "Bomb";
                bomb.transform.GetChild(0).GetComponent<Image>().sprite = bombSprite;

                int startRow = randomNumberRow - 1;
                int maxRow = randomNumberRow + 1;
                int startCol = randomNumberCol - 1;
                int maxCol = randomNumberCol + 1;

                if (randomNumberRow == 0) startRow += 1;
                if (randomNumberRow == 8) maxRow -= 1;
                if (randomNumberCol == 0) startCol += 1;
                if (randomNumberCol == 8) maxCol -= 1;

                for(int row = startRow; row <= maxRow; row++)
                {
                    for(int col = startCol; col <= maxCol; col++)
                    {
                        blocks[row, col].GetComponent<Block>().bombsAround++;
                    }
                }

                currentBombs++;
            }
        }

        for(int i = 0; i < 9; i++)
        {
            for (int j = 0; j < 9; j++)
            {
                if(blocks[i, j].tag != "Bomb")
                {
                    Transform block = blocks[i, j].transform;
                    int bombsAround = block.GetComponent<Block>().bombsAround;
                    block.GetChild(0).GetComponent<Image>().sprite = numberSprites[bombsAround];
                }
            }
        }
    }
}
