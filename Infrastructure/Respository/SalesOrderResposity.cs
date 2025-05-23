using Dapper;
using DocumentFormat.OpenXml.Drawing.Charts;
using DocumentFormat.OpenXml.Office2010.Excel;
using LabManagement.Infrastructure.IRespository;
using LabManagement.Models;
using LabManagement.Models.ProductionModels;
using LabManagement.Models.SaleModels;
using Mscc.GenerativeAI;
using System.Data;
using System.IO.Packaging;
using System.Reflection;

namespace LabManagement.Infrastructure.Respository
{
    public class SalesOrderResposity : ISalesOrderResposity
    {
        private readonly IDapperServices _services;
        public string DateFormat => _services.DateFormat!;
        public SalesOrderResposity(IDapperServices services)
        {
            _services = services;
        }

        public async Task<SalesTable> GetOrderByRecID(int RecID)
        {
            var query = @"Select * FROM LAB_SalesTable where RecID=@RecID";
            var model = new SalesTable();
            try
            {
                var dbParams = new DynamicParameters();
                dbParams.Add("@RecID", RecID);

                model = Task.FromResult(_services.Get<SalesTable>(query, dbParams, commandType: CommandType.Text)).Result;
            }
            catch (Exception ex) { }
            return model;
        }

        public async Task<SalesTable> GetOrderBySalesID(string SalesID)
        {
            var query = @"Select * FROM LAB_SalesTable where SalesID=@SalesID";
            var model = new SalesTable();
            try
            {
                var dbParams = new DynamicParameters();
                dbParams.Add("@SalesID", SalesID);

                model = Task.FromResult(_services.Get<SalesTable>(query, dbParams, commandType: CommandType.Text)).Result;
            }
            catch (Exception ex) { }
            return model;
        }

        public async Task<List<SalesTable>> GetOrders(int RecID, string SalesID, string CustomerID, string Search,
            DateTime? FromDate, DateTime? ToDate,string LabID)
        {
            var query = @"LAB_GetSalesTable";
            var lst = new List<SalesTable>();

            try
            {
                var dbParams = new DynamicParameters();
                dbParams.Add("@RecID", RecID);
                dbParams.Add("@SalesID", SalesID);
                dbParams.Add("@CustomerID", CustomerID);
                dbParams.Add("@Search", Search);
                dbParams.Add("@FromDate", FromDate?.ToString("yyyy-MM-dd"));
                dbParams.Add("@ToDate", ToDate?.ToString("yyyy-MM-dd"));
                dbParams.Add("@DATAAREAID", LabID);

                lst = Task.FromResult(_services.GetAll<SalesTable>(query, dbParams, commandType: CommandType.StoredProcedure)).Result;
            }
            catch (Exception ex) { }
            return lst;
        }

        public async Task<SalesTable> SaveOrder(SalesTable model)
        {
            try
            {
                var dbParams = new DynamicParameters();
               
                dbParams.Add("@RecID", model.RecID);
                dbParams.Add("@SalesID", model.SalesID);
                dbParams.Add("@SalesName", model.SalesName);
                dbParams.Add("@CaseNo", model.CaseNo);
                dbParams.Add("@CaseRef", model.CaseRef);
                dbParams.Add("@CaseDate", model.CaseDate?.ToString("yyyy-MM-dd"));
                dbParams.Add("@ShipDate", model.ShipDate?.ToString("yyyy-MM-dd"));
                dbParams.Add("@CustomerID", model.CustomerID);
                dbParams.Add("@DoctorName", model.DoctorName);
                dbParams.Add("@PatientName", model.PatientName);
                dbParams.Add("@WorkTicketNotes", model.WorkTicketNotes);
                dbParams.Add("@TranslateNotes", model.TranslateNotes);
                dbParams.Add("@SalesStatus", model.SalesStatus);
                dbParams.Add("@PaymentMode", model.PaymentMode);
                dbParams.Add("@ShippingDateRequested", model.ShippingDateRequested?.ToString("yyyy-MM-dd"));
                dbParams.Add("@ShippingDateConfirmed", model.ShippingDateConfirmed?.ToString("yyyy-MM-dd"));
                dbParams.Add("@CustReqShipDate", model.CustReqShipDate?.ToString("yyyy-MM-dd"));
                dbParams.Add("@LabpanNum", model.LabpanNum);
                dbParams.Add("@BoxRef", model.BoxRef);
                dbParams.Add("@DocAccount", model.DocAccount);
                dbParams.Add("@UserID", model.UserID);
                dbParams.Add("@DATAAREAID", model.DATAAREAID);
                dbParams.Add("@Assignment", model.Assignment);

                var query = @"LAB_SaveSalesTable";
                var res = Task.FromResult(_services.ExcuteScalerObject<SalesTable>(query, dbParams, commandType: CommandType.StoredProcedure)).Result;

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
                var query = "LAB_DeleteSaleOrder";               
                dbParams.Add("@RecID", RecID);
               
                res = Task.FromResult(_services.ExcuteScaler<SalesTable>(query, dbParams, commandType: CommandType.StoredProcedure)).Result;
            }
            catch (Exception ex) { }
            return res;
        }

