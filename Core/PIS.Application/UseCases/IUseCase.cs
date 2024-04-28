namespace PIS.Application.UseCases
{
    public interface IUseCase<TRequest,TResponse>
    {
        public Task<TResponse> HandleAsync(TRequest request);
    }
}
