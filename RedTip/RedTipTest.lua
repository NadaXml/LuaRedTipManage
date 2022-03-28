require "RedTip.RequireMock"


local RedTipManage = Assets.req("RedTip.RedTipManage")
local RedTipBase = Assets.req("RedTip.RedTipBase")
local RedData = Assets.req("RedTip.RedData")
local RedTipUtility = Assets.req("RedTip.RedTipUtility")

function main()

    RedTipManage:resetRoot()
    RedTipManage:initFromMatrix(RedData.matrix)

    test_findRedTipBase()
end

function test_findRedTipBase()
    local root = RedTipManage:getRoot()
    local findStrKey = RedData.KeyFlag.GiftStore
    local findFlag, findRedTipBase = RedTipUtility:findRedTipBaseByStrKey(findStrKey, root)
    --assert(findFlag == true, tostring(findRedTipBase))
    print(" find test ret", findStrKey, findFlag, findRedTipBase and findRedTipBase.nodeKey)
end


main()