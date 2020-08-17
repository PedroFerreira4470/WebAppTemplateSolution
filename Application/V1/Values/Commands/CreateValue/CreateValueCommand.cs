using Application.Common.Contracts.V1.ResponseType;

using MediatR;
namespace Application.V1.Values.Commands.CreateValue
{
    public class CreateValueCommand : IRequest<Response<int>>
    {
        /// <summary>
        /// Value for the Number
        /// </summary>
        public int ValueNumber { get; set; }
    }
}
