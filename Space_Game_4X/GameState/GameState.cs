using System;
using System.IO;
using System.Text.Json;

namespace Space_Game_4X;

public class GameState
{
    public Star[] Stars { get; set; }

    public int TurnCounter { get; set; }

    public void AutoSave()
    {
        string name = "autosave_" + TurnCounter;
        try
        {
            // Create the save directory if it doesn't exist
            string saveDirectory = "saves";
            if (!Directory.Exists(saveDirectory))
            {
                Directory.CreateDirectory(saveDirectory);
            }

            // Create the full file path
            string fileName = $"{name}.json";
            string filePath = Path.Combine(saveDirectory, fileName);

            // Configure JSON serialization options for better formatting
            var options = new JsonSerializerOptions
            {
                WriteIndented = true,
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            };

            // Serialize the game state to JSON
            string jsonString = JsonSerializer.Serialize(this, options);

            // Write to file
            File.WriteAllText(filePath, jsonString);

            Console.WriteLine($"Game saved successfully to: {Path.GetFullPath(filePath)}");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error saving game: {ex.Message}");
            throw;
        }
    }
}