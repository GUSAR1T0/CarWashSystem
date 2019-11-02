using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using VXDesign.Store.CarWashSystem.Server.DataStorage.Entities;
using VXDesign.Store.CarWashSystem.Server.DataStorage.Operation;

namespace VXDesign.Store.CarWashSystem.Server.DataStorage.Stores.Interfaces
{
    [Obsolete("It's just an example")]
    public interface IExampleStore
    {
        Task<IEnumerable<ExampleEntity>> GetExample1(IOperation operation);
    }
}