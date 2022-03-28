
--region 测试数据
local RedData = {}

RedData.KeyFlag = {
    Root = "Root",
    Tribe = "Tribe",
    MainMenu1 = "MainMenu1",
    MainMenu2 = "MainMenu2",
    MainMenu3 = "MainMenu3",
    Market = "Matrket",
    GiftStore = "GiftStore",
    Role = "Role",
}

--endregion

local NULL_CHILD = 0

local KeyFlag = RedData.KeyFlag

RedData.matrix = {
    [KeyFlag.Root] = {
        [KeyFlag.Tribe] = {
            [KeyFlag.MainMenu1] = NULL_CHILD,
            [KeyFlag.MainMenu2] = NULL_CHILD,
            [KeyFlag.MainMenu3] = NULL_CHILD,
        },
        [KeyFlag.Market] = {
            [KeyFlag.GiftStore] = NULL_CHILD
        },
        [KeyFlag.Role] = NULL_CHILD,--for dynamic
    }
}

RedData.dynamic = {
    [KeyFlag.Role] = {
        ["rol1Id1"] = NULL_CHILD,
    ["rol1Id2"] = NULL_CHILD,
    ["rol1Id3"] = NULL_CHILD,
    ["rol1Id4"] = NULL_CHILD,
    ["rol1Id5"] = NULL_CHILD,
    ["rol1Id6"] = NULL_CHILD,
    }
}

function RedData:isValidChild(nodeData)
    return nodeData ~= NULL_CHILD
end

return RedData