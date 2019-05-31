require "ctrl.UIDemoCtrl"

local _UIDemoView =
{
--start
	--Text
	txt=nil;
	--Image
	img=nil;
	--GameObject
	obj=nil;
	--GameObject[]
	objs=nil;
	--Button
	btn=nil;
	--Button
	btn2=nil;
--end
}

UIDemoView ={};

local ctrl = UIDemoCtrl;

function UIDemoView.New(obj)
	local s = obj or {};
	
	return setmetatable(s, { __index = _UIDemoView})
end

function _UIDemoView:Awake()
	
end

function _UIDemoView:GetCtrl()
	return ctrl;
end