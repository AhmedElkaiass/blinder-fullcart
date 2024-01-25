using Blinder.FullCart.Application.Dto;
using Microsoft.AspNetCore.Http;

namespace Blinder.FullCart.Application.Services;

public interface IAppFileService
{
    Task<ServiceResponse<List<FileInfoDto>>> UploadFiles(IFormFileCollection files);
    Task<ServiceResponse<FileInfoDto>> UploadFile(IFormFile files);
    ServiceResponse<FileInfoDto> GetFileInfo(string Id);
    ServiceResponse<object> MarkFileAsPresisted(string Id);
    ServiceResponse<object> MarkFileAsPresisted(List<string> Ids);
    ServiceResponse<object> DeleteFile(string Id);
    ServiceResponse<object> DeleteFiles(List<string> Ids);
    string GetFileUrlByID(string Id);
    FileInfoDto ReadFileInfoBytes(string FileID);
    List<FileInfoDto> ReadFileInfoBytes(List<string> Ids);
}
