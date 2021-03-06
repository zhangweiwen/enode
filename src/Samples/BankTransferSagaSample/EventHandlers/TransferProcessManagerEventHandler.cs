﻿using System;
using BankTransferSagaSample.Commands;
using BankTransferSagaSample.Events;
using ENode.Commanding;
using ENode.Eventing;

namespace BankTransferSagaSample.EventHandlers
{
    public class TransferProcessManagerEventHandler :
        IEventHandler<TransferProcessStarted>,
        IEventHandler<TransferOutRequested>,
        IEventHandler<TransferInRequested>
    {
        private ICommandService _commandService;

        public TransferProcessManagerEventHandler(ICommandService commandService)
        {
            _commandService = commandService;
        }

        void IEventHandler<TransferProcessStarted>.Handle(TransferProcessStarted evnt)
        {
            Console.WriteLine(evnt.Description);
        }
        void IEventHandler<TransferOutRequested>.Handle(TransferOutRequested evnt)
        {
            _commandService.Send(
                new TransferOut
                {
                    ProcessId = evnt.ProcessId,
                    SourceAccountId = evnt.SourceAccountId,
                    TargetAccountId = evnt.TargetAccountId,
                    Amount = evnt.Amount
                });
        }
        void IEventHandler<TransferInRequested>.Handle(TransferInRequested evnt)
        {
            _commandService.Send(
                new TransferIn
                {
                    ProcessId = evnt.ProcessId,
                    SourceAccountId = evnt.SourceAccountId,
                    TargetAccountId = evnt.TargetAccountId,
                    Amount = evnt.Amount
                });
        }
    }
}
