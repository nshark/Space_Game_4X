using System;
using System.DirectoryServices.ActiveDirectory;
using System.IO;
using System.Text.Json;
using System.Threading;
using System.Windows.Forms;

namespace Space_Game_4X.Serialization;

public class SaveLoadManager
{
    private const string DEFAULT_EXTENSION = ".json";
    private const string FILE_FILTER = "JSON files (*.json)|*.json|All files (*.*)|*.*";

    // Save game data using Windows Forms SaveFileDialog
    public static bool SaveGame(GameState gameData)
    {
        bool result = false;
            
        // Create and run on STA thread for Windows Forms compatibility
        Thread staThread = new Thread(() =>
        {
            try
            {
                using (SaveFileDialog saveDialog = new SaveFileDialog())
                {
                    saveDialog.Filter = FILE_FILTER;
                    saveDialog.DefaultExt = DEFAULT_EXTENSION;
                    saveDialog.AddExtension = true;
                    saveDialog.Title = "Save Game";
                    saveDialog.FileName = $"GameSave_{DateTime.Now:yyyyMMdd_HHmmss}";

                    if (saveDialog.ShowDialog() == DialogResult.OK)
                    {
                        // Serialize to JSON
                        string jsonString = JsonSerializer.Serialize(gameData, new JsonSerializerOptions 
                        { 
                            WriteIndented = true 
                        });

                        // Write to file
                        File.WriteAllText(saveDialog.FileName, jsonString);
                        result = true;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error saving game: {ex.Message}", "Save Error", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        });

        staThread.SetApartmentState(ApartmentState.STA);
        staThread.Start();
        staThread.Join(); // Wait for the dialog to complete
            
        return result;
    }

    // Load game data using Windows Forms OpenFileDialog
    public static GameState LoadGame()
    {
        GameState result = null;
            
        // Create and run on STA thread for Windows Forms compatibility
        Thread staThread = new Thread(() =>
        {
            try
            {
                using (OpenFileDialog openDialog = new OpenFileDialog())
                {
                    openDialog.Filter = FILE_FILTER;
                    openDialog.DefaultExt = DEFAULT_EXTENSION;
                    openDialog.Title = "Load Game";
                    openDialog.CheckFileExists = true;

                    if (openDialog.ShowDialog() == DialogResult.OK)
                    {
                        // Read file content
                        string jsonString = File.ReadAllText(openDialog.FileName);

                        // Deserialize from JSON
                        result = JsonSerializer.Deserialize<GameState>(jsonString);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading game: {ex.Message}", "Load Error", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        });

        staThread.SetApartmentState(ApartmentState.STA);
        staThread.Start();
        staThread.Join(); // Wait for the dialog to complete
            
        return result;
    }
    
    public static bool AutoSave(GameState gameData)
    {
        string name = "autosave_" + gameData.TurnCounter;
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
                WriteIndented = true
            };

            // Serialize the game state to JSON
            string jsonString = JsonSerializer.Serialize(gameData, options);

            // Write to file
            File.WriteAllText(filePath, jsonString);

            Console.WriteLine($"Game saved successfully to: {Path.GetFullPath(filePath)}");
            return true;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error saving game: {ex.Message}");
        }

        return false;
    }
}