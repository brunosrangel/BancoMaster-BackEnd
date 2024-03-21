using BancoMaster.Domain.Model;

namespace BancoMaster.Core
{
    public interface ICalculaValorRota
    {
        Task<RetornoCalculoModel> CriaRotaMaisBarata(ConsultaModel consulta, List<rotas> ListaDasRotas);
    }
}