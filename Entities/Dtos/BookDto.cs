namespace Entities.Dtos
{
   /* [Serializable] // dto nesnesinin xml'e serialize edilebilmesi için ekledik. recordun fiealdları olmadığı için serialize edilirken sorun oldu
    // eğer fiealdlar prop olarak tanımlanmış olsaydı böyle bir sorun yaşanmazdı.
    public record BookDto(int Id, String Title, decimal Price);
   */

    public record BookDto
    {
        public int Id { get; init; }
        public String Title { get; init; }
        public decimal Price { get; set; }
    }
}
