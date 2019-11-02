using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using VXDesign.Store.CarWashSystem.Server.DataStorage.Entities;
using VXDesign.Store.CarWashSystem.Server.DataStorage.Operation;

namespace VXDesign.Store.CarWashSystem.Server.Services.Interfaces
{
    [Obsolete("It's just an example")]
    public interface IExampleService
    {
        Task<IEnumerable<ExampleEntity>> GetExample1(IOperation operation);
    }
}