local RedTipBase = Assets.req("RedTip.RedTipBase")
local RedData = Assets.req("RedTip.RedData")

local RedTipManage = {}

function RedTipManage:init()
    self:resetRoot()
end

function RedTipManage:destroy()
    self:resetRoot()
end

function RedTipManage:resetRoot()
    if self.root ~= nil then
        self.root:destroy()
    end
    self.root = nil
end

function RedTipManage:getRoot()
    return self.root
end

function RedTipManage:createRedTip(nodeKey)
    local node = RedTipBase:new()
    node:init(nodeKey)
    return node
end

function RedTipManage:initFromMatrix(dataLuaTable)
    local rootKey = next(dataLuaTable)
    if rootKey == nil then
        error("没有根节点")
    end
    local rootData = dataLuaTable[rootKey]
    self.root = self:initChild(rootKey, rootData)
end

function RedTipManage:initChild(nodeKey, nodeData)
    local node = self:createRedTip(nodeKey)
    if not RedData:isValidChild(nodeData) then
        --没有子节点
        return node
    end
    for childNodeKey, childNodeData in pairs(nodeData) do
        local childNode = self:initChild(childNodeKey, childNodeData)
        node:appendChild(childNodeKey, childNode)
    end
    return node
end

return RedTipManage