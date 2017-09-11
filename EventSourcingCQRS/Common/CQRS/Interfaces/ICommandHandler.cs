using System;

namespace Common.CQRS.Interfaces
{
    public interface ICommandHandler<ICommand>
    {
        void Handle(ICommand command);
    }
}
