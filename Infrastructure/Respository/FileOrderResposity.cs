using Dapper;
using LabManagement.Infrastructure.IRespository;
using LabManagement.Models;
using System.Data;

namespace LabManagement.Infrastructure.Respository
{
    public class FileOrderResposity : IFileOrderResposity
    {
        private readonly IDapperServices _services;
        public FileOrderResposity(IDapperServices services)
        {
            _services = services;
        }

        public async Task<CaseOrder> GetOrder(int RecID)
        {
            var query = @"Select * FROM LAB_CaseOrders where RecID=@RecID";
            var model = new CaseOrder();
            try
            {
                var dbParams = new DynamicParameters();
                dbParams.Add("@RecID", RecID);

                model = Task.FromResult(_services.Get<CaseOrder>(query, dbParams, commandType: CommandType.Text)).Result;
            }
            catch (Exception ex) { }
            return model;
        }

        public async Task<List<CaseOrder>> GetOrders(int RecID, string OrderID, string UserID, string Search, DateTime? FromDate, DateTime? ToDate, string LoginUserID)
        {
            var query = @"LAB_GetCaseOrders";
            var lst = new List<CaseOrder>();

            try
            {
                var dbParams = new DynamicParameters();
                dbParams.Add("@RecID", RecID);
                dbParams.Add("@OrderID", OrderID);
                dbParams.Add("@UserID", UserID);
                dbParams.Add("@Search", Search);
                dbParams.Add("@FromDate", FromDate?.ToString("yyyy-MM-dd"));
                dbParams.Add("@ToDate", ToDate?.ToString("yyyy-MM-dd"));

                dbParams.Add("@DATAAREAID", _services!.DATAAREAID());
                dbParams.Add("@LoginUserID", LoginUserID);

                lst = Task.FromResult(_services.GetAll<CaseOrder>(query, dbParams, commandType: CommandType.StoredProcedure)).Result;
            }
            catch (Exception ex) { }
            return lst;
        }

        public async Task<CaseOrder> SaveOrder(CaseOrder model)
        {
            try
            {
                var dbParams = new DynamicParameters();

                model.DATAAREAID = _services!.DATAAREAID();

                dbParams.Add("@RecID", model.RecID);
                dbParams.Add("@OrderID", model.OrderID);
                dbParams.Add("@OrderNo", model.OrderNo);
                dbParams.Add("@OrderDate", model.OrderDate);
                dbParams.Add("@PatientName", model.PatientName);
                dbParams.Add("@DoctorName", model.DoctorName);
                dbParams.Add("@WorkNotes", model.WorkNotes);
                dbParams.Add("@UserID", model.UserID);
                dbParams.Add("@DATAAREAID", model.DATAAREAID);

                var query = @"LAB_SaveCasesOrder";
                var res = Task.FromResult(_services.ExcuteScalerObject<CaseOrder>(query, dbParams, commandType: CommandType.StoredProcedure)).Result;

                if (res != null)
                    model.RecID = Int32.Parse(res.ToString());
            }
            catch (Exception ex) { }

            return model;
        }

        public async Task<int> DeleteOrder(int RecID)
        {
            var res = 0;
            try
            {
                var dbParams = new DynamicParameters();
                var query = "DELETE FROM LAB_CaseOrders WHERE RecID=@RecID";
                query += " AND DATAAREAID=@DATAAREAID";

                dbParams.Add("@RecID", RecID);
                dbParams.Add("@DATAAREAID", _services!.DATAAREAID());

                res = Task.FromResult(_services.ExcuteScaler<CaseOrder>(query, dbParams, commandType: CommandType.Text)).Result;
            }
            catch (Exception ex) { }
            return res;
        }




        public async Task<List<OrderFile>> GetFiles(string OrderID, int FileType)
        {
            var query = @"Select * FROM LAB_OrderFiles WHERE OrderID=@OrderID AND FileType=@FileType";

            var lst = new List<OrderFile>();

            try
            {
                var dbParams = new DynamicParameters();
                dbParams.Add("@OrderID", OrderID);
                dbParams.Add("@FileType", FileType);

                lst = Task.FromResult(_services.GetAll<OrderFile>(query, dbParams, commandType: CommandType.Text)).Result;
            }
            catch (Exception ex) { }
            return lst;
        }

