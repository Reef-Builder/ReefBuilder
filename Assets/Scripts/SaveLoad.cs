using UnityEngine;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using System;
using System.Collections.Generic;
using System.Linq;

public static class SaveLoad
{

    public static void Save(int game)
    {
        // Save the current game as serializable format in the provided slot
        var save = GameScript.Instance.Serialize();
        var bf = new BinaryFormatter();
        var file = File.Create(Application.persistentDataPath + $"/savedGame{game}.gd");
        // Perform the serialization
        bf.Serialize(file, save);
        file.Close();

        //Now take a screenshot of using the special screenshot camera
    }

    public static void Load(int game)
    {
        if (File.Exists(Application.persistentDataPath + $"/savedGame{game}.gd"))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + $"/savedGame{game}.gd", FileMode.Open);
            var save = (GameData)bf.Deserialize(file);
            GameScript.Instance.Deserialize(save);
            file.Close();
        }
    }

    public static bool SaveExists(int game)
    {
        return File.Exists(Application.persistentDataPath + $"/savedGame{game}.gd");
    }

    public static Texture2D[] LoadSaveGameClip(int game)
    {
        var filePaths = Directory.GetFiles(Application.persistentDataPath);

        var clipFilePaths = filePaths.Where(filePath => filePath.Contains($"{game}_image_")).ToArray();

        var clipFilePathIds = clipFilePaths.Select(filePath => Convert.ToInt32(filePath.Substring(filePath.IndexOf("_image_") + "_image_".Length))).ToArray();

        var textures = new Texture2D[clipFilePaths.Length];

        for (var i = 0; i < clipFilePaths.Length; i++)
        {
            var fileData = File.ReadAllBytes(clipFilePaths[i]);
            var texture = new Texture2D(4, 4);
            texture.LoadImage(fileData);

            textures[(int)clipFilePathIds[i]] = texture;
        }

        return textures;
    }

    internal static void SaveImage(Texture2D image, int index)
    {
        var bytes = image.EncodeToPNG();
        var filename = Application.persistentDataPath + $"/{PersistentSettings.Instance.SaveGameId}_image_{index}";
        System.IO.File.WriteAllBytes(filename, bytes);
    }

    internal static void SaveClip(List<Texture2D> lastTenSeconds)
    {
        var i = 0;
        foreach (var image in lastTenSeconds)
        {
            var bytes = image.EncodeToPNG();
            var filename = Application.persistentDataPath + $"/{PersistentSettings.Instance.SaveGameId}_image_{i}";
            System.IO.File.WriteAllBytes(filename, bytes);
            i++;
        }
    }
}
