using System;
using VXDesign.Store.CarWashSystem.Server.DataStorage.Entities;

namespace VXDesign.Store.CarWashSystem.Server.WebAPI.Models
{
    [Obsolete("It's just an example")]
    public class ExampleEntityModel
    {
        public int X { get; set; }
        public int Y { get; set; }
    }

    [Obsolete("It's just an example")]
    internal static class ExampleEntityModelExtensions
    {
        internal static ExampleEntityModel ToModel(this ExampleEntity entity) => new ExampleEntityModel
        {
            X = entity.X,
            Y = entity.Y
        };
    }
}