namespace Core.UseCases
{
    public interface IRegisterUseCase
    {
        RegisterResponse Execute(RegisterCommand registerCommand);
    }
}