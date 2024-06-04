using CinemaxToledo.Integracao.Response;
using Refit;

namespace CinemaxToledo.Integracao.Refit
{
    public interface IViaCepIntegracaoRefit
    {

        [Get("/ws/{cep}/json")]
        Task<ApiResponse<ViaCepResponse>> ObterDadosViaCep(string cep);
    }
}
