using BancoMaster.Domain.Model;


namespace BancoMaster.Core
{
    public class CalculaValorRota : ICalculaValorRota
    {
       
        public async Task<RetornoCalculoModel> CriaRotaMaisBarata(ConsultaModel consulta, List<rotas> ListaDasRotas)
        {
            var matrixViagem = await criaMatrixComparacao(ListaDasRotas);
            var trajeto = TrajetoMaisBarato(consulta.origem, consulta.destino, matrixViagem);
            var CustoViagem = SomarCustoRota(trajeto, matrixViagem);
            var CalculoFinal = new RetornoCalculoModel
            {
                trajeto = string.Join(" - ", trajeto),
                CustoViagem = CustoViagem.ToString()
            };
            return CalculoFinal;
        }
        internal Task<Dictionary<string, Dictionary<string, int>>> criaMatrixComparacao(List<rotas> lstRotas)
        {
            var matrixViagem = new Dictionary<string, Dictionary<string, int>>();
            foreach (rotas rotas in lstRotas)
            {

                if (!matrixViagem.ContainsKey(rotas.Origem))
                {
                    matrixViagem[rotas.Origem] = new Dictionary<string, int>();
                }
                matrixViagem[rotas.Origem][rotas.Destino] = rotas.Valor;

                if (!matrixViagem.ContainsKey(rotas.Destino))
                {
                    matrixViagem[rotas.Destino] = new Dictionary<string, int>();
                }
                matrixViagem[rotas.Destino][rotas.Origem] = rotas.Valor;
            }
            return Task.FromResult(matrixViagem);
        }
        internal List<string> TrajetoMaisBarato(string origem, string destino, Dictionary<string, Dictionary<string, int>> lstRotas)
        {
            var anteriores = new Dictionary<string, string>();
            var distancias = new Dictionary<string, int>();
            var fila = new List<string>();

            foreach (var no in lstRotas.Keys)
            {
                if (no == origem)
                {
                    distancias[no] = 0;
                }
                else
                {
                    distancias[no] = int.MaxValue;
                }
                fila.Add(no);
            }

            while (fila.Count != 0)
            {
                fila.Sort((x, y) => distancias[x] - distancias[y]);
                var noAtual = fila.First();
                fila.Remove(noAtual);

                foreach (var vizinho in lstRotas[noAtual])
                {
                    var distanciaAlternativa = distancias[noAtual] + vizinho.Value;
                    if (distanciaAlternativa < distancias[vizinho.Key])
                    {
                        distancias[vizinho.Key] = distanciaAlternativa;
                        anteriores[vizinho.Key] = noAtual;
                    }
                }
            }

            var caminho = new List<string>();
            var atual = destino;
            while (anteriores.ContainsKey(atual))
            {
                caminho.Add(atual);
                atual = anteriores[atual];
            }
            caminho.Add(origem);
            caminho.Reverse();

            return caminho;
        }
        internal int SomarCustoRota(List<string> rota, Dictionary<string, Dictionary<string, int>> mtxRotas)
        {
            int valor = 0;
            for (int i = 0; i < rota.Count - 1; i++)
            {
                valor += mtxRotas[rota[i]][rota[i + 1]];
            }
            return valor;
        }


    }
}
