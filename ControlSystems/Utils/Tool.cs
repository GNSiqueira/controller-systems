using System.Text.Json;

namespace ControlSystems.Utils;

public static class Tool
{
    public static string ObjectJson(object objeto)
    {
        return JsonSerializer.Serialize(objeto, new JsonSerializerOptions { WriteIndented = true });
    }

    public static void PrintObject(object objeto)
    {
        Console.Write(JsonSerializer.Serialize(objeto, new JsonSerializerOptions { WriteIndented = true }));
    }
}
