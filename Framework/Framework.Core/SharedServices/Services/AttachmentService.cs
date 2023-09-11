//using Framework.Core.Data.Repositories;
//using Framework.Core.Extensions;
//using Framework.Core.SharedServices.Entities;
//using Framework.Resources;
//using Microsoft.AspNetCore.Http;
//using Microsoft.EntityFrameworkCore;
//using System;
//using System.Collections.Generic;
//using System.Drawing;
//using System.Drawing.Drawing2D;
//using System.Drawing.Imaging;
//using System.IO;
//using System.Linq;
//using System.Threading.Tasks;

//namespace Framework.Core.SharedServices.Services
//{
//    public class AttachmentService
//    {
//        private readonly IRepositoryBase<ICommonsDbContext, Attachment> _attachmentRepository;
//        private readonly IRepositoryBase<ICommonsDbContext, AttachmentType> _attachmentTypeRepository;
//        private readonly AppSettingsService _appSettingsService;

//        public AttachmentService(IRepositoryBase<ICommonsDbContext, Attachment> attachmentRepository,
//            IRepositoryBase<ICommonsDbContext, AttachmentType> attachmentTypeRepository,
//        AppSettingsService appSettingsService)
//        {
//            _attachmentRepository = attachmentRepository;
//            _attachmentTypeRepository = attachmentTypeRepository;
//            _appSettingsService = appSettingsService;
//        }


//        public ReturnResult<Attachment> AddAttachment(IFormFile file, string title = null, string contentType = null)
//        {
//            var result = new ReturnResult<Attachment>();
//            var attResult = this.AddOrUpdateAttachment(file, AttachmentTypes.GeneralFileAttachment, null, title, contentType);
//            if (!attResult.IsValid)
//            {
//                result.Merge(attResult);
//                return result;
//            }

//            result.Value = attResult.Value;
//            return result;
//        }

//        public ReturnResult<Attachment> AddOrUpdateAttachment(
//            IFormFile file,
//            AttachmentTypes attType,
//            Guid? attachmentId = null,
//            string title = null, string contentType = null)
//        {
//            var result = new ReturnResult<Attachment>();
//            if (file == null)
//            {
//                result.AddErrorItem(string.Empty, SharedResources.FileZeroLengthErrorMessage);
//                return result;
//            }

//            if (file.Length <= 0)
//            {
//                result.AddErrorItem(string.Empty, SharedResources.FileZeroLengthErrorMessage);
//                return result;
//            }

//            if (!_appSettingsService.SaveFilesToDatabase && string.IsNullOrEmpty(_appSettingsService.AttachmentsPath))
//            {
//                throw new Exception(
//                    "File can not be saved. Current Settings is. SaveFileToDatabase=true and Attachment Path is Missing");
//            }

//            var fileBytes = new byte[file.Length];

//            ////file.InputStream.Read(fileBytes, 0, file.Length);
//            var ms = new MemoryStream();
//            file.OpenReadStream().CopyTo(ms);

//            result.Value = this.AddOrUpdateAttachment(
//                file.FileName,
//                contentType ?? file.ContentType,
//                ms.ToArray(),
//                attType,
//                attachmentId,
//                title,
//                title);
//            return result;
//        }

//        public Attachment AddOrUpdateAttachment(
//            string fileName,
//            string contentType,
//            byte[] fileBytes,
//            AttachmentTypes attType,
//            Guid? attachmentId = null,
//            string titleAr = null,
//            string titleEn = null,
//            string descriptionAr = null,
//            string descriptionEn = null,
//            int? itemOrder = null)
//        {
//            var isUpdateFile = attachmentId.HasValue && attachmentId.Value != Guid.Empty;

//            var attachment = isUpdateFile
//                                 ? _attachmentRepository.GetById(attachmentId.Value)
//                                 : new Attachment { Id = Guid.NewGuid().AsSequentialGuid() };

//            if (attachment == null)
//            {
//                throw new Exception("The Attachment File You are trying to update Does Not Exist in the database");
//            }

