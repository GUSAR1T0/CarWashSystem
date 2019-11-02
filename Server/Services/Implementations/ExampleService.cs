using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using VXDesign.Store.CarWashSystem.Server.DataStorage.Entities;
using VXDesign.Store.CarWashSystem.Server.DataStorage.Operation;
using VXDesign.Store.CarWashSystem.Server.DataStorage.Stores.Interfaces;
using VXDesign.Store.CarWashSystem.Server.Services.Interfaces;

namespace VXDesign.Store.CarWashSystem.Server.Services.Implementations
{
    [Obsolete("It's just an example")]
    public class ExampleService : IExampleService
    {
        private readonly IExampleStore store;

        public ExampleService(IExampleStore store)
        {
            this.store = store;
        }

        public async Task<IEnumerable<ExampleEntity>> GetExample1(IOperation operation) => await store.GetExample1(operation);
    }
}