namespace Eris.Handlers;

internal interface IHandlerManager<THandler> where THandler : IExecutableHandler
{
    void AddHandler(THandler handler);
}
