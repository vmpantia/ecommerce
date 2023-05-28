﻿using ECommerce.BAL.Contractors;
using ECommerce.BAL.Models.DTOs;
using ECommerce.Common.Constants.Messages;
using ECommerce.Common;
using ECommerce.DAL.Contractors;
using ECommerce.BAL.Models.Requests;
using ECommerce.Common.Constants;
using ECommerce.DAL.DataAccess.Entities;

namespace ECommerce.BAL.Services
{
    public class ProductService : IProductService
    {
        private readonly IUnitOfWork _uow;
        private readonly IFileService _file;
        public ProductService(IUnitOfWork uow, IFileService file)
        {
            _uow = uow;
            _file = file;
        }

        public async Task<IEnumerable<ProductDTO>> GetProductsAsync()
        {
            var result = await _uow.ProductRepository.GetAllAsync();
            if (result == null)
                throw new Exception(ErrorMessage.GET_PRODUCTS);

            return result.Select(data => new ProductDTO
            {
                InternalID = data.InternalID,
                Name = data.Name,
                Description = data.Description,
                Image = new FileDTO
                {
                    FileName = data.Image,
                    UrlFilePath = _file.GetURLFilePath(data.Image),
                },
                Status = data.Status,
                StatusDescription = Parser.ParseStatus(data.Status),
                CreatedDate = data.CreatedDate,
                ModifiedDate = data.ModifiedDate
            });
        }

        public async Task SaveProductAsync(SaveProductRequest request)
        {
            if (request == null)
                throw new Exception(ErrorMessage.SAVE_PRODUCT_REQUEST_EMPTY);

            var isAdd = request.inputProduct.InternalID == Guid.Empty;
            request.inputProduct.InternalID = isAdd ? Guid.NewGuid() : request.inputProduct.InternalID;

            //Upload Image
            if (request.inputProduct.Image?.File != null)
                request.inputProduct.Image.FileName = await _file.UploadFileAsync(request.inputProduct.InternalID, FileType.PRODUCT, request.inputProduct.Image.File);

            if (isAdd)
                await _uow.ProductRepository.InsertAsync(new Product
                {
                    InternalID = request.inputProduct.InternalID,
                    Name = request.inputProduct.Name,
                    Description = request.inputProduct.Description,
                    Image = request.inputProduct.Image?.FileName,
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
                        Image = request.inputProduct.Image?.FileName,
                        request.inputProduct.Status,
                        request.inputProduct.CreatedDate,
                        ModifiedDate = DateTime.Now
                    });
            await _uow.SaveAsync();
        }
    }
}