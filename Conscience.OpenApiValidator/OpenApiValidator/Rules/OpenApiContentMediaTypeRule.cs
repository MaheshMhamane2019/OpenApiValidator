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
		public JsonSchema _jsonSchema { get; private set; }

		/// <summary>
		///
		/// </summary>
		public OpenApiContentMediaTypeRule(IValidatorContext context, KeyValuePair<string, OpenApiMediaType> element) : base(context, element)
		{
			var JSONSchema = JsonSchemas.XApiJsonSchemas?.Where(e => e.Key == element.Value.Schema.Title)?.Select(e => e.Value).FirstOrDefault();

			if (_jsonSchema == null)
			{
				try
				{
					var schemaṬext = element.Value.Schema.Serialize(OpenApiSpecVersion.OpenApi3_0, OpenApiFormat.Json);
					var serializer = new JsonSerializer();
					var json = JsonValue.Parse(schemaṬext);
					_jsonSchema = serializer.Deserialize<JsonSchema>(json);
				}
				catch (Exception e)
				{
					_ruleLogs.Log(e, LogNature.ExArgument);
				}
			}
		}

		public void SetData(HttpRequest request)
		{
			using(var reader = new StreamReader(request.Body, Encoding.UTF8))
			{
				Data = JsonValue.Parse(reader);
			}
		}

		/// <summary>
		///
		/// </summary>
		public override RuleLogs Validate()
		{

			try
			{
				//JsonSerializer serializer = new JsonSerializer();
				//var jsonValue = serializer.Serialize(Data);
				var valid = _jsonSchema.Validate(Data);
				if(!valid.IsValid)
					foreach(var schemaResult in valid.NestedResults)
					{
						_ruleLogs.Log(schemaResult.ErrorMessage);
					}
			}
			catch
			{
			}

			return _ruleLogs;
		}
	}
}
