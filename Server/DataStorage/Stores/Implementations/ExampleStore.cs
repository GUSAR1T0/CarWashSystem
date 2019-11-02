using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using VXDesign.Store.CarWashSystem.Server.DataStorage.Entities;
using VXDesign.Store.CarWashSystem.Server.DataStorage.Operation;
using VXDesign.Store.CarWashSystem.Server.DataStorage.Stores.Interfaces;

namespace VXDesign.Store.CarWashSystem.Server.DataStorage.Stores.Implementations
{
    [Obsolete("It's just an example")]
    public class ExampleStore : IExampleStore
    {
        public async Task<IEnumerable<ExampleEntity>> GetExample1(IOperation operation)
        {
            return await operation.QueryAsync<ExampleEntity>(@"SELECT * FROM [example].[Example1]");
        }
    }
}