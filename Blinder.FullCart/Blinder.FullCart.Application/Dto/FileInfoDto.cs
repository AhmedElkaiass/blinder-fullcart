using Blinder.FullCart.Application.Extensions;

namespace Blinder.FullCart.Application.Dto;

public record FileInfoDto
{
    public string Id { get; set; }
    public string Name { get; set; }
    public string RelativePath { get; set; }
    public string Url { get; set; }
    public string Extention { get; set; }
    public long SizeInBytes { get; set; }
    public bool IsPresisted { get; set; }
    public string UploadedByUserId { get; set; }
    public long SizeInMigaByte
    {
        get
        {
            if (SizeInBytes == 0)
                return 0;
            else
                return SizeInBytes / (1024 * 2);
        }
    }
    public List<byte> FileBytes { get; set; }
    public string ContentType => Name.GetContentType();
}
