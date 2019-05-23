using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace BoEngine.UI
{
	[Serializable]
	public class UIText : UIContainer
	{
		[SerializeField]
		protected Text label;

		public Text Text
		{
			get { return label; }
		}

		public string text
		{
			get
			{
				if (IsExist())
					return label.text;
				return string.Empty;
			}
			set
			{
				if (IsExist())
					label.text = value;
			}
		}

		


		public UIText(GameObject _go) : base(_go)
		{
			label = _go.GetComponent<Text>();
		}

		public UIText(Text _text)
		{
			label = _text;
			if (label!=null)
			{
				Init(label.gameObject);
			}
		}

		public override bool IsExist()
		{
			if (label != null)
				return true;
			LoggerHelper.Error("Text为空，请检查！");
			return false;
		}

		public override void Dispose()
		{
			base.Dispose();
			label = null;
		}
	}
}