        public async Task<List<SalesLine>> GetOrderLines(string SalesID)
        {
            var query = @"Select * FROM LAB_SalesLine where SalesID=@SalesID";
            var lst = new List<SalesLine>();

            try
            {
                var dbParams = new DynamicParameters();                
                dbParams.Add("@SalesID", SalesID);
              
                lst = Task.FromResult(_services.GetAll<SalesLine>(query, dbParams, commandType: CommandType.Text)).Result;
            }
            catch (Exception ex) { }
            return lst;
        }

        public async Task<SalesLine> GetOrderLine(int RecID)
        {
            var query = @"Select * FROM LAB_SalesLine where RecID=@RecID";
            var model = new SalesLine();
            try
            {
                var dbParams = new DynamicParameters();
                dbParams.Add("@RecID", RecID);

                model = Task.FromResult(_services.Get<SalesLine>(query, dbParams, commandType: CommandType.Text)).Result;
            }
            catch (Exception ex) { }
            return model;
        }

        public async Task<SalesLine> SaveOrderLine(SalesLine model)
        {
            try
            {
                var dbParams = new DynamicParameters();
               
                dbParams.Add("@RecID", model.RecID);
                dbParams.Add("@SalesID", model.SalesID);                
                dbParams.Add("@CaseNo", model.CaseNo);
                dbParams.Add("@SalesStatus", model.SalesStatus);
                dbParams.Add("@PatientName", model.PatientName);
                dbParams.Add("@ItemID", model.ItemID);
                dbParams.Add("@ItemCode", model.ItemCode);
                dbParams.Add("@ItemName", model.ItemName);
                dbParams.Add("@UsTeeth", model.UsTeeth);
                dbParams.Add("@EurTeeth", model.EurTeeth);
                dbParams.Add("@Shade", model.Shade);
                dbParams.Add("@SalesQty", model.SalesQty);
                dbParams.Add("@SalesPrice", model.SalesPrice);
                dbParams.Add("@LineAmount", model.LineAmount);
                dbParams.Add("@CurrencyCode", model.CurrencyCode);
                dbParams.Add("@OtherNotes", model.OtherNotes);
                dbParams.Add("@Rework", model.Rework);
                dbParams.Add("@RemakeCode", model.RemakeCode);
                dbParams.Add("@QtyOrdered", model.QtyOrdered);
                dbParams.Add("@DATAAREAID", model.DATAAREAID);

                var query = @"LAB_SaveSalesLine";
                model.RecID = Task.FromResult(_services.ExcuteScaler<SalesLine>(query, dbParams, commandType: CommandType.StoredProcedure)).Result;
            }
            catch (Exception ex) { }

            return model;
        }

