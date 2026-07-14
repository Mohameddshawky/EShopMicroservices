using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuildingBlocks.CQRS
{

    public interface ICommand : ICommand<Unit>//no response unit->void
    {
    }

    public interface ICommand<TResponse>:IRequest<TResponse>
    {
    }
}
