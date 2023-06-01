using ECommerce.BAL.Contractors;
using ECommerce.BAL.Models.DTOs;
using ECommerce.Common.Constants.Messages;
using ECommerce.Common;
using ECommerce.DAL.Contractors;
using ECommerce.BAL.Models.Requests;
using ECommerce.Common.Constants;
using ECommerce.DAL.DataAccess.Entities;
using ECommerce.Common.Utils;

namespace ECommerce.BAL.Services
{
    public class ProductService : IProductService
    {
        private readonly IUnitOfWork _uow;
        public ProductService(IUnitOfWork uow)
        {
            _uow = uow;
        }

        public async Task<IEnumerable<ProductDTO>> GetProductsAsync()
        {
            var result = await _uow.ProductRepository.GetAllAsync();
            if (result == null)
                throw new Exception(Error.GET_PRDCTS_NULL);

            return result.Select(data => new ProductDTO
            {
                InternalID = data.InternalID,
                Name = data.Name,
                Description = data.Description,
                Image = FileUtil.GetURLFilePath(data.Image),
                Status = data.Status,
                StatusDescription = Parser.ParseStatus(data.Status),
                CreatedDate = data.CreatedDate,
                ModifiedDate = data.ModifiedDate
            });
        }

        public async Task SaveProductAsync(SaveProductRequest request)
        {
            if (request == null)
                throw new Exception(Error.SAVE_PRDCTS_REQUEST_NULL);

            var isAdd = request.inputProduct.InternalID == Guid.Empty;
            request.inputProduct.InternalID = isAdd ? Guid.NewGuid() : request.inputProduct.InternalID;

            //Upload Image
            if (request.formImage != null)
                request.inputProduct.Image = await FileUtil.UploadFileAsync(request.inputProduct.InternalID, FileType.PRODUCT, request.formImage);

            if (isAdd)
                await _uow.ProductRepository.InsertAsync(new Product
                {
                    InternalID = request.inputProduct.InternalID,
                    Name = request.inputProduct.Name,
                    Description = request.inputProduct.Description,
                    Image = request.inputProduct.Image,
                    Status = request.inputProduct.Status,
                    CreatedDate = DateTime.Now,
                    ModifiedDate = null
                });
            else
                await _uow.ProductRepository.UpdateAsync(request.inputProduct.InternalID,
                    new
                    {
                        //request.inputProduct.InternalID,
                        request.inputProduct.Name,
                        request.inputProduct.Description,
                        request.inputProduct.Image,
                        request.inputProduct.Status,
                        //request.inputProduct.CreatedDate,
                        ModifiedDate = DateTime.Now
                    });
            await _uow.SaveAsync();
        }
    }
}
