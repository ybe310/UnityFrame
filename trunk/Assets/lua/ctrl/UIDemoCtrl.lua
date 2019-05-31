
UIDemoCtrl =
{
	view = nil;
};

local this = UIDemoCtrl;

function UIDemoCtrl.Awake(_view)
	this.view = _view;


	this.view.lb:AddClick(this.view.btn, this.Test, CS.UnityEngine.Vector3(2,3.5,444));
	this.view.lb:AddClick(this.view.btn2, this.Test2);
end

function UIDemoCtrl.Test(_vec)
	local vec = _vec;
	vec.x = 0.2;

	print(vec.x)
end

function UIDemoCtrl.Test2()
	local v = this.view.lb:Vector3ParamMethod({x = 2, y = 3.5, z = 444});

	this.Test(v);
end

function UIDemoCtrl.Get(...)
	return ...;
end

function UIDemoCtrl.Update()
	
end