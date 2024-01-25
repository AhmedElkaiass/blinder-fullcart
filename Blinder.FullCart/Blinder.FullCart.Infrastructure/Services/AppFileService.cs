using Blinder.FullCart.Application.Dto;
using Blinder.FullCart.Application.Services;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
namespace Blinder.FullCart.Infrastructure.Services;

public class AppFileService
{
    public const string FilesStorageLocation = "AppFileStorage";
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly IHostingEnvironment _environment;
    private readonly IUserSessionService _sessionService;

    public AppFileService(IHostingEnvironment environment,
                                   IUserSessionService sessionService,
                                   IHttpContextAccessor httpContextAccessor)
    {
        _environment = environment;
        _httpContextAccessor = httpContextAccessor;
        _sessionService = sessionService;
    }
    public ServiceResponse<object> DeleteFile(string Id)
    {
        string ServerAbsolutePath = _environment.WebRootPath;
        var StorageFolder = Path.Combine(ServerAbsolutePath, FilesStorageLocation, Id);
        if (Directory.Exists(StorageFolder))
            Directory.Delete(StorageFolder, recursive: true);
        return ServiceResponse<object>.Success(null, "File Deleted successfully");
    }

    public ServiceResponse<object> DeleteFiles(List<string> Ids)
    {
        foreach (var fileId in Ids)
            DeleteFile(fileId);
        return ServiceResponse<object>.Success(null, "files deleted successfully");
    }

    public ServiceResponse<FileInfoDto> GetFileInfo(string Id)
    {
        var info = ReadFileInfo(Id);
        if (info != null)
            return ServiceResponse<FileInfoDto>.Success(info);
        else
            return ServiceResponse<FileInfoDto>.Error("file not found");

    }

    public ServiceResponse<object> MarkFileAsPresisted(string Id)
    {
        var fileInfo = ReadFileInfo(Id);
        if (fileInfo == null)
            return ServiceResponse<object>.Error("file not found");

        fileInfo.IsPresisted = true;
        SaveFileInfo(fileInfo);
        return ServiceResponse<object>.Success(null, "file not found");
    }
    public string GetFileUrlByID(string Id)
    {
        var fileInfo = ReadFileInfo(Id);
        if (fileInfo == null)
            return "";
        string url = $"{_httpContextAccessor.HttpContext.Request.Scheme}://{_httpContextAccessor.HttpContext.Request.Host}/{fileInfo.RelativePath}";
        url = url.Replace("\\", "/");
        return url;
    }
    public ServiceResponse<object> MarkFileAsPresisted(List<string> Ids)
    {
        foreach (var fileID in Ids)
            MarkFileAsPresisted(fileID);
        return ServiceResponse<object>.Error("file not found");
    }

    public async Task<ServiceResponse<FileInfoDto>> UploadFile(IFormFile file)
    {
        string ServerAbsolutePath = _environment.WebRootPath;
        FileInfoDto UploadedFileInfo = new();
        UploadedFileInfo.Extention = Path.GetExtension(file.FileName);
        UploadedFileInfo.Id = Guid.NewGuid().ToString();
        UploadedFileInfo.Name = file.FileName;
        UploadedFileInfo.SizeInBytes = file.Length;
        UploadedFileInfo.UploadedByUserId = _sessionService.Id.ToString();
        UploadedFileInfo.IsPresisted = false;
        string SavedFileName = $"{UploadedFileInfo.Id}{UploadedFileInfo.Extention}";
        UploadedFileInfo.RelativePath = Path.Combine(FilesStorageLocation, UploadedFileInfo.Id, SavedFileName);
        UploadedFileInfo.Url = GetFileUrlByRelativePath(UploadedFileInfo.RelativePath);


        var FoldMagicStoreath = Path.Combine(ServerAbsolutePath, FilesStorageLocation, UploadedFileInfo.Id);
        Directory.CreateDirectory(FoldMagicStoreath);
        var PhysicalFilePath = Path.Combine(FoldMagicStoreath, SavedFileName);


        using (var stream = new FileStream(PhysicalFilePath, FileMode.Create))
        {
            await file.CopyToAsync(stream);
            stream.Flush();
            stream.Close();
            stream.Dispose();
        }
        SaveFileInfo(UploadedFileInfo);
        return ServiceResponse<FileInfoDto>.Success(UploadedFileInfo);
    }

