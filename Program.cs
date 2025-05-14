using AssestManagement.dao;

namespace AssestManagement
{
    internal class Program
    {
        static void Main(string[] args)
        {
            IAssetManagementService service = new AssetManagementServiceImpl();
            AssetUI ui = new AssetUI(service);
            ui.Run();
        }
    }
}