//            //if (attachment.AttachmentContent == null)
//            //{
//            //    attachment.AttachmentContent = new AttachmentContent();
//            //}

//            //attachment.AttachmentContent.FileContent = fileBytes;

//            attachment.TitleAr = titleAr;
//            attachment.TitleEn = titleEn;
//            attachment.DescriptionAr = descriptionAr ?? attachment.DescriptionAr;
//            attachment.DescriptionEn = descriptionEn ?? attachment.DescriptionEn;
//            attachment.ContentType = contentType;
//            attachment.Extension = new FileInfo(fileName).Extension;
//            attachment.FileName = fileName;
//            var attachmentType = _attachmentTypeRepository.TableNoTracking.FirstOrDefault();

//            attachment.AttachmentTypeId = attachmentType.Id;
//            if (contentType.StartsWith("image/"))
//                attachment.Thumbnail = this.GenerateThumbnail(fileBytes);

//            // in updating delete old file
//            if (isUpdateFile)
//            {
//                this.DeleteAttachmentFromFileSystem(attachment.FilePath);
//            }

//            attachment.FilePath = _appSettingsService.SaveFilesToDatabase
//                                      ? null
//                                      : this.SaveAttachmentToFileSystem(attachment, fileBytes);
//            //attachment.AttachmentContent.Id = attachment.Id;
//            //attachment.AttachmentContent.FileContent =
//            //    _appSettingsService.SaveFilesToDatabase ? fileBytes : null;

//            if (!isUpdateFile)
//            {

//                _attachmentRepository.Insert(attachment, true);
//            }

//            return attachment;
//        }

//        public Attachment GetAttachment(Guid attachmentId)
//        {
//            var attachment = _attachmentRepository
//                .TableNoTracking.FirstOrDefault(at => at.Id == attachmentId);
//            return attachment;
//        }
//        public async Task<Attachment> GetAttachmentForDownloadAsync(Guid? attachmentId)
//        {
//            var attachment = await _attachmentRepository.TableNoTracking.Where(at => at.Id == attachmentId)
//                .AsNoTracking().SingleOrDefaultAsync();

//            //if (_appSettingsService.SaveFilesToDatabase && attachment?.AttachmentContent.FileContent != null)
//            //{
//            //    return attachment;
//            //}
//            if (string.IsNullOrEmpty(_appSettingsService.AttachmentsPath)
//                || string.IsNullOrEmpty(attachment?.FilePath))
//            {
//                return attachment;
//            }

//            var filePath = $"{_appSettingsService.AttachmentsPath}{attachment.FilePath}";
//            if (File.Exists(filePath))
//            {
//                attachment.FileContent = File.ReadAllBytes(filePath);
//            }

//            return attachment;
//        }
//        public Attachment GetAttachmentForDownload(Guid? attachmentId)
//        {
//            var attachment = _attachmentRepository.TableNoTracking.Where(at => at.Id == attachmentId)
//                .AsNoTracking().SingleOrDefault();


//            if (string.IsNullOrEmpty(_appSettingsService.AttachmentsPath)
//                || string.IsNullOrEmpty(attachment?.FilePath))
//            {
//                return attachment;
//            }

//            var filePath = $"{_appSettingsService.AttachmentsPath}{attachment.FilePath}";
//            if (File.Exists(filePath))
//            {
//                attachment.FileContent = File.ReadAllBytes(filePath);
//            }

//            return attachment;
//        }
//        public async Task<byte[]> GetAttachmentIMGThumbnailAsync(Guid? attachmentId)
//        {
//            return await _attachmentRepository.TableNoTracking.Where(at => at.Id == attachmentId).Select(at => at.Thumbnail)
//                 .AsNoTracking().SingleOrDefaultAsync();
//        }
//        public void RemoveRange(List<Guid> deleteIds)
//        {
//            this._attachmentRepository.Delete(a => deleteIds.Contains(a.Id), true);
//        }
//        public async Task<bool> RemoveAsync(Guid id)
//        {
//            return await this._attachmentRepository.DeleteAsync(a => a.Id == id, true);
//        }

