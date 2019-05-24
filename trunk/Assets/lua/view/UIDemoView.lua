

local _UIDemoView =
{
	image=nil;
	text = nil;
	texture=nil;
	a = "ppp2";
}

UIDemoView = {};

function UIDemoView.New(obj)
	local s = obj or {};
	
	return setmetatable(s, { __index = _UIDemoView})
end


function _UIDemoView:Test()
	print(self.a)	;
end