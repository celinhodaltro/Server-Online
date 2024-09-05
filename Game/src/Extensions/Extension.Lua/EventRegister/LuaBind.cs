using Extension.Lua.EventRegister.Binds;

namespace Extension.Lua.EventRegister;

public static class LuaBind
{
    public static void Setup()
    {
        ItemFunctionBind.Setup();
    }
}