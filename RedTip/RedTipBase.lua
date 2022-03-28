local RedTipBase = {}

function RedTipBase:new()
    local tbl = {}
    setmetatable(tbl, self)
    self.__index = self
    return tbl
end

--region 基本方法

---RedTipBase.initProtperty 基本属性
---@return void
function RedTipBase:initProtperty()
    --子节点列表
    self.childrenNodes = nil
    --子节点Map
    self.childrenNodesMap = nil
    --红点标志
    self.isRed = false
end

function RedTipBase:init(nodeKey)
    self.nodeKey = nodeKey
    self:initProtperty()
    self:onInit()
end

function RedTipBase:isEqualByStrKey(strKey)
    return self.nodeKey == strKey
end

function RedTipBase:hasChildNode()
    return self.childrenNodes ~= nil and #self.childrenNodes > 0
end

function RedTipBase:destroy()
    if self.childrenNodes ~= nil then
        for i = 1, #self.childrenNodes do
            self.childrenNodes[i]:destroy()
        end
        self.childrenNodes = nil
    end
end

function RedTipBase:appendChild(childKey, childNode)
    if self.childrenNodes == nil then
        self.childrenNodes = {}
        self.childrenNodesMap = {}
    end
    if self.childrenNodesMap[childKey] ~= nil then
        error("重复创建同名子节点")
    end
    table.insert(self.childrenNodes, childNode)
    self.childrenNodesMap[childKey] = childNode
end

--region 子节点访问迭代器
local ChildrenIterator = {}

function ChildrenIterator:new(curIndex, redTipBase)
    local tbl = {}
    setmetatable(tbl, self)
    self.__index = self
    tbl.curIndex = curIndex
    tbl.redTipBase = redTipBase
    return tbl
end

function ChildrenIterator:next()
    self.curIndex = self.curIndex + 1
    if self:hasNext() then
        return self.redTipBase.childrenNodes[self.curIndex]
    else
        return nil
    end
end

function ChildrenIterator:hasNext()
    return self.curIndex <= #self.redTipBase.childrenNodes
end
--endregion

function RedTipBase:getChildrenIterator()
    return ChildrenIterator:new(0, self)
end

--endregion

--region 可以注册的回调

---RedTipBase.onRefresh 当刷新的时候调用
---@return  void 空
function RedTipBase:onRefresh()

end

function RedTipBase:onInit()
    self:mockIsRed()
end

--endregion

--region 子类必须重写的东西

---RedTipBase.refresh 具体的刷新方法，子类重写
---@return  void 空
function RedTipBase:refresh()
    self:onRefresh()
end

--endregion

--region test 方法
function RedTipBase:mockIsRed()
    local RedData = Assets.req("RedTip.RedData")
    if RedData.KeyFlag == "MainMenu1" then
        self.isRed = true
    elseif RedData.KeyFlag == "GiftStore" then
        self.isRed = true
    end
end

return RedTipBase