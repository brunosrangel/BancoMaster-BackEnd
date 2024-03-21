using BancoMaster.Domain.Model;

namespace BancoMaster.Service
{
    public interface IGerenciarRotas
    {
        Task CriarRotas(rotas rotas);
        Task AtualizarRota(rotas rotas);
        Task DeletarRotaAsync(rotas rotas);
        Task<List<rotas>> ConsultaRota();
        Task<rotas> ConsultaRotaPorId(int id);
        Task<RetornoCalculoModel> CalculoRotaMaisBarata(ConsultaModel consulta);

    }
}