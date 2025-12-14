using AutoMapper;
using Microsoft.EntityFrameworkCore;
using WebBlazorAPI.Server.Data;
using WebBlazorAPI.Server.RepositorioGeneral;
using WebBlazorAPI.Shared.DTO.Menu;
using WebBlazorAPI.Shared.Modelo;

namespace WebBlazorAPI.Server.Servicios.Implementacion
{
    public class Menus : IMenus
    {
        public readonly IGenericoModelo<Menu> _modeloRepositorio;
        public readonly IMapper _mapper;
        public readonly AppDbContext _db;

        public Menus(IGenericoModelo<Menu> modeloRepositorio, IMapper mapper, AppDbContext db)
        {
            _modeloRepositorio = modeloRepositorio;
            _mapper = mapper;
            _db = db;
        }

        public async Task<List<MenuDropDTO>> GetParendMenu(string Estado)
        {
            try
            {
                var consulta = _modeloRepositorio.GetAllWithWhere(x => x.Estado_menu == Estado).OrderBy(m => m.Id_menu);
                List<MenuDropDTO> lista = _mapper.Map<List<MenuDropDTO>>(await consulta.ToListAsync());
                return lista;

            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public async Task<bool> DeleteMenuLogica(int id)
        {
            try
            {
                var consulta = _modeloRepositorio.GetAllWithWhere(p => p.Id_menu == id);
                var fromDbmodelo = await consulta.FirstOrDefaultAsync();
                if (fromDbmodelo != null)
                {
                    fromDbmodelo.Estado_menu = "I";
                    var respuesta = await _modeloRepositorio.Upadate(fromDbmodelo);
                    if (!respuesta)
                        throw new TaskCanceledException("No se puedo Eliminar");
                    return respuesta;
                }
                else
                { throw new TaskCanceledException("No nose encontraron datos"); }

            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<string> Name_Menu(int id_menu)
        {

            try
            {
                var consulta = await _modeloRepositorio.GetAllWithWhere(p => p.Id_menu == id_menu).FirstOrDefaultAsync();
                if (consulta != null)
                {
                    return consulta.Descripcion ?? "";

                }
                else
                { throw new TaskCanceledException("No nose encontraron datos"); }

            }
            catch (Exception)
            {

                throw;
            }

        }
    }
}
