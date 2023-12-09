using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class HoeUse : ItemUse
{
    private Hoe hoeItem;

    private float maxDistance = 2.0f;

    private Tilemap groundTilemap = null;

    private Tile highlightedTile = null;

    private Vector3Int highlightedTileCell = Vector3Int.zero;
    private Vector3 highlightedTilePosition = Vector3.zero;

    private GameObject ghostTile = null;
    private SpriteRenderer ghostTileSpriteRenderer = null;

    void Start()
    {
        hoeItem = item as Hoe;

        var tilemapObject = GameObject.FindGameObjectWithTag("Ground Tilemap");
        if (tilemapObject != null)
            groundTilemap = tilemapObject.GetComponent<Tilemap>();

        if (groundTilemap == null)
        {
            Destroy(this);
            return;
        }

        Cursor.visible = true;
        CreateGhostTile();
    }

    void CreateGhostTile()
    {
        ghostTile = new GameObject("GhostTile");

        ghostTileSpriteRenderer = ghostTile.AddComponent<SpriteRenderer>();

        Sprite sprite = hoeItem.SoilTile.gameObject.GetComponent<SpriteRenderer>().sprite;
        ghostTileSpriteRenderer.sprite = Sprite.Create(sprite.texture, sprite.rect, 
                                                       new Vector2(0, 0), sprite.pixelsPerUnit);
        //ghostTileSpriteRenderer.sprite = sprite;

        ghostTile.transform.position = new Vector3(0, 0, -1);
        //ghostTile.transform.localScale = new Vector3(1, 1, 1);
    }

    private void FixedUpdate()
    {
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        Vector3Int cellPosition = groundTilemap.WorldToCell(mousePosition);
        TileBase tileBase = groundTilemap.GetTile(cellPosition);
        Tile tile = tileBase as Tile;
        if (tile == null)
        {
            return;
        }

        highlightedTile = tile;
        highlightedTilePosition = groundTilemap.CellToWorld(cellPosition);
        highlightedTileCell = cellPosition;
        MouseHoverHighlightedTile();
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (highlightedTile == null) return;

            MouseDownOnHighlightedTile();
        }
    }

    private void OnDestroy()
    {
        if (ghostTile != null)
        {
            Destroy(ghostTile);
        }
    }

    private void MouseHoverHighlightedTile()
    {
        ghostTile.transform.position = new Vector3(highlightedTilePosition.x, highlightedTilePosition.y, 
                                                   highlightedTilePosition.z - 0.1f);

        if (CanPlow(highlightedTile, highlightedTilePosition))
        {
            ghostTileSpriteRenderer.color = new Color(0, 1, 0, 0.7f);
        }
        else
        {
            ghostTileSpriteRenderer.color = new Color(1, 0, 0, 0.7f);
        }
    }

    private bool CanPlow(Tile tile, Vector3 tilePosition)
    {
        float distance = Vector3.Distance(transform.position, tilePosition);
        return distance <= maxDistance && tile is not SoilTile;
    }

    private void MouseDownOnHighlightedTile()
    {
        Debug.Log("Mouse down");

        if (!CanPlow(highlightedTile, highlightedTilePosition)) return;

        Debug.Log("Can plow");

        SoilTile soilTile = hoeItem.SoilTile;

        if (soilTile == null)
            return;

        groundTilemap.SetTile(highlightedTileCell, soilTile);
    }
}
