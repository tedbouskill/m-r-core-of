using System;

using Common.CQRS.Interfaces;

namespace Common.CQRS
{
    public class Command<IdT, CommandDataT> : ICommand
    {
        public readonly IdT Id;

        public readonly DateTime CommandTimestamp;

        public readonly string CommandName;

        public readonly CommandDataT CommandData;

        Command(IdT id, DateTime commandTimestamp, string commandName, CommandDataT commandData)
        {
            Id = id;
            CommandTimestamp = commandTimestamp;
            CommandName = commandName;
            CommandData = commandData;
        }
    }
}
