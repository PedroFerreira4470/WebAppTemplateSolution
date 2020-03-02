using MediatR;
namespace Application.Values.Commands.CreateValue
{
    public class CreateValueCommand : IRequest<int>
    {
        public int ValueNumber { get; set; }
    }
}
