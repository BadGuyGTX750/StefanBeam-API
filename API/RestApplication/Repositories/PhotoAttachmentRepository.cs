using Microsoft.EntityFrameworkCore;
using RestApplication.Data;
using RestApplication.Models.Attachment;
using RestApplication.Models.Product;

namespace RestApplication.Repositories
{
    public class PhotoAttachmentRepository
    {
        private readonly Entities dbContext;


        public PhotoAttachmentRepository(Entities dbContext)
        {
            this.dbContext = dbContext;
        }


        public async Task<bool> AddPhotoAttachment(PhotoAttachmentModel photo)
        {
            try
            {
                await dbContext.photoAttachments.AddAsync(photo);
                await dbContext.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }


        public async Task<PhotoAttachmentModel> GetPhotoAttachmentById(Guid id)
        {
            try
            {
                return await dbContext.photoAttachments.Where(u => u.id == id).FirstAsync();
            }
            catch (Exception ex)
            {
                return null;
            }
        }


        public async Task<PhotoAttachmentModel> GetPhotoAttachmentByName(string name)
        {
            try
            {
                return await dbContext.photoAttachments.Where(u => u.name == name).FirstAsync();
            }
            catch (Exception ex)
            {
                return null;
            } 
        }


        public async Task<PhotoAttachmentModel> GetPhotoProductName(string name)
        {
            try
            {
                return await dbContext.photoAttachments.Where(u => u.productName == name).FirstAsync();
            }
            catch (Exception ex)
            {
                return null;
            }
        }


        public async Task<PhotoAttachmentModel> GetPhotoByProductName(string name)
        {
            try
            {
                return await dbContext.photoAttachments.Where(u => u.productName == name).FirstAsync();
            }
            catch (Exception ex)
            {
                return null;
            }
        }


        public async Task<bool> UpdatePhotoAttachment(PhotoAttachmentModel photo)
        {
            try
            {
                dbContext.photoAttachments.Update(photo);
                await dbContext.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }


        public async Task<bool> DeletePhotoAttachmentById(Guid id)
        {
            try
            {
                var photoAttachment = dbContext.photoAttachments.Where(u => u.id == id).FirstOrDefault();
                dbContext.photoAttachments.Remove(photoAttachment);
                await dbContext.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public async Task<bool> DeletePhotoAttachmentByName(string name)
        {
            try
            {
                var photoAttachment = dbContext.photoAttachments.Where(u => u.name == name).FirstOrDefault();
                dbContext.photoAttachments.Remove(photoAttachment);
                await dbContext.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}
