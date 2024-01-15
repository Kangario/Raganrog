
using UnityEngine;
using System.IO;
public class RWorld : MonoBehaviour
{
    public string nameWorld;
    public Generate_Landscape generateLandscape;
    public void SaveWorld()
    {   
        nameWorld = GameObject.Find("TransferData").GetComponent<DataTransfer>().nameWorld;
        if (GameObject.Find("GeneratorLandscape") != null)
        {


            Generate_Landscape generator = GameObject.Find("GeneratorLandscape").GetComponent<Generate_Landscape>();

            // ������� ������ ��� ������������
            LandscapeData[] dataArray;
            dataArray = generator.landscapeData.ToArray(); // �����������, ��� landscapeData - ��� List<LandscapeData>


            // ����������� � JSON
            string json = "{\r\n  \"data\":[";
            foreach (LandscapeData dt in dataArray)
            {
                json += JsonUtility.ToJson(dt);
                json += ",\n";

            }
            json = json.Remove(json.Length - 2);
            json += "]\r\n}";
                // ��������� � ����
            string nameWorld = GameObject.Find("TransferData").GetComponent<DataTransfer>().nameWorld;
            File.WriteAllText("Assets/Worlds/" + nameWorld + ".json", json);
            Debug.Log("World saved to worldData.json");
        }
        else
        {
            Debug.Log("Dont have world!");
        }
    }

   
}
