using Microsoft.EntityFrameworkCore;
using RestApplication.Models.Attachment;
using RestApplication.Models.Product;
using RestApplication.Repositories;

namespace RestApplication.Services
{
    public class PhotoAttachmentService
    {
        private readonly PhotoAttachmentRepository repository;


        public PhotoAttachmentService(PhotoAttachmentRepository repository)
        {
            this.repository = repository;
        }



        public async Task<bool> AddPhotoAttachment(PhotoAttachmentModel photo)
        {
            return await repository.AddPhotoAttachment(photo);
        }


        public async Task<PhotoAttachmentModel> GetPhotoAttachmentById(Guid id)
        {
            return await repository.GetPhotoAttachmentById(id);
        }


        public async Task<PhotoAttachmentModel> GetPhotoAttachmentByName(string name)
        {
            return await repository.GetPhotoAttachmentByName(name);
        }


        public async Task<List<PhotoAttachmentModel>> GetAllByProductName(string name)
        {
            return await repository.GetAllByProductName(name);
        }


        public async Task<bool> UpdatePhotoAttachment(PhotoAttachmentModel photo)
        {
            return await repository.UpdatePhotoAttachment(photo);
        }


        public async Task<bool> DeletePhotoAttachmentById(Guid id)
        {
            return await repository.DeletePhotoAttachmentById(id);
        }

        public async Task<bool> DeletePhotoAttachmentByName(string name)
        {
            return await repository.DeletePhotoAttachmentByName(name);
        }
    }
}
