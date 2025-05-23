using Dapper;
using LabManagement.Infrastructure.IRespository;
using LabManagement.Models.ProductionModels;
using LabManagement.Models.PurchasingModels;
using LabManagement.Models.SaleModels;
using System.Data;

namespace LabManagement.Infrastructure.Respository
{
    public class PurchasingResposity : IPurchasingResposity
    {
        private readonly IDapperServices _services;
        public PurchasingResposity(IDapperServices services)
        {
            _services = services;
        }

        public async Task<int> DeleteVendor(int Id)
        {
            var res = 0;
            try
            {
                var dbParams = new DynamicParameters();
                var query = "Delete FROM PUR_VENDORS WHERE RecID=@RecID";
                dbParams.Add("@RecID", Id);

                res = Task.FromResult(_services.ExcuteScaler<SalesLine>(query, dbParams, commandType: CommandType.StoredProcedure)).Result;
            }
            catch (Exception ex) { }
            return res;
        }

        public async Task<List<Vendor>> GetVendors(string VendorID, string Search)
        {
            var query = @"PUR_GetVendors";

            var lst = new List<Vendor>();

            try
            {
                var dbParams = new DynamicParameters();
                dbParams.Add("@VendorID", VendorID);
                dbParams.Add("@Search", Search);
               
                lst = Task.FromResult(_services.GetAll<Vendor>(query, dbParams, commandType: CommandType.StoredProcedure)).Result;
            }
            catch (Exception ex) { }

            return lst;
        }

        public async Task<Vendor> SaveVendor(Vendor model)
        {            
            try
            {
                var dbParams = new DynamicParameters();
                var query = @"PUR_SaveVendor";

                dbParams.Add("@RecID", model.RecID);
                dbParams.Add("@VendorID", model.VendorID);
                dbParams.Add("@VendorName", model.VendorName);
                dbParams.Add("@MobilePhone", model.MobilePhone);
                dbParams.Add("@GroupID", model.GroupID);
                dbParams.Add("@Address", model.Address);
                dbParams.Add("@Attention", model.Attention);
                dbParams.Add("@Tel", model.Tel);
                dbParams.Add("@Email", model.Email);
                dbParams.Add("@PaymentTerm", model.PaymentTerm);
                dbParams.Add("@TradingTerm", model.TradingTerm);
                dbParams.Add("@CurrencyCode", model.CurrencyCode);
                dbParams.Add("@LabID", _services.DATAAREAID());
                dbParams.Add("@UserID", model.UserID);
                dbParams.Add("@Remarks", model.Remarks);

                var RecID = Task.FromResult(_services.ExcuteScalerObject<Vendor>(query, dbParams, commandType: CommandType.StoredProcedure)).Result;

                if (RecID != null)
                    model.RecID = Int32.Parse(RecID.ToString());
            }
            catch (Exception ex) { }
            return model;
        }
    }
}
