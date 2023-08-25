using System.Collections.Generic;
using UnityEngine;

public class DraggableUIGrid : MonoBehaviour
{
    private List<Draggable> items = new List<Draggable>();
    List<Vector3>[] grid = new List<Vector3>[0];
    public Transform upperLeftPos;
    public Transform bottomRightPos;
    public float xSpacing = .35f;
    public float ySpacing = .35f;


    public void ResetGrid() {
        grid = new List<Vector3>[0];
        items.Clear();
        OnItemChanged();
    }

    public void AddItemToGrid(Draggable spawnedItem)
    {
        items.Add(spawnedItem);
        //print("Item to grid... item count " + items.Count);
        OnItemChanged();
    }


    public void OnItemChanged()
    {

        //print("On Item changed");
        float xRange = (bottomRightPos.position.x - upperLeftPos.position.x);
        float yRange = (upperLeftPos.position.y - bottomRightPos.position.y);
        float centerY = bottomRightPos.position.y + yRange / 2f;
        int maxItemsPerRow = Mathf.RoundToInt(xRange / xSpacing);

        if (maxItemsPerRow <= 0) { Debug.LogWarning("UI Grid Max Rows 0... space or range is likely wrong."); return; }

        //print("item count: " + items.Count + " max itesm per row: " + maxItemsPerRow);
        int rows = Mathf.CeilToInt((float)items.Count / maxItemsPerRow);
        //print("rows: " + rows);

        grid = new List<Vector3>[rows];

        float totalHeight = (rows - 1) * ySpacing;
        float startY = centerY + totalHeight / 2;

        int runningLeft = items.Count;
        for (int gridY = 0; gridY < rows; gridY++)
        {
            int rowSize = Mathf.Min(runningLeft, maxItemsPerRow);
            grid[gridY] = new List<Vector3>(rowSize);
            float yVal = startY - gridY * ySpacing;
            for (int gridX = 0; gridX < rowSize; gridX++)
            {
                grid[gridY].Add(new Vector3(upperLeftPos.position.x + (xRange / rowSize) * gridX, yVal));
                //grid[gridY][gridX] = ;
                runningLeft--;
            }
        }

        //print("OnITemChanged done... grid count : " + grid.Length);

    }

    public void RemoveItemFromGrid(Draggable item)
    {
        items.Remove(item);
        OnItemChanged();
    }


    private void UpdateItemPositions()
    {
        int i = 0;
        for (int gridY = 0; gridY < grid.Length; gridY++)
        {
            for (int gridX = 0; gridX < grid[gridY].Count; gridX++)
            {
                items[i].MoveTo(Vector3.Lerp(items[i].transform.position, grid[gridY][gridX], Time.deltaTime * 20f));
                i++;
            }
        }
    }

    private void Update()
    {
        //If the grid has items in it, update their positions.
        if (grid.Length > 0)
        {
            UpdateItemPositions();
        }
    }

}
