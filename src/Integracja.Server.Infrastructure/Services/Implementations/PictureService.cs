using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Integracja.Server.Infrastructure.Data;
using Integracja.Server.Infrastructure.Enums;
using Integracja.Server.Infrastructure.Exceptions;
using Integracja.Server.Infrastructure.Services.Interfaces;
using Integracja.Server.Infrastructure.Settings;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Formats;
using SixLabors.ImageSharp.Formats.Bmp;
using SixLabors.ImageSharp.Formats.Gif;
using SixLabors.ImageSharp.Formats.Jpeg;
using SixLabors.ImageSharp.Formats.Png;
using SixLabors.ImageSharp.Formats.Tga;
using SixLabors.ImageSharp.Processing;

namespace Integracja.Server.Infrastructure.Services.Implementations
{
    public class PictureService : IPictureService
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly IStorageService _fileService;
        private readonly DefaultSettings _defaultSettings;
        private readonly PictureSettings _settings;
        private readonly string _contentType;
        private readonly IImageEncoder _imageEncoder;

        public static readonly string[] ValidMimeTypes =
        {
            "image/bmp",
            "image/x-windows-bmp",
            "image/gif",
            "image/jpeg",
            "image/pjpeg",
            "image/png",
            "image/tga",
            "image/x-tga",
            "image/x-targa"
       };

        public static readonly string[] ValidFileExtensions =
        {
            ".bm",
            ".bmp",
            ".dip",
            ".gif",
            ".jpg",
            ".jpeg",
            ".jfif",
            ".png",
            ".tga",
            ".vda",
            ".icb",
            ".vst",
        };

        public PictureService(ApplicationDbContext dbContext, IOptions<PictureSettings> options, IStorageService fileService, IOptions<DefaultSettings> defaultOptions)
        {
            _dbContext = dbContext;
            _fileService = fileService;
            _settings = options.Value;
            _defaultSettings = defaultOptions.Value;
            (_contentType, _imageEncoder) = Init(_settings);
        }

        public async Task<string> Save(IFormFile formFile, int userId)
        {
            if (formFile.Length > _settings.MaxSize)
            {
                throw new PayloadTooLargeException();
            }

            if (!ValidateContentType(formFile.ContentType) || !ValidateExtension(formFile.FileName))
            {
                throw new UnsupportedMediaTypeException();
            }

            var userEntity = await _dbContext.Users
                .FirstOrDefaultAsync(u => u.Id == userId && !u.IsDeleted);

            if (userEntity == null)
            {
                throw new UnauthorizedException();
            }

            using var picture = new MemoryStream();
            using var thumbnail = new MemoryStream();

            try
            {
                Resize(picture, formFile, _settings.PictureWidth, _settings.PictureHeight);
                Resize(thumbnail, formFile, _settings.ThumbnailWidth, _settings.ThumbnailHeight);
            }
            catch (Exception ex)
            {
                throw new UnprocessableEntityException(innerException: ex);
            }

            userEntity.ProfilePicture = (await _fileService.AddOrUpdate(picture, _contentType, GetFileName(ImageType.ProfilePicture, userId))).AbsoluteUri;
            userEntity.ProfileThumbnail = (await _fileService.AddOrUpdate(thumbnail, _contentType, GetFileName(ImageType.ProfileThumbnail, userId))).AbsoluteUri;

            await _dbContext.SaveChangesAsync();

            return userEntity.ProfilePicture;
        }

        public async Task Delete(int userId)
        {
            var userEntity = await _dbContext.Users
                .FirstOrDefaultAsync(u => u.Id == userId && !u.IsDeleted);

            if (userEntity == null)
            {
                throw new UnauthorizedException();
            }

            if (userEntity.ProfilePicture != null && userEntity.ProfilePicture != _defaultSettings.ProfilePicture)
            {
                await _fileService.Delete(GetFileName(ImageType.ProfilePicture, userId));
            }

            if (userEntity.ProfileThumbnail != null && userEntity.ProfileThumbnail != _defaultSettings.ProfileThumbnail)
            {
                await _fileService.Delete(GetFileName(ImageType.ProfileThumbnail, userId));
            }

            userEntity.ProfilePicture = _defaultSettings.ProfilePicture;
            userEntity.ProfileThumbnail = _defaultSettings.ProfileThumbnail;

            await _dbContext.SaveChangesAsync();
        }

        private void Resize(Stream output, IFormFile formFile, int width, int height)
        {
            using var input = formFile.OpenReadStream();
            using var image = Image.Load(input);

            image.Metadata.ExifProfile = null;
            image.Mutate(x => x.Resize(width, height));
            image.Save(output, _imageEncoder);

            output.Position = 0;
        }

        private static (string, IImageEncoder) Init(PictureSettings settings)
        {
            return settings.Format switch
            {
                ImageFormat.Bmp => ("image/bmp", new BmpEncoder()),
                ImageFormat.Gif => ("image/gif", new GifEncoder()),
                ImageFormat.Jpeg => ("image/jpeg", new JpegEncoder()),
                ImageFormat.Png => ("image/png", new PngEncoder()),
                ImageFormat.Tga => ("image/tga", new TgaEncoder()),
                _ => throw new NotImplementedException(),
            };
        }

        private static string GetFileName(ImageType imageType, int userId)
        {
            return $"{userId}-{imageType}";
        }

        private static bool ValidateContentType(string contentType)
        {
            return ValidMimeTypes.Contains(contentType.ToLower());
        }

        private static bool ValidateExtension(string fileName)
        {
            return ValidFileExtensions.Contains(Path.GetExtension(fileName).ToLower());
        }
    }
}
