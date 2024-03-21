using BancoMaster.Core;
using BancoMaster.Db;
using BancoMaster.Domain;
using BancoMaster.Domain.Model;

namespace BancoMaster.Service
{
    public class GerenciarRotas : IGerenciarRotas
    {
        public readonly ICalculaValorRota _calculaValorRota;
        private readonly IRepository<rotas> _rotasRepository;

        public GerenciarRotas(ICalculaValorRota calculaValorRota, IRepository<rotas> rotasRepository)
        {
            _calculaValorRota = calculaValorRota;
            _rotasRepository = rotasRepository;
        }

        public async Task CriarRotas(rotas rotas)
        {
            await _rotasRepository.AddAsync(rotas);
        }

        public async Task AtualizarRota(rotas rotas)
        {
             await _rotasRepository.UpdateAsync(rotas);          
        }

        public async Task DeletarRotaAsync(rotas rotas)
        {
            await _rotasRepository.DeleteAsync(rotas);           
        }

        public async Task<List<rotas>> ConsultaRota()
        {
            return await _rotasRepository.GetAllAsync();
        }

        public async Task<rotas> ConsultaRotaPorId(int id) => await _rotasRepository.GetByIdAsync(id);

        public async Task<RetornoCalculoModel> CalculoRotaMaisBarata(ConsultaModel consulta)
        {
            return await _calculaValorRota.CriaRotaMaisBarata(consulta, await ConsultaRota());
        }


    }
}
