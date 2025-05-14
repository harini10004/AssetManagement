using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AssestManagement.dao;
using AssestManagement.Entity;

namespace AssestManagement
{
    public class AssetUI
    {
        private readonly IAssetManagementService service;

        public AssetUI(IAssetManagementService service)
        {
            this.service = service;
        }

        public void Run()
        {
            while (true)
            {
                Console.WriteLine("\n--- Asset Management Menu ---");
                Console.WriteLine("1. Add Asset");
                Console.WriteLine("2. Update Asset");
                Console.WriteLine("3. Delete Asset");
                Console.WriteLine("4. Allocate Asset");
                Console.WriteLine("5. Deallocate Asset");
                Console.WriteLine("6. Perform Maintenance");
                Console.WriteLine("7. Reserve Asset");
                Console.WriteLine("8. Withdraw Reservation");
                Console.WriteLine("9. Exit");
                Console.Write("Enter your choice: ");

                int choice = Convert.ToInt32(Console.ReadLine());
                try
                {
                    switch (choice)
                    {
                        case 1:
                            AddAsset();
                            break;
                        case 2:
                            UpdateAsset();
                            break;
                        case 3:
                            DeleteAsset();
                            break;
                        case 4:
                            AllocateAsset();
                            break;
                        case 5:
                            DeallocateAsset();
                            break;
                        case 6:
                            PerformMaintenance();
                            break;
                        case 7:
                            ReserveAsset();
                            break;
                        case 8:
                            WithdrawReservation();
                            break;
                        case 9:
                            Environment.Exit(0);
                            break;
                        default:
                            Console.WriteLine("Invalid choice!");
                            break;
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error: " + ex.Message);
                }
            }
        }

        private void AddAsset()
        {
            Console.Write("Enter asset name: ");
            string name = Console.ReadLine();
            Console.Write("Enter asset type: ");
            string type = Console.ReadLine();
            Console.Write("Enter serial number: ");
            string serial = Console.ReadLine();
            Console.Write("Enter purchase date (yyyy-mm-dd): ");
            string purchaseDate = Console.ReadLine();
            Console.Write("Enter location: ");
            string location = Console.ReadLine();
            Console.Write("Enter status: ");
            string status = Console.ReadLine();
            Console.Write("Enter owner employee ID: ");
            int ownerId = Convert.ToInt32(Console.ReadLine());

            Asset asset = new Asset(0, name, type, serial, DateTime.Parse(purchaseDate), location, status, ownerId);
            Console.WriteLine(service.AddAsset(asset) ? "Asset added." : "Failed to add asset.");
        }

        private void UpdateAsset()
        {
            Console.Write("Enter asset ID to update: ");
            int id = Convert.ToInt32(Console.ReadLine());
            Console.Write("Enter new name: ");
            string name = Console.ReadLine();
            Console.Write("Enter new type: ");
            string type = Console.ReadLine();
            Console.Write("Enter new serial number: ");
            string serial = Console.ReadLine();
            Console.Write("Enter new purchase date (yyyy-mm-dd): ");
            string date = Console.ReadLine();
            Console.Write("Enter new location: ");
            string location = Console.ReadLine();
            Console.Write("Enter new status: ");
            string status = Console.ReadLine();
            Console.Write("Enter new owner ID: ");
            int ownerId = Convert.ToInt32(Console.ReadLine());

            Asset asset = new Asset(id, name, type, serial, DateTime.Parse(date), location, status, ownerId);
            Console.WriteLine(service.UpdateAsset(asset) ? "Asset updated." : "Failed to update asset.");
        }

        private void DeleteAsset()
        {
            Console.Write("Enter asset ID to delete: ");
            int id = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine(service.DeleteAsset(id) ? "Asset deleted." : "Failed to delete asset.");
        }

        private void AllocateAsset()
        {
            Console.Write("Enter asset ID to allocate: ");
            int assetId = Convert.ToInt32(Console.ReadLine());
            Console.Write("Enter employee ID: ");
            int empId = Convert.ToInt32(Console.ReadLine());
            Console.Write("Enter allocation date (yyyy-mm-dd): ");
            string date = Console.ReadLine();
            Console.WriteLine(service.AllocateAsset(assetId, empId, date) ? "Asset allocated." : "Allocation failed.");
        }

        private void DeallocateAsset()
        {
            Console.Write("Enter asset ID to deallocate: ");
            int assetId = Convert.ToInt32(Console.ReadLine());
            Console.Write("Enter employee ID: ");
            int empId = Convert.ToInt32(Console.ReadLine());
            Console.Write("Enter return date (yyyy-mm-dd): ");
            string returnDate = Console.ReadLine();
            Console.WriteLine(service.DeallocateAsset(assetId, empId, returnDate) ? "Asset deallocated." : "Deallocation failed.");
        }

        private void PerformMaintenance()
        {
            Console.Write("Enter asset ID for maintenance: ");
            int id = Convert.ToInt32(Console.ReadLine());
            Console.Write("Enter maintenance date (yyyy-mm-dd): ");
            string date = Console.ReadLine();
            Console.Write("Enter description: ");
            string desc = Console.ReadLine();
            Console.Write("Enter cost: ");
            double cost = Convert.ToDouble(Console.ReadLine());
            Console.WriteLine(service.PerformMaintenance(id, date, desc, cost) ? "Maintenance recorded." : "Failed to record maintenance.");
        }

        private void ReserveAsset()
        {
            Console.Write("Enter asset ID to reserve: ");
            int assetId = Convert.ToInt32(Console.ReadLine());
            Console.Write("Enter employee ID: ");
            int empId = Convert.ToInt32(Console.ReadLine());
            Console.Write("Enter reservation date (yyyy-mm-dd): ");
            string resDate = Console.ReadLine();
            Console.Write("Enter start date (yyyy-mm-dd): ");
            string start = Console.ReadLine();
            Console.Write("Enter end date (yyyy-mm-dd): ");
            string end = Console.ReadLine();
            Console.WriteLine(service.ReserveAsset(assetId, empId, resDate, start, end) ? "Asset reserved." : "Reservation failed.");
        }

        private void WithdrawReservation()
        {
            Console.Write("Enter reservation ID to withdraw: ");
            int id = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine(service.WithdrawReservation(id) ? "Reservation withdrawn." : "Failed to withdraw reservation.");
        }
    }
}
