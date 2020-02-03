using System.Collections.Generic;

namespace OpenApiValidatorWebRequest.Controllers
{
	public class Dimensions
	{
		public double Length { get; set; }
		public double Width { get; set; }
		public double Height { get; set; }
	}

	public class Product
	{
		public int ProductId { get; set; }
		public string ProductName { get; set; }
		public double Price { get; set; }
		public List<string> Tags { get; set; }
		public Dimensions Dimensions { get; set; }
	}
}
