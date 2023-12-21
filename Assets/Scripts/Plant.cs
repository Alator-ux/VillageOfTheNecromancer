using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Plant : Interactable
{
    public enum PlantState
    {
        Seed,
        Child,
        Grown
    }

    [Header("Fruit info")]
    [SerializeField]
    private Item fruit;
    [SerializeField]
    private int fruitMinCount;
    [SerializeField]
    private int fruitMaxCount;


    [Space(10)]
    [Header("Grow states")]
    
    [SerializeField]
    private int baseGrowPoints = 5;
    [SerializeField]
    private Sprite seedSpriteRenderer;

    [SerializeField]
    private int childGrowPointsThreshold;
    [SerializeField]
    private Sprite childSpriteRenderer;

    [SerializeField]
    private int grownGrowPointsThreshold;
    [SerializeField]
    private Sprite grownSpriteRenderer;


    [Space(10)]
    [Header("Plant care properties")]

    [SerializeField]
    private bool needsWater = false;
    [SerializeField]
    private int healthyGrowPointsIncrement = 5;
    [SerializeField]
    private int growPointsPenaltyPerViolation = 5;

    public PlantState CurrentState { get; private set; }
    public int GrowPoints { get; private set; }

    [Space(20)]
    [Header("Service information")]
    public Soil Soil;

    private SpriteRenderer spriteRenderer;

    protected new void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        Debug.Log("I am planted!");
        GrowPoints = baseGrowPoints;
        base.Start();
        StartCoroutine(GrowTimer());
    }

    private void OnDestroy()
    {
        Soil.RemovePlant();
    }

    // For debug
    private IEnumerator GrowTimer()
    {
        while (true)
        {
            yield return new WaitForSeconds(3);

            UpdateGrowPoints();
            Debug.Log($"Updated grow points: {GrowPoints}");
        }
    }

    public void UpdateGrowPoints()
    {
        int countViolated = Conditions().Count(c => !c);
        if (countViolated > 0)
        {
            GrowPoints -= countViolated * growPointsPenaltyPerViolation;
            if (GrowPoints <= 0)
                Die();
        }
        else
            GrowPoints += healthyGrowPointsIncrement;
        
        UpdatePlantState();
    }

    protected virtual IEnumerable<bool> Conditions()
    {
        return new List<bool>() { EnoughWater() };
    }

    protected bool EnoughWater() => needsWater ? Soil.Wet : !Soil.Wet;

    public void Die()
    {
        Debug.Log("Plant dies");
        Soil.RemovePlant();
        Destroy(gameObject);
    }

    private void UpdatePlantState()
    {
        switch (CurrentState)
        {
            case PlantState.Seed:
                if (GrowPoints >= childGrowPointsThreshold)
                    CurrentState = PlantState.Child;
                if (GrowPoints >= grownGrowPointsThreshold)
                    CurrentState = PlantState.Grown;

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

        int count = Random.Range(fruitMinCount, fruitMaxCount + 1);
        int added = inventory.AddItem(fruit, count);
        if (added != count)
        {
            int fruitsToDrop = count - added;
            DropFruits(fruitsToDrop);
        }
        inventory.LogGrid();
        AfterPickUp();
    }

    private void DropFruits(int count)
    {
        for (int i = 0; i < count; i++)
        {
            fruit.Drop(transform.position, transform.rotation);
        }
    }

    protected virtual void AfterPickUp()
    {
        Destroy(gameObject);
    }

    public override void EnterInteractionArea(GameObject interactor)
    {
    }

    public override void LeaveInteractionArea(GameObject interactor)
    {
    }
}
