﻿/**
    Author: Ashutosh Rautela
    Date: 29 October 2019
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridManager : MonoBehaviour
{
    public int rows = 20;
    public int columns = 11;

    public GameObject tilePrefab;
    public GameObject hexColumnPrefab;
    private static GridManager instance;

    private Vector2 gridBounds = Vector2.zero;
    private Vector2 tileBounds = Vector2.zero;
    // private List<HexRow> hexRows;
    public List<HexColumn> hexColumns;

    private Vector3 gridReferenceCordinate = Vector2.zero;
    private Vector3 tileAnchorCordinate = Vector2.zero;

    // Start is called before the first frame update
    void Start()
    {
        instance = this;

        RectTransform rt = this.GetComponent<RectTransform>();
        this.gridBounds.Set(rt.rect.width, rt.rect.height);

        RectTransform tI = tilePrefab.GetComponent<RectTransform>();
        this.tileBounds.Set(tI.rect.width, tI.rect.height);

        this.gridReferenceCordinate.Set(-this.gridBounds.x/2 , -this.gridBounds.y/2, 0);
        this.tileAnchorCordinate.Set(this.tileBounds.x/2 , this.tileBounds.y/2, 0);

        this.setupColumns().fillColumns();
    }

    private GridManager setupColumns() {
        for (int i = 0 ; i < columns; i++) {
            GameObject hexColumn = Instantiate(hexColumnPrefab, Vector2.zero, Quaternion.identity);
            hexColumn.transform.SetParent(this.transform, false);

            hexColumn.transform.localPosition = new Vector3(i * 3 * this.tileBounds.x/4 , 0 , 0) + this.gridReferenceCordinate + this.tileAnchorCordinate;
            if (i%2 != 0) {
                hexColumn.transform.localPosition = new Vector3(i * 3 *  this.tileBounds.x/4 , 0 , 0) + this.gridReferenceCordinate + this.tileAnchorCordinate + (this.tileBounds.y/2 * Vector3.up);
            }
            HexColumn hexColumn1 = hexColumn.GetComponent<HexColumn>();
            hexColumn1.ColumnIndex = i;
            this.hexColumns.Add(hexColumn1);
        }
        return this;
    }

    private GridManager fillColumns() {
        this.hexColumns.ForEach(delegate(HexColumn hexColumn){
            hexColumn.Fill(this.rows);
        });
        return this;
    }

     public static GridManager Instance
    {
        get { return GridManager.instance; }
    }

    public Vector2 TileBounds {
        get {return this.tileBounds;}
    }
}
