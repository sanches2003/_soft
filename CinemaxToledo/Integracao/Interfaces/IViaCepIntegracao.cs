using CinemaxToledo.Integracao.Response;

namespace CinemaxToledo.Integracao.Interfaces
{
    public interface IViaCepIntegracao
    {

        Task<ViaCepResponse> ObterDadosViaCep(string cep);
    }
}
