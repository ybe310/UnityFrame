using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace BoEngine.UI
{
	[Serializable]
	public class UIInput : UIContainer
	{
		[SerializeField]
		protected InputField inputField;

		public InputField InputField
		{
			get { return inputField; }
		}

		public string text
		{
			get
			{
				if (IsExist())
					return inputField.text;
				return null;
			}
			set
			{
				if (IsExist())
					inputField.text = value;
			}
		}


		public UIInput(GameObject _go) : base(_go)
		{
			inputField = _go.GetComponent<InputField>();
		}

		public UIInput(InputField _input)
		{
			inputField = _input;
			if (inputField!=null)
			{
				Init(inputField.gameObject);
			}
		}


		public override bool IsExist()
		{
			if (inputField != null)
				return true;
			LoggerHelper.Error("InputField为空，请检查！");
			return false;
		}

		public override void Dispose()
		{
			base.Dispose();
			inputField = null;
		}
	}
}