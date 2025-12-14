using AutoMapper;
using Microsoft.EntityFrameworkCore;
using WebBlazorAPI.Server.Data;
using WebBlazorAPI.Server.RepositorioGeneral;
using WebBlazorAPI.Shared.DTO.Pais;
using WebBlazorAPI.Shared.Modelo;

namespace WebBlazorAPI.Server.Servicios.Implementacion
{
    public class Paises : IPaises
    {
        public readonly IGenericoModelo<Pais> _modeloRepositorio;
        public readonly IMapper _mapper;
        public readonly AppDbContext _db;

        public Paises(IGenericoModelo<Pais> modeloRepositorio, IMapper mapper, AppDbContext db)
        {
            _modeloRepositorio = modeloRepositorio;
            _mapper = mapper;
            _db = db;
        }
        public async Task<bool> DeletePaisLogica(int id_pais)
        {
            try
            {
                var consulta = _modeloRepositorio.GetAllWithWhere(p => p.Id_pais == id_pais);
                var fromDbmodelo = await consulta.FirstOrDefaultAsync();
                if (fromDbmodelo != null)
                {
                    fromDbmodelo.Estado_pais = "I";
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

        public async Task<List<PaisDropDTO>> GetPaisCombo(string Estado_Activo)
        {
            try
            {
                var consulta = _modeloRepositorio.GetAllWithWhere(x => x.Estado_pais == Estado_Activo).OrderBy(m => m.Nombre_pais);
                List<PaisDropDTO> lista = _mapper.Map<List<PaisDropDTO>>(await consulta.ToListAsync());
                return lista;

            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public async Task<List<PaisDTO>> GetListaAllPaises()
        {

            try
            {
                var consulta = _modeloRepositorio.GetAll()
                    .Include(x => x.Provincias)
                    .OrderBy(m => m.Id_pais);
                List<PaisDTO> lista = _mapper.Map<List<PaisDTO>>(await consulta.ToListAsync());
                return lista;

            }
            catch (Exception ex)
            {

                throw ex;
            }

        }

        public async Task<List<PaisDTO>> GetListPaisActivo(string Estado_Activo)
        {
            try
            {
                ///con referencia
                var consulta = _modeloRepositorio.GetAllWithWhere(p => p.Estado_pais == Estado_Activo);

                var fromDBmodelo = await consulta.ToListAsync();
                if (fromDBmodelo != null && fromDBmodelo.Any())
                {
                    return _mapper.Map<List<PaisDTO>>(fromDBmodelo);
                }
                else
                { throw new TaskCanceledException("No nose encontraron considencia"); }

            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

    }
}
