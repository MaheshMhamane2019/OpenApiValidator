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
using Codeplex.Data;

namespace Conscience.OpenApiValidator
{
	public static class JsonSchemas
	{
		private static Dictionary<string, JsonSchema> xApiItems = new Dictionary<string, JsonSchema>();

		public static Dictionary<string, JsonSchema> XApiJsonSchemas { get => xApiItems; }

		public static void Read(string path)
		{
		}
	}

	/// <summary>										  
	///	 
	/// </summary>	 
	[Serializable]
	public class OpenApiSchemaRuleUsingManateeJson : ValidatorRule<JsonValue, OpenApiSchema>
	{
		public JsonSchema _jsonSchema { get; private set; }

		/// <summary>
		///
		/// </summary>
		public OpenApiSchemaRuleUsingManateeJson(IValidatorContext context, OpenApiSchema element) : base(context, element)
		{
			_jsonSchema = JsonSchemas.XApiJsonSchemas?.Where(e => e.Key == element.Title)?.Select(e => e.Value).FirstOrDefault();
			if (_jsonSchema == null)
			{
				try
				{
					var schemaṬext = element.SerializeAsJson(OpenApiSpecVersion.OpenApi3_0);
					var serializer = new JsonSerializer();
					var json = JsonValue.Parse(schemaṬext);

					_jsonSchema = serializer.Deserialize<JsonSchema>(json);
				}
				catch (Exception e)
				{
					_ruleLogs.Log($"OpenApiSchema to JSONSchema conversion {e.Message}", "OpenApiSchemaRule Constructor", LogNature.Log, Env.DEV);//_ruleLogs.Log(e, LogNature.ExArgument);
				}
			}
		}

		/// <summary>
		///
		/// </summary>
		public override RuleLogs Validate()
		{
			try
			{
				///This try catch will be removed shortly. 
				///This is not good progrmming practice.
				JsonSchemaOptions.OutputFormat=SchemaValidationOutputFormat.Verbose;
				var valid = _jsonSchema.Validate(Data);

				if(!valid.IsValid)
					foreach(var schemaResult in valid.NestedResults)
						_ruleLogs.Log(schemaResult.ErrorMessage);
			}
			catch
			{ }

			//try
			//{
			//	//This try catch will be removed shortly.
			//	//This is not good progrmming practice.
			//	//JToken json = JToken.Parse(_data);
			//	//IList<ValidationError> errors;

			//	JsonSchemaOptions.OutputFormat=SchemaValidationOutputFormat.Verbose;
			//	var jsonv = JsonValue.Parse(DynamicJson.Parse(Data).ToString());
			//	//JsonSerializer serializer = new JsonSerializer();
			//	//var jsonValue = serializer.Serialize(Data);
			//	SchemaValidationResults schemaResults = _jsonSchema.Validate(jsonv);

			//	if(!schemaResults.IsValid)
			//		foreach(var schemaResult in schemaResults.NestedResults)
			//		{
			//			_ruleLogs.Log(schemaResult.ErrorMessage);
			//		}

			//}
			//catch
			//{	 
			//}

			return _ruleLogs;
		}
	}
}
