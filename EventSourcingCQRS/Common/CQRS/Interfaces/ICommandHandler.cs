using System.Threading.Tasks;

namespace Common.CQRS.Interfaces
{
    /// <summary>
    /// Used to decorate a command handler interface with commands
    /// </summary>
    public interface ICommandHandler<ICommand>
    {
        Task Handle(ICommand command);
    }
}
