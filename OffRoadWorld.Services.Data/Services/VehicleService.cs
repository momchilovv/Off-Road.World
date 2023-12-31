﻿using Microsoft.EntityFrameworkCore;
using OffRoadWorld.Data;
using OffRoadWorld.Data.Models;
using OffRoadWorld.Services.Data.Contracts;
using OffRoadWorld.Web.ViewModels.Category;
using OffRoadWorld.Web.ViewModels.Vehicle;

namespace OffRoadWorld.Services.Data.Services
{
    public class VehicleService : IVehicleService
    {
        private readonly OffRoadWorldDbContext dbContext;

        public VehicleService(OffRoadWorldDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task AddVehicleAsync(VehicleFormModel model)
        {
            var vehicle = new Vehicle()
            {
                Id = new Guid(),
                Make = model.Make,
                Model = model.Model,
                ProductionYear = model.ProductionYear,
                Price = model.Price,
                ImageUrl = model.ImageUrl,
                CategoryId = model.CategoryId,
                HorsePower = model.HorsePower,
                EngineCapacity = model.EngineCapacity,
                OwnerId = model.OwnerId
            };

            await dbContext.Vehicles.AddAsync(vehicle);
            await dbContext.SaveChangesAsync();
        }

        public async Task<ICollection<CategoryModel>> GetAllCategoriesAsync()
        {
            return await dbContext.Categories
                .Select(c => new CategoryModel()
                {
                    Id = c.Id,
                    Name = c.Name
                })
                .ToListAsync();
        }

        public async Task<ICollection<VehicleViewModel>> GetMyVehiclesAsync(string userId)
        {
            return await dbContext.Vehicles
                .Where(v => v.OwnerId == userId)
                .Select(v => new VehicleViewModel()
                {
                    Id = v.Id,
                    Make = v.Make,
                    Model = v.Model,
                    ProductionYear = v.ProductionYear,
                    HorsePower = v.HorsePower,
                    EngineCapacity = v.EngineCapacity,
                    Price = v.Price,
                    Category = v.Category.Name
                })
                .ToListAsync();
        }
    }
}