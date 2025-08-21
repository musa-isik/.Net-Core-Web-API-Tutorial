namespace Entities.Exceptions
{

    public class PricaOutOfRangeBadRequestException : BadRequestException
    {
        public PricaOutOfRangeBadRequestException() : base("Maximum price should be more less 1000 and greater than 10.")
        {
        }
    }

}
