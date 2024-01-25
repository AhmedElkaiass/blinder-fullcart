
using Microsoft.AspNetCore.StaticFiles;

namespace Blinder.FullCart.Application.Extensions;
public static class FileExtensions
{
    public static string GetFileType(string fileName)
    {
        // Get the extension from the file name
        string extension = Path.GetExtension(fileName);

        // Remove the leading dot from the extension
        if (!string.IsNullOrEmpty(extension) && extension[0] == '.')
        {
            extension = extension.Substring(1);
        }

        return extension;
    }

    public static List<byte> GetFileAsBytes(this string path)
    {
        // Open the file.
        using (FileStream fileStream = new FileStream(path, FileMode.Open, FileAccess.Read))
        {
            // Create a byte array to store the file bytes.
            byte[] fileBytes = new byte[fileStream.Length];
            fileStream.Read(fileBytes, 0, fileBytes.Length);
            fileStream.Close();
            // Close the file.
            return fileBytes.ToList();
        }
    }
    public static string GetContentType(this string fileName)
    {
        var provider = new FileExtensionContentTypeProvider();
        if (!provider.TryGetContentType(fileName, out var contentType))
            contentType = "application/octet-stream";
        return contentType;
    }
}