//        //get
//        public List<Attachment> GetRange(List<Guid> ids)
//        {
//            return this._attachmentRepository.TableNoTracking.Where(a => ids.Contains(a.Id)).ToList();
//        }


//        public async Task<bool> UpdateAttachmentsTitlesAsync(List<Guid> ids, List<string> titles)
//        {
//            if (ids.IsNullOrEmpty())
//                return false;

//            var attachments = await _attachmentRepository.Table
//                              .Where(a => ids.Contains(a.Id))
//                              .OrderBy(a => a.Id)
//                              .ToListAsync();

//            if (attachments.Count != ids.Count)
//                return false;

//            for (int i = 0; i < ids.Count; i++)
//            {
//                for (int j = 0; j < attachments.Count; j++)
//                {
//                    Attachment attachment = attachments[j];
//                    if (ids[i] == attachment.Id)
//                    {
//                        attachment.TitleEn = titles[i];
//                        await _attachmentRepository.UpdateAsync(attachment, true);
//                    }
//                }
//            }

//            return true;
//        }

//        public void DeleteAttachmentFromFileSystem(string fileRelativePath)
//        {
//            if (string.IsNullOrEmpty(fileRelativePath))
//            {
//                return;
//            }

//            var filePath = $@"{_appSettingsService.AttachmentsPath}{fileRelativePath}";
//            if (File.Exists(filePath))
//            {
//                File.Delete(filePath);
//            }
//        }

//        public async Task DeleteAttachmentFromDbAndFileSystem(Guid attachmentId)
//        {
//            if (attachmentId == Guid.Empty)
//            {
//                return;
//            }
//            var attachment = _attachmentRepository.TableNoTracking.FirstOrDefault(a => a.Id == attachmentId);
//            var filePath = $@"{_appSettingsService.AttachmentsPath}{attachment.FilePath}";
//            if (File.Exists(filePath))
//            {
//                //remove from file system
//                File.Delete(filePath);
//            }
//            //remove from db
//            await RemoveAsync(attachmentId);
//        }

//        private byte[] GenerateThumbnail(byte[] bytes)
//        {
//            using (var ms = new MemoryStream(bytes))
//            {
//                var thumb = new Bitmap(220, 220);
//                using (var bmp = Image.FromStream(ms))
//                {
//                    using (var g = Graphics.FromImage(thumb))
//                    {
//                        g.InterpolationMode = InterpolationMode.HighQualityBicubic;
//                        g.CompositingQuality = CompositingQuality.HighQuality;
//                        g.SmoothingMode = SmoothingMode.HighQuality;
//                        g.DrawImage(bmp, 0, 0, 220, 220);
//                    }
//                }

//                using (var msWrite = new MemoryStream())
//                {
//                    thumb.Save(msWrite, ImageFormat.Png);
//                    return msWrite.ToArray();
//                }
//            }
//        }

//        private string SaveAttachmentToFileSystem(Attachment attach, byte[] fileBytes)
//        {
//            var relativeFolderPath = $"\\{DateTime.Now.Year}\\{DateTime.Now.Month}\\{DateTime.Now.Day}";
//            var fullFolderPath = $"{_appSettingsService.AttachmentsPath}{relativeFolderPath}";
//            var fileName = attach.Id + attach.Extension;
//            var fileRelativePath = $@"{relativeFolderPath}\{fileName}";

//            if (!Directory.Exists(fullFolderPath))
//            {
//                Directory.CreateDirectory(fullFolderPath);
//            }
//            using (var bw = new BinaryWriter(File.Open(Path.Combine(fullFolderPath, fileName), FileMode.OpenOrCreate)))
//            {
//                bw.Write(fileBytes);
//            }
//            return fileRelativePath;
//        }


//        public Attachment Insert(Attachment attachment)
//        {
//            return _attachmentRepository.Insert(attachment, true);
//        }


//    }
//}
