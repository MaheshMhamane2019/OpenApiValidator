using System;
using System.Linq;
using Microsoft.AspNetCore.Http;

namespace Conscience.OpenApiValidator
{
	/// <summary>
	///
	/// </summary>	 
	[Serializable]
	public class OpenApiPathValidator : OpenApiValidatorBase<PathString, object>
	{
		PathString _pathString;

		public OpenApiPathValidator(IValidatorContext context) : base(context, null)
		{ }

		public void SetData(HttpRequest request)
		{
			_pathString = request.Path;
		}

		public override RuleLogs Validate()
		{
			return base.Validate();
		}
	}
}