        public async Task<ProdTable> SaveProdOrder(SalesLine SL)
        {
            var model = new ProdTable();
            try
            {
                model.ProdStatus = 0;
                model.ItemID= SL.ItemID;                
                model.ItemName= SL.ItemName;
                model.ItemCode= SL.ItemCode;
                model.UsTeeth = SL.UsTeeth;
                model.EurTeeth = SL.EurTeeth;
                model.Shade = SL.Shade;
                model.Quantity = SL.SalesQty;
                model.SalesID = SL.SalesID;
                model.CaseNo = SL.CaseNo;
                model.CustomerRequests = SL.CustomerRequests;
                model.WTNotes = SL.WorkNotes;
                model.UserID = SL.UserID;
                model.INVENTREFTRANSID = SL.INVENTTRANSID;
                model.DATAAREAID = SL.DATAAREAID;

                var dbParams = new DynamicParameters();
               
                dbParams.Add("@TransDate", DateTime.Now.ToString("yyyy-MM-dd"));
                dbParams.Add("@ItemID", model.ItemID);
                dbParams.Add("@ItemCode", model.ItemCode);
                dbParams.Add("@ItemName", model.ItemName);
                dbParams.Add("@ProdStatus", model.ProdStatus);
                dbParams.Add("@Quantity", model.Quantity);
                dbParams.Add("@INVENTREFTRANSID", model.INVENTREFTRANSID);
                dbParams.Add("@SalesID", model.SalesID);
                dbParams.Add("@CaseNo", model.CaseNo);
                dbParams.Add("@CustomerRequests", model.CustomerRequests);
                dbParams.Add("@WTNotes", model.WTNotes);
                dbParams.Add("@UsTeeth", model.UsTeeth);
                dbParams.Add("@EurTeeth", model.EurTeeth);
                dbParams.Add("@Shade", model.Shade);
                dbParams.Add("@UserID", model.UserID);
                dbParams.Add("@DATAAREAID", model.DATAAREAID);

                var query = @"LAB_SaveProductOrder";
                var RecID = Task.FromResult(_services.ExcuteScalerObject<ProdTable>(query, dbParams, commandType: CommandType.StoredProcedure)).Result;                
                
                if(RecID != null)
                model.RecID = Int32.Parse(RecID.ToString());
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
                var query = "LAB_DeleteSaleLine";
               
                dbParams.Add("@RecID", RecID);
               
                res = Task.FromResult(_services.ExcuteScaler<SalesLine>(query, dbParams, commandType: CommandType.StoredProcedure)).Result;
            }
            catch (Exception ex) { }
            return res;
        }

        public async Task<List<ProdTable>> GetProdOrders(int RecID, string SalesID,string LabID, string FromDate, string ToDate,string Search)
        {
            var query = @"LAB_GetProdOrders";
            
            var lst = new List<ProdTable>();

            try
            {
                var dbParams = new DynamicParameters();
                dbParams.Add("@RecID", RecID);
                dbParams.Add("@SalesID", SalesID);
                dbParams.Add("@LabID", LabID);
                dbParams.Add("@FromDate", FromDate);
                dbParams.Add("@ToDate", ToDate);
                dbParams.Add("@Search", Search);

                lst = Task.FromResult(_services.GetAll<ProdTable>(query, dbParams, commandType: CommandType.StoredProcedure)).Result;
            }
            catch (Exception ex) { }
            return lst;
        }

        public async Task<ProdTable> UpdateProdOrder(ProdTable model)
        {
            try
            {
                              
                var dbParams = new DynamicParameters();
              
                dbParams.Add("@TransDate", DateTime.Now.ToString("yyyy-MM-dd"));
                dbParams.Add("@ItemID", model.ItemID);
                dbParams.Add("@ItemCode", model.ItemCode);
                dbParams.Add("@ItemName", model.ItemName);
                dbParams.Add("@ProdStatus", model.ProdStatus);
                dbParams.Add("@Quantity", model.Quantity);
                dbParams.Add("@INVENTREFTRANSID", model.INVENTREFTRANSID);
                dbParams.Add("@SalesID", model.SalesID);
                dbParams.Add("@CaseNo", model.CaseNo);
                dbParams.Add("@PatientName", model.PatientName);
                dbParams.Add("@WTNotes", model.WTNotes);
                dbParams.Add("@UsTeeth", model.UsTeeth);
                dbParams.Add("@EurTeeth", model.EurTeeth);
                dbParams.Add("@Shade", model.Shade);
                dbParams.Add("@UserID", model.UserID);
                dbParams.Add("@DATAAREAID", model.DATAAREAID);

                var query = @"LAB_SaveProductOrder";
                var RecID = Task.FromResult(_services.ExcuteScalerObject<ProdTable>(query, dbParams, commandType: CommandType.StoredProcedure)).Result;

            }
            catch (Exception ex) { }

            return model;
        }

