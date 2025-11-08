using Catalog.Application.DTOs;
using Catalog.Application.Interfaces;
using Catalog.Domain.Entities;
using Catalog.Domain.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Catalog.Application.Services
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _repository;

        public ProductService(IProductRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<ProductDto>> GetAllAsync()
        {
            var products = await _repository.GetAllAsync();

            return products.Select(p => new ProductDto
            {
                Id = p.Id,
                Name = p.Name,
                Description = p.Description,
                Price = p.Price,
                Stock = p.Stock
            });
        }

        public async Task<ProductDto?> GetByIdAsync(Guid id)
        {
            var p = await _repository.GetByIdAsync(id);
            if (p == null) return null;

            return new ProductDto
            {
                Id = p.Id,
                Name = p.Name,
                Description = p.Description,
                Price = p.Price,
                Stock = p.Stock
            };
        }

        public async Task AddAsync(CreateProductDto dto)
        {
            // 1) Validaciones básicas del DTO (reglas de negocio)
            if (dto.Price < 0)
                throw new InvalidProductException("Price cannot be negative.");

            if (string.IsNullOrWhiteSpace(dto.Name))
                throw new InvalidProductException("Name cannot be empty.");

            // 2) Verificar duplicados ANTES de crear/guardar
            var exists = await _repository.ExistsByNameAsync(dto.Name);
            if (exists)
                throw new InvalidOperationException($"Ya existe un producto con el nombre '{dto.Name}'.");

            // 3) Crear la entidad usando el constructor del dominio (encapsulado)
            var product = new Product(dto.Name, dto.Description, dto.Price, dto.Stock);

            // 4) Persistir la entidad
            await _repository.AddAsync(product);
        }



        public async Task UpdateAsync(Guid id, CreateProductDto dto)
        {
            var existing = await _repository.GetByIdAsync(id);
            if (existing == null) return;

            existing.UpdateDetails(dto.Name, dto.Description, dto.Price, dto.Stock);

            await _repository.UpdateAsync(existing);
        }


        public async Task DeleteAsync(Guid id)
        {
            await _repository.DeleteAsync(id);
        }
    }
}
