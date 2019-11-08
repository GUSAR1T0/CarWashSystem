namespace VXDesign.Store.CarWashSystem.Server.WebAPI.Models
{
    internal interface IEntityToModelConvertible<in TEntity, out TModel> where TEntity : class where TModel : class
    {
        TModel ToModel(TEntity? entity);
    }
}