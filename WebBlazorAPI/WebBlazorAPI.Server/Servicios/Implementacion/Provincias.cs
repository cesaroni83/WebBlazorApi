using AutoMapper;
using Microsoft.EntityFrameworkCore;
using WebBlazorAPI.Server.Data;
using WebBlazorAPI.Server.RepositorioGeneral;
using WebBlazorAPI.Shared.DTO.Provincia;
using WebBlazorAPI.Shared.Modelo;

namespace WebBlazorAPI.Server.Servicios.Implementacion
{
    public class Provincias : IProvincias
    {
        public readonly IGenericoModelo<Provincia> _modeloRepositorio;
        public readonly IMapper _mapper;
        public readonly AppDbContext _context;
        // private object fromDBmodelo;
        public Provincias(IGenericoModelo<Provincia> modeloRepositorio, IMapper mapper, AppDbContext context)
        {
            _modeloRepositorio = modeloRepositorio;
            _mapper = mapper;
            _context = context;
        }


        public async Task<bool> DeleteProvinciaLogica(int Id_provincia_aux)
        {
            try
            {
                var consulta = _modeloRepositorio.GetAllWithWhere(p => p.Id_provincia == Id_provincia_aux);
                var fromDbmodelo = await consulta.FirstOrDefaultAsync();
                if (fromDbmodelo != null)
                {
                    fromDbmodelo.Estado_provincia = "I";
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

        public async Task<List<ProvinciaDTO>> GetListProvinciaActivo(string Estado_Activo)
        {
            try
            {
                ///con referencia
                var consulta = _modeloRepositorio.GetAllWithWhere(p => p.Estado_provincia == Estado_Activo);

                var fromDBmodelo = await consulta.ToListAsync();
                if (fromDBmodelo != null && fromDBmodelo.Any())
                {
                    return _mapper.Map<List<ProvinciaDTO>>(fromDBmodelo);
                }
                else
                { throw new TaskCanceledException("No nose encontraron considencia"); }

            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public async Task<List<ProvinciaDTO>> GetProvinciaByPais(int id_pais)
        {
            try
            {
                var consulta = _modeloRepositorio.GetAllWithWhere(p => p.Id_pais == id_pais).OrderBy(m => m.Id_provincia);
                List<ProvinciaDTO> lista = _mapper.Map<List<ProvinciaDTO>>(await consulta.ToListAsync());
                return lista;

            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public async Task<List<ProvinciaDropDTO>> GetProvinciaCombo(int id_pais, string Estado_Activo)
        {
            try
            {
                var consulta = _modeloRepositorio.GetAllWithWhere(x => x.Paises!.Id_pais == id_pais && x.Estado_provincia == Estado_Activo).OrderBy(m => m.Nombre_provincia);
                List<ProvinciaDropDTO> lista = _mapper.Map<List<ProvinciaDropDTO>>(await consulta.ToListAsync());
                return lista;

            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
    }
}
