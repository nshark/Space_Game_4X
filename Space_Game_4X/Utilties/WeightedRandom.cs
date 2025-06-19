using System;
using System.Collections.Generic;

namespace Space_Game_4X;

public class WeightedRandom
{
    private readonly List<(string Item, double CumulativeWeight)> weightedItems = new List<(string, double)>();
    private readonly Random random = new Random();
    private double totalWeight;

    public void AddItem(string item, double weight)
    {
        if (weight <= 0)
            throw new ArgumentException("Weight must be greater than zero.", nameof(weight));

        totalWeight += weight;
        weightedItems.Add((item, totalWeight));
    }

    public string GetRandomItem()
    {
        double r = random.NextDouble() * totalWeight;

        foreach (var weightedItem in weightedItems)
        {
            if (r < weightedItem.CumulativeWeight)
                return weightedItem.Item;
        }

        // This line should technically never be reached
        throw new InvalidOperationException("Failed to select a random item.");
    }
}
