namespace FashFuns.Common.Exceptions.GlobalHandler.Contracts
{
    public interface IGlobalExceptionFactory
    {
        IGlobalExceptionHandler New();
    }
}
