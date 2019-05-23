

local _UIDemoView =
{
	a = 2;
	b = "4455"
}

UIDemoView = {};

function UIDemoView.New(obj)
	local s = obj or {};
	print("tttttttttttttt")
	return setmetatable(s, { __index = _UIDemoView })
end


print("UIDemoView")