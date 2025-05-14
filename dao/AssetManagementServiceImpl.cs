using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using AssestManagement.Entity;

namespace AssestManagement.dao
{
    public class AssetManagementServiceImpl : IAssetManagementService
    {
        public bool AddAsset(Asset asset)
        {
            SqlConnection conn = DBUtility.GetConnection();
            try
            {
                string query = "INSERT INTO Assets (name, type, serial_number, purchase_date, location, status, owner_id) " +
                               "VALUES (@name, @type, @serialNumber, @purchaseDate, @location, @status, @ownerId)";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@name", asset.Name);
                cmd.Parameters.AddWithValue("@type", asset.Type);
                cmd.Parameters.AddWithValue("@serialNumber", asset.SerialNumber);
                cmd.Parameters.AddWithValue("@purchaseDate", asset.PurchaseDate);
                cmd.Parameters.AddWithValue("@location", asset.Location);
                cmd.Parameters.AddWithValue("@status", asset.Status);
                cmd.Parameters.AddWithValue("@ownerId", asset.OwnerId);

                int rows = cmd.ExecuteNonQuery();
                return rows > 0;
            }
            catch (SqlException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                return false;
                throw new AssetException($"Error Adding Asset: {ex.Message}");
            }

            finally
            {
                DBUtility.CloseDbConnection(conn);
            }
        }

        public bool UpdateAsset(Asset asset)
        {
            SqlConnection conn = DBUtility.GetConnection();
            try
            {
                string query = "UPDATE Assets SET name=@name, type=@type, serial_number=@serialNumber, " +
                               "purchase_date=@purchaseDate, location=@location, status=@status, owner_id=@ownerId " +
                               "WHERE asset_id=@assetId";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@name", asset.Name);
                cmd.Parameters.AddWithValue("@type", asset.Type);
                cmd.Parameters.AddWithValue("@serialNumber", asset.SerialNumber);
                cmd.Parameters.AddWithValue("@purchaseDate", asset.PurchaseDate);
                cmd.Parameters.AddWithValue("@location", asset.Location);
                cmd.Parameters.AddWithValue("@status", asset.Status);
                cmd.Parameters.AddWithValue("@ownerId", asset.OwnerId);
                cmd.Parameters.AddWithValue("@assetId", asset.AssetId);

                int rows = cmd.ExecuteNonQuery();
                return rows > 0;
            }
            catch (SqlException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error Updating Asset: {ex.Message}");
                return false;
            }
            finally
            {
                DBUtility.CloseDbConnection(conn);
            }
        }

        public bool DeleteAsset(int assetId)
        {
            SqlConnection conn = DBUtility.GetConnection();
            try
            {
                string query = "DELETE FROM Assets WHERE asset_id = @assetId";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@assetId", assetId);

                int rows = cmd.ExecuteNonQuery();
                return rows > 0;
            }
            catch (SqlException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error Deleting Asset: {ex.Message}");
                return false;
            }
            finally
            {
                DBUtility.CloseDbConnection(conn);
            }
        }

        public bool AllocateAsset(int assetId, int employeeId, string allocationDate)
        {
            SqlConnection conn = DBUtility.GetConnection();
            try
            {
                string query = "INSERT INTO AssetAllocations (asset_id, employee_id, allocation_date) " +
                               "VALUES (@assetId, @employeeId, @allocationDate)";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@assetId", assetId);
                cmd.Parameters.AddWithValue("@employeeId", employeeId);
                cmd.Parameters.AddWithValue("@allocationDate", DateTime.Parse(allocationDate));

                int rows = cmd.ExecuteNonQuery();
                return rows > 0;
            }
            catch (SqlException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                return false;
                throw new AssetException($"Error Allocating Asset: {ex.Message}");
            }
            finally
            {
                DBUtility.CloseDbConnection(conn);
            }
        }

        public bool DeallocateAsset(int assetId, int employeeId, string returnDate)
        {
            SqlConnection conn = DBUtility.GetConnection();
            try
            {
                string query = @"UPDATE AssetAllocations
                             SET return_date = @returnDate
                             WHERE asset_id = @assetId
                               AND employee_id = @employeeId
                               AND return_date IS NULL";

                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@assetId", assetId);
                cmd.Parameters.AddWithValue("@employeeId", employeeId);
                cmd.Parameters.AddWithValue("@returnDate", returnDate);

                int rowsAffected = cmd.ExecuteNonQuery();

                if (rowsAffected == 0)
                {
                    Console.WriteLine("No active allocation found for given asset and employee.");
                    return false;
                }

                return true;
            
            }
            catch (SqlException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                return false;
                throw new AssetException($"Error Deallocating Asset: {ex.Message}");
            }
            finally
            {
                DBUtility.CloseDbConnection(conn);
            }
        }

        public bool PerformMaintenance(int assetId, string maintenanceDate, string description, double cost)
        {
            SqlConnection conn = DBUtility.GetConnection();
            try
            {
                string query = "INSERT INTO MaintenanceRecords (asset_id, maintenance_date, description, cost) " +
                               "VALUES (@assetId, @maintenanceDate, @description, @cost)";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@assetId", assetId);
                cmd.Parameters.AddWithValue("@maintenanceDate", DateTime.Parse(maintenanceDate));
                cmd.Parameters.AddWithValue("@description", description);
                cmd.Parameters.AddWithValue("@cost", cost);

                int rows = cmd.ExecuteNonQuery();
                return rows > 0;
            }
            catch (SqlException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                return false;
                throw new AssetException($"Error Performing Maintenance: {ex.Message}");
            }
            finally
            {
                DBUtility.CloseDbConnection(conn);
            }
        }

        public bool ReserveAsset(int assetId, int employeeId, string reservationDate, string startDate, string endDate)
        {
            SqlConnection conn = DBUtility.GetConnection();
            try
            {
                string query = "INSERT INTO  Reservations (asset_id, employee_id, reservation_date, start_date, end_date, status) " +
                               "VALUES (@assetId, @employeeId, @reservationDate, @startDate, @endDate, @status)";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@assetId", assetId);
                cmd.Parameters.AddWithValue("@employeeId", employeeId);
                cmd.Parameters.AddWithValue("@reservationDate", DateTime.Parse(reservationDate));
                cmd.Parameters.AddWithValue("@startDate", DateTime.Parse(startDate));
                cmd.Parameters.AddWithValue("@endDate", DateTime.Parse(endDate));
                cmd.Parameters.AddWithValue("@status", "Pending");

                int rows = cmd.ExecuteNonQuery();
                return rows > 0;
            }
            catch (SqlException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                return false;
                throw new AssetException($"Error Reserving Asset: {ex.Message}");
            }
            finally
            {
                DBUtility.CloseDbConnection(conn);
            }
        }

        public bool WithdrawReservation(int reservationId)
        {
            SqlConnection conn = DBUtility.GetConnection();
            try
            {
                string query = "UPDATE reservations SET status = 'Canceled' WHERE reservation_id = @reservationId";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@reservationId", reservationId);

                int rows = cmd.ExecuteNonQuery();
                return rows > 0;
            }
            catch (SqlException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                return false;
                throw new AssetException($"Error Withdrawing Reservation: {ex.Message}");
            }
            finally
            {
                DBUtility.CloseDbConnection(conn);
            }
        }
    }
}
