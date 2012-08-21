using System.IO;

public static class MyExtensions
{
    /// <summary>
    /// Recursively create directory
    /// </summary>
    /// <param name="dirInfo">Folder path to create.</param>
    public static void CreateDirectory(this DirectoryInfo dirInfo)
    {
        if (dirInfo.Parent != null) CreateDirectory(dirInfo.Parent);
        if (!dirInfo.Exists) dirInfo.Create();
    }
}