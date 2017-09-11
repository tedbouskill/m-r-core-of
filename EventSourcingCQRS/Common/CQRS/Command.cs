using System;

using Common.CQRS.Interfaces;

namespace Common.CQRS
{
    public class Command<KeyT, CommandDataT> : ICommand
    {
        public readonly KeyT Key;

        public readonly DateTime CommandTimestamp;

        public readonly string CommandName;

        public readonly CommandDataT CommandData;

        Command(KeyT key, DateTime commandTimestamp, string commandName, CommandDataT commandData)
        {
            Key = key;
            CommandTimestamp = commandTimestamp;
            CommandName = commandName;
            CommandData = commandData;
        }
    }
}
