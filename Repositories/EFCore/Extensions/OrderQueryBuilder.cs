using System.Reflection;
using System.Text;

namespace Repositories.EFCore.Extensions
{
    public static class OrderQueryBuilder
    {
        public static String CreateOrderQuery<T>(String orderByOrderString)
        {
            var orderParams = orderByOrderString.Trim().Split(','); // virgülle ayrılmış parametreleri alırız. bir dizi döner

            var propertyInfos = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance);

            var orderQueryBuilder = new StringBuilder();

            foreach (var param in orderParams)
            {
                var propertyFromQueryName = param.Split(' ')[0];
                var objectProperty = propertyInfos.FirstOrDefault(pi => pi.Name.Equals(propertyFromQueryName, StringComparison.InvariantCultureIgnoreCase));
                if (objectProperty is null)
                    continue; // eğer null ise yani böyle bir property yoksa devam et

                var direction = param.EndsWith(" desc") ? "descending" : "ascending"; // eğer parametre desc ile bitiyorsa OrderByDescending, değilse OrderBy
                orderQueryBuilder.Append($"{objectProperty.Name.ToString()} {direction},"); // orderQueryBuilder'a property'nin ismini ve yönünü ekle
            }
            var orderQuery = orderQueryBuilder.ToString().TrimEnd(',', ' '); // sonundaki virgülü kaldırıp boşluk ekledik

            return orderQuery;
        }
    }
}
