using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Microsoft.AspNetCore.Http;
using Microsoft.OpenApi.Models;

namespace Conscience.OpenApiValidator
{
	/// <summary>
	///
	/// </summary>	   
	[Serializable]
	public class OpenApiBodyValidator : OpenApiValidatorBase<Stream, OpenApiRequestBody>
	{
		/// <summary>
		///
		/// </summary>
		public bool Required { get; set; }

		/// <summary>
		///
		/// </summary>
		public IDictionary<string, OpenApiMediaType> Content { get; set; }

		/// <summary>
		///
		/// </summary>
		public OpenApiBodyValidator(IValidatorContext context, OpenApiRequestBody openApiBody) : base(context, openApiBody)
		{

		}

		public void SetData(HttpRequest request)
		{
			foreach(var rule in Rules)
			{
				var queryStringParameterRule = rule as Conscience.OpenApiValidator.OpenApiContentMediaTypeRule;

				queryStringParameterRule.SetData(request);

				//if(queryStringParameterRule!=null&&request.Headers!=null)
				//	foreach(var header in request.Headers.Where(e => rule.RequestElementName==e.Key))
				//	{
				//		queryStringParameterRule.SetData(header);
				//	}
			}

			//foreach (var item in request.Cookies)
			//{
			//	foreach (var rule in Rules.Where(e => e.RequestElementName == item.Key))
			//	{
			//		rule.SetData(item);
			//	}
			//}
		}

		/// <summary>
		///
		/// </summary>
		public override RuleLogs Validate()
		{
			return base.Validate();
		}
	}
}
