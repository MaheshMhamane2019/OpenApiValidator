using System;
using System.Linq;
using Microsoft.OpenApi.Models;
using System.Collections.Generic;
using Microsoft.OpenApi.Extensions;
using Microsoft.OpenApi;
using Conscience.Logging;
using Manatee.Json.Schema;
using Manatee.Json.Serialization;
using Manatee.Json;
using Microsoft.AspNetCore.Http;
using System.IO;
using System.Text;
using Codeplex.Data;

namespace Conscience.OpenApiValidator
{
	/// <summary>										  
	///
	/// </summary>		  
	[Serializable]
	public class OpenApiContentMediaTypeRule : ValidatorRule<JsonValue, KeyValuePair<string, OpenApiMediaType>>
	{
		private OpenApiSchemaRuleUsingManateeJson _openApiSchemaRule;

		/// <summary>
		///
		/// </summary>
		public OpenApiContentMediaTypeRule(IValidatorContext context, KeyValuePair<string, OpenApiMediaType> element) : base(context, element)
		{

		}

		public void SetData(HttpRequest request)
		{
			using(var reader = new StreamReader(request.Body, Encoding.UTF8))
			{
				Data=JsonValue.Parse(reader);
				_openApiSchemaRule?.SetData(Data);
			}
		}

		public override void AddRule(IValidate rule)
		{
			if(rule is OpenApiSchemaRuleUsingManateeJson)
			{
				_openApiSchemaRule=rule as OpenApiSchemaRuleUsingManateeJson;
			}
		}

		/// <summary>
		///
		/// </summary>
		public override RuleLogs Validate()
		{
			if(_openApiSchemaRule!=null)
				_ruleLogs.Log(_openApiSchemaRule.Validate());

			return _ruleLogs;
		}
			//JsonSerializer serializer = new JsonSerializer();
			//var jsonValue = serializer.Serialize(Data);
			//var valid = _jsonSchema.Validate(Data);
			//if(!valid.IsValid)
			//	foreach(var schemaResult in valid.NestedResults)
			//	{
			//		_ruleLogs.Log(schemaResult.ErrorMessage);
			//	}

		}
}
