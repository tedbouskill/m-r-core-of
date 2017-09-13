using System.Threading.Tasks;

namespace Common.CQRS.Interfaces
{
    public interface ICommandHandler<ICommand>
    {
        Task Handle(ICommand command);
    }
}