    public async Task<ServiceResponse<List<FileInfoDto>>> UploadFiles(IFormFileCollection files)
    {
        List<FileInfoDto> data = new();
        if (files == null || files.Count == 0)
            return ServiceResponse<List<FileInfoDto>>.Error("No files to be uploaded");
        foreach (var file in files)
        {
            var res = await UploadFile(file);
            data.Add(res.ResponseData);
        }
        return ServiceResponse<List<FileInfoDto>>.Success(data);
    }
    private string GetFileUrlByRelativePath(string RelativePath)
    {
        if (RelativePath == null)
            return "";
        return $"{_httpContextAccessor.HttpContext.Request.Scheme}://{_httpContextAccessor.HttpContext.Request.Host}/{RelativePath}";
    }
    public FileInfoDto ReadFileInfoBytes(string FileID)
    {
        if (string.IsNullOrWhiteSpace(FileID))
        {
            return null;
        }
        else
        {
            string ServerAbsolutePath = _environment.WebRootPath;
            var StorageFolder = Path.Combine(ServerAbsolutePath, FilesStorageLocation, FileID);
            var FileInfoFilePath = Path.Combine(StorageFolder, "FileInfo");
            if (!Directory.Exists(StorageFolder) || !File.Exists(FileInfoFilePath))
                return null;
            FileInfoDto FileInfo = new();
            string FileInfoString = File.ReadAllText(Path.Combine(StorageFolder, "FileInfo"));
            FileInfo = JsonConvert.DeserializeObject<FileInfoDto>(FileInfoString);
            string filePath = Path.Combine(StorageFolder, FileInfo.Id + FileInfo.Extention);
            FileInfo.FileBytes = GetBytes(filePath);
            FileInfo.Url = $"{_httpContextAccessor.HttpContext.Request.Scheme}://{_httpContextAccessor.HttpContext.Request.Host}/{FileInfo.RelativePath}";
            return FileInfo;
        }
    }
    public List<FileInfoDto> ReadFileInfoBytes(List<string> Ids) => Ids.Select(x => ReadFileInfoBytes(x)).ToList();

    FileInfoDto ReadFileInfo(string FileID)
    {
        if (string.IsNullOrWhiteSpace(FileID))
        {
            return null;
        }
        else
        {
            string ServerAbsolutePath = _environment.WebRootPath;
            var StorageFolder = Path.Combine(ServerAbsolutePath, FilesStorageLocation, FileID);
            var FileInfoFilePath = Path.Combine(StorageFolder, "FileInfo");
            if (!Directory.Exists(StorageFolder) || !File.Exists(FileInfoFilePath))
                return null;
            FileInfoDto FileInfo = new();
            string FileInfoString = File.ReadAllText(Path.Combine(StorageFolder, "FileInfo"));
            FileInfo = JsonConvert.DeserializeObject<FileInfoDto>(FileInfoString);
            string filePath = Path.Combine(StorageFolder, FileInfo.Id + FileInfo.Extention);
            //FileInfo.FileBytes = GetBytes(filePath);
            return FileInfo;
        }

    }
    private void SaveFileInfo(FileInfoDto fileInfo)
    {
        string ServerAbsolutePath = _environment.WebRootPath;
        var StorageFolder = Path.Combine(ServerAbsolutePath, FilesStorageLocation, fileInfo.Id);
        Directory.CreateDirectory(StorageFolder);
        var FileInfoFilePath = Path.Combine(StorageFolder, "FileInfo");
        var serializedFileInfo = JsonConvert.SerializeObject(fileInfo);
        File.WriteAllText(FileInfoFilePath, serializedFileInfo);
    }
    private List<byte> GetBytes(string filePath)
    {
        using (FileStream fileStream = new FileStream(filePath, FileMode.Open, FileAccess.Read))
        {
            byte[] fileBytes = new byte[fileStream.Length];
            fileStream.Read(fileBytes, 0, fileBytes.Length);
            fileStream.Close();
            return fileBytes.ToList();
        }
    }

}
