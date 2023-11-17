using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plant : Interactable
{
    public enum PlantState
    {
        Seed,
        Child,
        Grown
    }

    [SerializeField]
    private int childGrowPointsThreshold;
    [SerializeField]
    private int grownGrowPointsThreshold;

    [SerializeField]
    private Sprite seedSpriteRenderer;
    [SerializeField]
    private Sprite childSpriteRenderer;
    [SerializeField]
    private Sprite grownSpriteRenderer;

    [SerializeField]
    private Item fruit;
    [SerializeField]
    private int fruitMinCount;
    [SerializeField]
    private int fruitMaxCount;

    public PlantState CurrentState { get; private set; }
    public int GrowPoints { get; private set; }

    private SpriteRenderer spriteRenderer;

    protected new void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        Debug.Log("I am planted!");
        base.Start();
        StartCoroutine(GrowTimer());
    }

    // For debug
    private IEnumerator GrowTimer()
    {
        yield return new WaitForSeconds(3);

        GrowPoints += 5;

        yield return new WaitForSeconds(3);

        GrowPoints += 5;
    }

    private void Update()
    {
        switch (CurrentState)
        {
            case PlantState.Seed:
                if (GrowPoints >= childGrowPointsThreshold)
                    CurrentState = PlantState.Child;
                if (GrowPoints >= grownGrowPointsThreshold)
                    CurrentState= PlantState.Grown;

                if (CurrentState != PlantState.Seed)
                    UpdateSprite();
                break;
            case PlantState.Child:
                if (GrowPoints >= grownGrowPointsThreshold)
                {
                    CurrentState = PlantState.Grown;
                    UpdateSprite();
                }
                break;
        }
    }

    private void UpdateSprite()
    {
        switch (CurrentState)
        {
            case PlantState.Seed:
                spriteRenderer.sprite = seedSpriteRenderer;
                break;
            case PlantState.Child:
                spriteRenderer.sprite = childSpriteRenderer;
                break;
            case PlantState.Grown:
                spriteRenderer.sprite = grownSpriteRenderer;
                break;
        }
    }

    public override void Interact(GameObject interactor)
    {
        if (interactor == null || CurrentState != PlantState.Grown) return;

        Inventory inventory = interactor.GetComponent<Inventory>();
        
        if (inventory == null) return;

        Debug.Log("Try add");
        int count = Random.Range(fruitMinCount, fruitMaxCount + 1);
        int added = inventory.AddItem(fruit, count);
        if (added != count)
        {
            int fruitsToDrop = count - added;
            for (int i = 0; i < fruitsToDrop; i++)
            {
                fruit.Drop(transform.position, transform.rotation);
            }
        }
        inventory.LogGrid();
    }

    public override void EnterInteractionArea(GameObject interactor)
    {
    }

    public override void LeaveInteractionArea(GameObject interactor)
    {
    }
}
