using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridManager : MonoBehaviour
{
    [SerializeField] private Transform[] gridPositions;
    [SerializeField] private int startIndex;
    int currentIndex;
    const int maxHeight = 2;
    const int maxWidth = 4;

    void Awake() => currentIndex = startIndex;

    public Vector3 GetNewGridPosition(Vector3 currentPos, Vector2 direction)
    {
        if(direction == Vector2.up)
        {
            if(currentPos.y == maxHeight)
                return currentPos;
            else
            {
                currentIndex = currentIndex - 3;
                return gridPositions[currentIndex].position;
            }
        }
        else if(direction == Vector2.down)
        {
            if(currentPos.y == -maxHeight)
                return currentPos;
            else
            {
                currentIndex = currentIndex + 3;
                return gridPositions[currentIndex].position;
            }
        }

        if(direction == Vector2.right)
        {
            if(currentPos.x == maxWidth)
                return currentPos;
            else
            {
                currentIndex++;
                return gridPositions[currentIndex].position;
            }
        }
        else if(direction == Vector2.left)
        {
            if(currentPos.x == -maxWidth)
                return currentPos;
            else
            {
                currentIndex--;
                return gridPositions[currentIndex].position;
            }
        }
        return currentPos;
    }

    public Vector3 GetRandomGridPostion()
    {
        int randomIndex = Random.Range(0, gridPositions.Length);
        return gridPositions[randomIndex].position;
    }

    public Vector3 GetStartPos() => gridPositions[startIndex].position;
}
