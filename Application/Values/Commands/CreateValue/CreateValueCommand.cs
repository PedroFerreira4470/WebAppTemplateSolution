using MediatR;
namespace Application.Values.Commands.CreateValue
{
    public class CreateValueCommand : IRequest<int>
    {
         /// <summary>
         /// Value for the Number
         /// </summary>
        public int ValueNumber { get; set; }
    }
}
