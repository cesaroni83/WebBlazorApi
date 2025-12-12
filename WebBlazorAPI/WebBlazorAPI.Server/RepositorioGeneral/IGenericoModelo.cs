using System.Linq.Expressions;

namespace WebBlazorAPI.Server.RepositorioGeneral
{
    public interface IGenericoModelo<Tmodelo> where Tmodelo : class
    {
        IQueryable<Tmodelo> GetAllWithWhere(Expression<Func<Tmodelo, bool>>? filtro = null);
        Task<Tmodelo> CreateReg(Tmodelo modelo);
        Task<bool> Upadate(Tmodelo modelo);
        Task<bool> Delete(Tmodelo modelo);
        IQueryable<Tmodelo> GetAll();
    }
}
