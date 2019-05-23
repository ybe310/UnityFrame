using System;
using System.Collections;
using System.Collections.Generic;
using BoEngine.UI;
using UnityEngine;
using UnityEngine.UI;

namespace BoEngine.UI
{
	[Serializable]
	public class UITexture : UIContainer
	{
		[SerializeField]
		protected RawImage rawImage;

		public RawImage RawImage
		{
			get { return rawImage; }
		}

		public Texture texture
		{
			get
			{
				if (IsExist())
					return rawImage.texture;
				return null;
			}
		}


		public UITexture(GameObject _go) : base(_go)
		{
			rawImage = _go.GetComponent<RawImage>();
		}

		public UITexture(RawImage _rawImage)
		{
			rawImage = _rawImage;
			if (rawImage!=null)
			{
				Init(rawImage.gameObject);
			}
		}

		public override bool IsExist()
		{
			if (rawImage != null)
				return true;
			LoggerHelper.Error("RawImage为空，请检查！");
			return false;
		}

		public override void Dispose()
		{
			base.Dispose();
			rawImage = null;
		}
	}
}