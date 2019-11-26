namespace VXDesign.Store.CarWashSystem.Server.WebAPI.Models
{
    internal interface IModelToEntityConvertible<out TEntity> where TEntity : class
    {
        TEntity ToEntity();
    }
}