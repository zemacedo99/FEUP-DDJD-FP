using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;


public static class SaveSystem
{
    private const string FogOfWarDataFilePath = "FogOfWarData.dat";

    public static void SaveFogOfWarData(List<int>  indices, List<float>  alphas)
    {
		BinaryFormatter formatter = new BinaryFormatter();
        string filePath = Path.Combine(Application.persistentDataPath, FogOfWarDataFilePath);
		FileStream stream = new FileStream(filePath,FileMode.Create);

        FogOfWarData data = new FogOfWarData(indices,alphas);

		formatter.Serialize(stream, data);
		stream.Close();
    }

    public static void DeleteFogOfWarData()
    {
        string filePath = Path.Combine(Application.persistentDataPath, FogOfWarDataFilePath);
        if (File.Exists(filePath))
        {
            File.Delete(filePath);
        }
    }

    public static FogOfWarData LoadFogOfWarData()
    {
        string filePath = Path.Combine(Application.persistentDataPath, FogOfWarDataFilePath);

        if (File.Exists(filePath))
        {
            BinaryFormatter formatter = new BinaryFormatter();
			FileStream stream = new FileStream(filePath,FileMode.Open);

			FogOfWarData data = formatter.Deserialize(stream) as FogOfWarData;

			stream.Close();

            return data;
        }
		else
		{
            return null;
		}
    }
}
