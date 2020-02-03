using Conscience.Logging;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace Conscience.OpenApiValidator
{
	/// <summary>
	///
	/// </summary>
	public interface IValidate
	{
		/// <summary>
		///
		/// </summary>
		string RequestElementName { get; set; }

		/// <summary>
		///
		/// </summary>
		RuleLogs Validate();

		/// <summary>
		///
		/// </summary>
		///void AddRule(IValidate validator);

		/// <summary>
		///
		/// </summary>
		///void AddRule(IList<IValidate> validator);

		/// <summary>
		///
		/// </summary>
		void AddRule(IValidate validator);

		/// <summary>
		///
		/// </summary>
		void AddRule(IEnumerable<IValidate> validator);

		void ClearLog();  

		void ReleaseElement();  
	}

	public interface ISetData<TData>
	{
		/// <summary>
		///
		/// </summary>
		void SetData(TData parent);
	}

	/// <summary>
	///
	/// </summary>	 
	[Serializable]
	[DebuggerDisplay("Log= {LogCount}; Rules= {Rules.Count}")]
	public class ValidatorRule<TData, TElement> : IValidate, ISetData<TData> 
	{
		/// <summary>
		///
		/// </summary>
		//protected TData _data;

		/// <summary>
		///
		/// </summary>	
		[NonSerialized]
		private TElement _openApiElement;

		/// <summary>
		///
		/// </summary>
		[NonSerialized]
		public TData Data;

		/// <summary>
		///
		/// </summary>
		private IValidatorContext _context;

		/// <summary>
		///
		/// </summary>
		internal RuleLogs _ruleLogs;

		/// <summary>
		///
		/// </summary>
		private int LogCount => _ruleLogs == null ? 0 : _ruleLogs.Count;

		/// <summary>
		///
		/// </summary>
		private IValidatorContext context;

		/// <summary>
		///
		/// </summary>
		public string RequestElementName { get; set; }

		public string RuleName { get { return this.GetType().Name; } }

		public string RuleCode { get; set; }

		public string LocalizationResourceKey { get; set; }

		public ValidatorRule(IValidatorContext context, TElement element) : this(context, element, new RuleLogs())
		{ }		   

		/// <summary>
		///
		/// </summary>
		public ValidatorRule(IValidatorContext context, TElement element, RuleLogs ruleLogs)
		{
			_ruleLogs=ruleLogs;

			if (context == null)
				_ruleLogs.Log(new NullReferenceException($"Rule: {RuleName}, Field: _data"), LogNature.ExArgument, $"NULL - Data Field of {RuleName}");

			_context = context;
			_openApiElement = element;
			if (element != null)
				OpenApiElementName = element.GetType().Name;

			Rules=new List<IValidate>();

			RequestElementName = element.GetFieldNameForRules();
		}

		/// <summary>
		///
		/// </summary>
		public virtual List<IValidate> Rules { get; set; }

		public string OpenApiElementName { get; set; }

		/// <summary>
		///
		/// </summary>
		public virtual RuleLogs Validate()
		{
			//if(_data==null)
				//_ruleLogs.Log(new NullReferenceException($"Rule: {RuleName}, Field: _data"), LogNature.ExArgument, $"NULL - Data Field of {RuleName}");

			//foreach(var rule in this)
			//{
			//	_ruleLogs.Log(rule.Validate());
			//}

			foreach(var rule in Rules)
			{
				_ruleLogs.Log(rule.Validate());
			}

			return _ruleLogs;
		}

		public virtual void ClearLog()
		{
			_ruleLogs.Clear();
			Data=default(TData);
			foreach(var rule in Rules)
			{
				rule.ClearLog();
			}
		}


		/// <summary>
		///
		/// </summary>
		public void ReleaseElement()
		{
			_openApiElement=default(TElement);

			foreach(var rule in Rules)
			{
				rule.ReleaseElement();
			}
		}

		/// <summary>
		///
		/// </summary>
		public virtual void SetData(TData data)
		{
			Data=data;
		}

		/// <summary>
		///
		/// </summary>
		public override int GetHashCode()
		{
			//var hash = new HashCode();
			//hash.Add(_context);
			//hash.Add(hash);
			//hash.Add(_context.ValidatorOptions);
			//return hash.ToHashCode();
			return base.GetHashCode();
		}

		/// <summary>
		///
		/// </summary>
		//public void AddRule(IValidate validator)
		//{
		//	if(validator!=null)
		//		this.Add(validator);
		//}

		/// <summary>
		///
		/// </summary>
		//public void AddRule(IList<IValidate> validators)
		//{
		//if(validators!=null)
		//	this.AddRange(validators);
		//}

		/// <summary>
		///
		/// </summary>
		public virtual void AddRule(IValidate validator)
		{
			if(validator!=null)
			{
				Rules.Add(validator);
			}
		}

		/// <summary>
		///
		/// </summary>
		public virtual void AddRule(IEnumerable<IValidate> validators)
		{
			if(validators!=null)
			{
				Rules.AddRange(validators);
			}
		}
	}

	/// <summary>
	/// Object containing contextual information based on where the walker is currently referencing in an OpenApiDocument
	/// </summary>
	internal class CurrentKeys
	{
		/// <summary>
		/// Current Path key
		/// </summary>
		internal string Path { get; set; }

		/// <summary>
		/// Current Operation Type
		/// </summary>
		internal OperationType? Operation { get; set; }

		/// <summary>
		/// Current Response Status Code
		/// </summary>
		internal string Response { get; set; }

		/// <summary>
		/// Current Content Media Type
		/// </summary>
		internal string Content { get; set; }

		/// <summary>
		/// Current Callback Key
		/// </summary>
		internal string Callback { get; set; }

		/// <summary>
		/// Current Link Key
		/// </summary>
		//internal string Link { get; set; }

		/// <summary>
		/// Current Header Key
		/// </summary>
		internal string Header { get; set; }

		/// <summary>
		/// Current Encoding Key
		/// </summary>
		internal string Encoding { get; set; }

		/// <summary>
		/// Current Example Key
		/// </summary>
		//internal string Example { get; internal set; }

		/// <summary>
		/// Current Extension Key
		/// </summary>
		internal string Extension { get; set; }

		/// <summary>
		/// Current ServerVariable
		/// </summary>
		internal string ServerVariable { get; set; }
	}
}