        public async Task<int> DeleteProdOrder(int RecID)
        {
            var res = 0;
            try
            {
                var dbParams = new DynamicParameters();
                var query = "LAB_DeleteProdTable";
               

                dbParams.Add("@RecID", RecID);
              
                res = Task.FromResult(_services.ExcuteScaler<ProdTable>(query, dbParams, commandType: CommandType.StoredProcedure)).Result;
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

        public async Task<int> AddFile(string OrderID, string FilePath,int FileType)
        {
            var res = 0;
            try
            {
                var dbParams = new DynamicParameters();
                var query = "INSERT INTO LAB_OrderFiles(OrderID,FileType,FilePath) values(@OrderID,@FileType,@FilePath)";

                dbParams.Add("@OrderID", OrderID);
                dbParams.Add("@FileType", FileType);
                dbParams.Add("@FilePath", FilePath);

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

        public async Task<SequenceInfo> GetSequenceNum(string SeqID)
        {
            var query = @"SYS_GetSequence";
            var model = new SequenceInfo();
            try
            {
                var dbParams = new DynamicParameters();
               
                dbParams.Add("@SeqID", SeqID);
                model = Task.FromResult(_services.Get<SequenceInfo>(query, dbParams, commandType: CommandType.StoredProcedure)).Result;
            }
            catch (Exception ex) { }
            return model;
        }

        public async Task<List<AssignmentInfo>> GetAssignment()
        {
            var query = @"Select * FROM LAB_Assignment";

            var lst = new List<AssignmentInfo>();

            try
            {
               
                lst = Task.FromResult(_services.GetAll<AssignmentInfo>(query, null, commandType: CommandType.Text)).Result;
            }
            catch (Exception ex) { }
            return lst;
        }

        public async Task<int> SaveSalesLog(SalesLog log)
        {
            var res = 0;
            try
            {
                var dbParams = new DynamicParameters();
                var query = @"INSERT INTO LAB_IMPORTSALESLOG(PackageID,CaseNo,TransDate,Imported,UserID) 
                                    values(@PackageID,@CaseNo,@TransDate,@Imported,@UserID)";

                dbParams.Add("@PackageID", log.PackageID);
                dbParams.Add("@CaseNo", log.CaseNo);
                dbParams.Add("@TransDate", log.TransDate?.ToString("yyyy-MM-dd"));
                dbParams.Add("@Imported", log.Imported);
                dbParams.Add("@UserID", log.UserID);

                res = Task.FromResult(_services.ExcuteScaler<SalesLog>(query, dbParams, commandType: CommandType.Text)).Result;
            }
            catch (Exception ex) { }
            return res;
        }

        public async Task<List<SalesLog>> GetSalesLog(string PackageID)
        {
            var query = @"Select * FROM LAB_IMPORTSALESLOG WHERE PackageID=@PackageID";
            var lst = new List<SalesLog>();

            try
            {
                var dbParams = new DynamicParameters();
                dbParams.Add("@PackageID", PackageID);
               
                lst = Task.FromResult(_services.GetAll<SalesLog>(query, dbParams, commandType: CommandType.Text)).Result;
            }
            catch (Exception ex) { }
            return lst;
        }

        public async Task<List<SalesTable>> GetOrdersByPackageID(string PackageID)
        {
            var query = @"LAB_GetSalesTableImported";
            var lst = new List<SalesTable>();

            try
            {
                var dbParams = new DynamicParameters();
                dbParams.Add("@PackageID", PackageID);              
                lst = Task.FromResult(_services.GetAll<SalesTable>(query, dbParams, commandType: CommandType.StoredProcedure)).Result;
            }
            catch (Exception ex) { }
            return lst;
        }
    }
}
