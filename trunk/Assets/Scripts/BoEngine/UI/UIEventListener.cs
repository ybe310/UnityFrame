using System;
using System.Collections;
using System.Collections.Generic;
using BoEngine.UI;
using UnityEngine;
using UnityEngine.EventSystems;

namespace BoEngine.UI
{
	[Serializable]
	public class UIEventListener : UIContainer
	{
		[SerializeField]
		protected EventTriggerListener eventTrigger;

		public EventTriggerListener EventListener
		{
			get { return eventTrigger; }
		}

		private EventTriggerListener.VoidDelegate _OnClick;
		private EventTriggerListener.BoolDelegate _OnPress;
		private EventTriggerListener.VoidDelegate _OnDown;
		private EventTriggerListener.VoidDelegate _OnEnter;
		private EventTriggerListener.VoidDelegate _OnExit;
		private EventTriggerListener.VoidDelegate _OnUp;
		private EventTriggerListener.VoidDelegate _OnSelect;
		private EventTriggerListener.VoidDelegate _OnUpdateSelect;
		private EventTriggerListener.VectorDelegate _OnDrag;

		public EventTriggerListener.VoidDelegate onClick
		{
			set { _OnClick = value; }
		}

		public EventTriggerListener.BoolDelegate onPress
		{
			set { _OnPress = value; }
		}

		public EventTriggerListener.VoidDelegate onDown
		{
			set { _OnDown = value; }
		}

		public EventTriggerListener.VoidDelegate onEnter
		{
			set { _OnEnter = value; }
		}

		public EventTriggerListener.VoidDelegate onExit
		{
			set { _OnExit = value; }
		}

		public EventTriggerListener.VoidDelegate onUp
		{
			set { _OnUp = value; }
		}

		public EventTriggerListener.VoidDelegate onSelect
		{
			set { _OnSelect = value; }
		}

		public EventTriggerListener.VoidDelegate onUpdateSelect
		{
			set { _OnUpdateSelect = value; }
		}

		public EventTriggerListener.VectorDelegate onDrag
		{
			set { _OnDrag = value; }
		}

		public object parameter
		{
			get { return eventTrigger.parameter; }
			set { eventTrigger.parameter = value; }
		}


		public UIEventListener(GameObject _go) : base(_go)
		{
			eventTrigger = EventTriggerListener.Get(_go);
			if (eventTrigger != null)
			{
				eventTrigger.onClick = _OnClick;
				eventTrigger.onPress = _OnPress;
				eventTrigger.onDown = _OnDown;
				eventTrigger.onEnter = _OnEnter;
				eventTrigger.onExit = _OnExit;
				eventTrigger.onUp = _OnUp;
				eventTrigger.onSelect = _OnSelect;
				eventTrigger.onUpdateSelect = _OnUpdateSelect;
				eventTrigger.onDrag = _OnDrag;
			}

		}

	}
}