        public async Task<int> AddFile(string OrderID, string FilePath, int FileType, string UserID)
        {
            var res = 0;
            try
            {
                var dbParams = new DynamicParameters();
                var query = "INSERT INTO LAB_OrderFiles(OrderID,FileType,FilePath,UserID,UploadDate) values(@OrderID,@FileType,@FilePath,@UserID,@UploadDate)";

                dbParams.Add("@OrderID", OrderID);
                dbParams.Add("@FileType", FileType);
                dbParams.Add("@FilePath", FilePath);
                dbParams.Add("@UserID", UserID);
                dbParams.Add("@UploadDate", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));

                res = Task.FromResult(_services.ExcuteScaler<OrderFile>(query, dbParams, commandType: CommandType.Text)).Result;
            }
            catch (Exception ex) { }
            return res;
        }

        public async Task<int> DeleteFile(int RecID)
        {
            var res = 0;
            try
            {
                var dbParams = new DynamicParameters();
                var query = "DELETE FROM LAB_OrderFiles WHERE RecID=@RecID";

                dbParams.Add("@RecID", RecID);

                res = Task.FromResult(_services.ExcuteScaler<OrderFile>(query, dbParams, commandType: CommandType.Text)).Result;
            }
            catch (Exception ex) { }
            return res;
        }

        public Task<int> DeleteProdOrder(int RecID)
        {
            throw new NotImplementedException();
        }

        public async Task<int> SaveDownloadStatus(string OrderID, string UserID, int FileRecID, int DownloadStatus)
        {
            var res = 0;
            try
            {
                var dbParams = new DynamicParameters();
                var query = "LAB_SaveOrderDownloadStatus";

                dbParams.Add("@OrderID", OrderID);
                dbParams.Add("@FileRecID", FileRecID);
                dbParams.Add("@UserID", UserID);
                dbParams.Add("@DownloadStatus", DownloadStatus);


                res = Task.FromResult(_services.ExcuteScaler<OrderFile>(query, dbParams, commandType: CommandType.StoredProcedure)).Result;
            }
            catch (Exception ex) { }
            return res;
        }

        public async Task<List<CaseOrderLine>> GetOrderLines(string OrderID)
        {
            var query = @"Select * FROM LAB_CaseOrderLines where OrderID=@OrderID";
            var lst = new List<CaseOrderLine>();

            try
            {
                var dbParams = new DynamicParameters();
                dbParams.Add("@OrderID", OrderID);

                lst = Task.FromResult(_services.GetAll<CaseOrderLine>(query, dbParams, commandType: CommandType.Text)).Result;
            }
            catch (Exception ex) { }
            return lst;
        }

        public async Task<CaseOrderLine> GetOrderLine(int RecID)
        {
            var query = @"Select * FROM LAB_CaseOrderLines where RecID=@RecID";
            var model = new CaseOrderLine();
            try
            {
                var dbParams = new DynamicParameters();
                dbParams.Add("@RecID", RecID);

                model = Task.FromResult(_services.Get<CaseOrderLine>(query, dbParams, commandType: CommandType.Text)).Result;
            }
            catch (Exception ex) { }
            return model;
        }

        public async Task<CaseOrderLine> SaveOrderLine(CaseOrderLine model)
        {
            try
            {
                var dbParams = new DynamicParameters();

                model.DATAAREAID = _services!.DATAAREAID();

                dbParams.Add("@RecID", model.RecID);
                dbParams.Add("@OrderID", model.OrderID);                            
                dbParams.Add("@ItemID", model.ItemID);
                dbParams.Add("@ItemCode", model.ItemCode);
                dbParams.Add("@ItemName", model.ItemName);
                dbParams.Add("@UsTeeth", model.UsTeeth);
                dbParams.Add("@EurTeeth", model.EurTeeth);
                dbParams.Add("@Shade", model.Shade);
                dbParams.Add("@Quantity", model.Quantity);              
                dbParams.Add("@OtherNotes", model.OtherNotes);         
                dbParams.Add("@DATAAREAID", model.DATAAREAID);

                var query = @"LAB_SaveCaseOrderLine";
                model.RecID = Task.FromResult(_services.ExcuteScaler<CaseOrderLine>(query, dbParams, commandType: CommandType.StoredProcedure)).Result;
            }
            catch (Exception ex) { }

            return model;
        }

        public async Task<int> DeleteOrderLine(int RecID)
        {
            var res = 0;
            try
            {
                var dbParams = new DynamicParameters();
                var query = "DELETE FROM LAB_CaseOrderLines WHERE RecID=@RecID";
                query += " AND DATAAREAID=@DATAAREAID";

                dbParams.Add("@RecID", RecID);
                dbParams.Add("@DATAAREAID", _services!.DATAAREAID());

                res = Task.FromResult(_services.ExcuteScaler<CaseOrderLine>(query, dbParams, commandType: CommandType.Text)).Result;
            }
            catch (Exception ex) { }
            return res;
        }
    }
}
