using System;
using VXDesign.Store.CarWashSystem.Server.Core.Common;
using VXDesign.Store.CarWashSystem.Server.DataStorage.Entities;

namespace VXDesign.Store.CarWashSystem.Server.WebAPI.Models.Lookup
{
    public class EnumRowModel : IEntityToModelConvertible<EnumRowEntity, EnumRowModel>
    {
        public int Id { get; set; }
        public string Name { get; set; } = "";

        public EnumRowModel ToModel(EnumRowEntity? entity)
        {
            if (entity == null) throw new Exception(ExceptionMessage.EmptyResponse);

            Id = entity.Id;
            Name = entity.Name;
            return this;
        }
    }
}