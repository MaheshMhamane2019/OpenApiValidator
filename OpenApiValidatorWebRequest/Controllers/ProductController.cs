using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace OpenApiValidatorWebRequest.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class ProductController : ControllerBase
	{
		// GET api/values
		[HttpGet]
		public ActionResult<IEnumerable<Product>> Get()
		{
			return new List<Product>() { new Product { Price=10 } };
		}

		// GET api/values/5
		//[HttpGet("{id}")]
		//public ActionResult<Product> Get([FromQuery] int id)
		//{
		//	return new Product { Price=10 };
		//}

		// POST api/values
		[HttpPost]
		public void Post([FromBody] Product value)
		{
		}

		// PUT api/values/5
		[HttpPut("{id}")]
		public void Put(int id, [FromBody] Product value)
		{
		}

		// DELETE api/values/5
		[HttpDelete("{id}")]
		public void Delete(int id)
		{

		}
	}
}
