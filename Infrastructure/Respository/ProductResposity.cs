using Dapper;
using DocumentFormat.OpenXml.Office2010.Excel;
using LabManagement.Infrastructure.IRespository;
using LabManagement.Models;
using System.Data;
using System.Diagnostics.Eventing.Reader;
using System.Reflection;

namespace LabManagement.Infrastructure.Respository
{
    public class ProductResposity : IProductResposity
    {

        private readonly IDapperServices _services;
        private readonly string TableName= "LAB_Products";
        public ProductResposity(IDapperServices services)
        {
            _services = services;
        }

        public async Task<Product> Get(int RecID)
        {
            var query = @"Select * FROM " + TableName + " where RecID=@RecID";
            query += " AND DATAAREAID=@DATAAREAID";
            var model = new Product();
            try
            {
                var dbParams = new DynamicParameters();
                dbParams.Add("@RecID", RecID);
                dbParams.Add("@DATAAREAID", _services!.DATAAREAID());

                model = Task.FromResult(_services.Get<Product>(query, dbParams, commandType: CommandType.Text)).Result;
            }
            catch (Exception ex) { }
            return model;
        }

        public async Task<List<Product>> GetAll(int RecID, string ItemID, string Search)
        {
            var query = @"LAB_GetProducts";
            var lst = new List<Product>();

            try
            {
                var dbParams = new DynamicParameters();
                dbParams.Add("@RecID", RecID);
                dbParams.Add("@ItemID", ItemID);
                dbParams.Add("@Search", Search);
                dbParams.Add("@DATAAREAID", _services!.DATAAREAID());

                lst = Task.FromResult(_services.GetAll<Product>(query, dbParams, commandType: CommandType.StoredProcedure)).Result;
            }
            catch (Exception ex) { }
            return lst;
        }

        public async Task<Product> Save(Product model)
        {
            try
            {
                model.DATAAREAID = _services!.DATAAREAID();

                var dbParams = new DynamicParameters();
                Type classType = typeof(Product);
                
                PropertyInfo[] propertyInfos = classType.GetProperties();

                var fieldList = "";
                var fieldData = "";
                foreach (PropertyInfo propertyInfo in propertyInfos)
                {
                    var type = propertyInfo.PropertyType.Name;
                    var fieldName = propertyInfo.Name;
                    var attribute = (ModelAttribute)propertyInfo.GetCustomAttribute(typeof(ModelAttribute));

                    var fieldDesc = "";
                    if (attribute != null)
                    {
                        fieldDesc = attribute.Description;
                    }

                    if (!fieldDesc.Equals("NotTableField"))
                    {                       
                        fieldList += "" + fieldName + ",";
                        fieldData += "@" + fieldName + ",";                      
                        dbParams.Add("@" + fieldName, propertyInfo.GetValue(model));
                    }
                }

                var query = @"LAB_SaveProducts";
                model.RecID = Task.FromResult(_services.ExcuteScaler<Product>(query, dbParams, commandType: CommandType.StoredProcedure)).Result;
            }
            catch (Exception ex) { }

            return model;
        }
        public async Task<int> Delete(int RecID)
        {
            var res = 0;
            try
            {
                var dbParams = new DynamicParameters();
                var query = "DELETE FROM " + TableName + " WHERE RecID=@RecID";
                query += " AND DATAAREAID=@DATAAREAID";

                dbParams.Add("@RecID", RecID);
                dbParams.Add("@DATAAREAID", _services!.DATAAREAID());

                res = Task.FromResult(_services.ExcuteScaler<Product>(query, dbParams, commandType: CommandType.Text)).Result;
            }
            catch (Exception ex) { }
            return res;
        }

        public async Task<List<TablePrice>> GetAllPrice(int RecID,string PriceID, string ItemID, string Search)
        {
            var query = @"LAB_GetPriceList";         
            var lst = new List<TablePrice>();

            try
            {
                var dbParams = new DynamicParameters();
                dbParams.Add("@RecID", RecID);
                dbParams.Add("@PriceID", PriceID);
                dbParams.Add("@ItemID", ItemID);
                dbParams.Add("@Search", Search);
                dbParams.Add("@DATAAREAID", _services!.DATAAREAID());

                lst = Task.FromResult(_services.GetAll<TablePrice>(query, dbParams, commandType: CommandType.StoredProcedure)).Result;
            }
            catch (Exception ex) { }
            return lst;
        }

        public async Task<TablePrice> GetPrice(int RecID)
        {
            var query = @"Select * FROM LAB_PriceList where RecID=@RecID";
            query += " AND DATAAREAID=@DATAAREAID";
            var model = new TablePrice();
            try
            {
                var dbParams = new DynamicParameters();
                dbParams.Add("@RecID", RecID);
                dbParams.Add("@DATAAREAID", _services!.DATAAREAID());

                model = Task.FromResult(_services.Get<TablePrice>(query, dbParams, commandType: CommandType.Text)).Result;
            }
            catch (Exception ex) { }
            return model;
        }

        public async Task<TablePrice> SavePrice(TablePrice model)
        {            
            try
            {
               
                var dbParams = new DynamicParameters();
                
                dbParams.Add("@RecID", model.RecID);
                dbParams.Add("@PriceID", model.PriceID);
                dbParams.Add("@ItemID", model.ItemID);
                dbParams.Add("@UnitPrice", model.UnitPrice);
                dbParams.Add("@FromDate", model.FromDate?.ToString("yyyy-MM-dd"));
                dbParams.Add("@UserRef", model.UserRef);
                dbParams.Add("@DATAAREAID", _services!.DATAAREAID());

                var query = @"LAB_SavePriceList";
                model.RecID = Task.FromResult(_services.ExcuteScaler<TablePrice>(query, dbParams, commandType: CommandType.StoredProcedure)).Result;
            }
            catch (Exception ex) { }

            return model;
        }

        public async Task<int> DeletePrice(int RecID)
        {
            var res = 0;
            try
            {
                var dbParams = new DynamicParameters();
                var query = "DELETE FROM LAB_PriceList WHERE RecID=@RecID";
                query += " AND DATAAREAID=@DATAAREAID";

                dbParams.Add("@RecID", RecID);
                dbParams.Add("@DATAAREAID", _services!.DATAAREAID());

                res = Task.FromResult(_services.ExcuteScaler<TablePrice>(query, dbParams, commandType: CommandType.Text)).Result;
            }
            catch (Exception ex) { }
            return res;
        }

        public async Task<List<TablePriceMaster>> GetPriceListMaster(int RecID, string PriceID, string Search)
        {
            var query = @"Select * FROM LAB_PriceListMaster WHERE 1=1";
            query += " AND (@RecID=0 OR RecID=@RecID)";
            query += " AND (@PriceID='' OR PriceID=@PriceID)";
            query += " AND (@Search='' OR PriceName LIKE '%@Search%')";
            var lst = new List<TablePriceMaster>();

            try
            {
                var dbParams = new DynamicParameters();
                dbParams.Add("@RecID", RecID);
                dbParams.Add("@PriceID", PriceID);
                dbParams.Add("@Search", Search);
                
                lst = Task.FromResult(_services.GetAll<TablePriceMaster>(query, dbParams, commandType: CommandType.Text)).Result;
            }
            catch (Exception ex) { }
            return lst;
        }

        public async Task<TablePriceMaster> SavePriceListMaster(TablePriceMaster model)
        {
            try
            {
                var dbParams = new DynamicParameters();
                dbParams.Add("@RecID", model.RecID);                
                dbParams.Add("@PriceName", model.PriceName);

                if (model.RecID == 0)
                {
                    var query = "INSERT INTO LAB_PriceListMaster(PriceID,PriceName)  OUTPUT INSERTED.RecID VALUES(NewID(),@PriceName)";
                    model.RecID = Task.FromResult(_services.ExcuteScaler<TablePriceMaster>(query, dbParams, commandType: CommandType.Text)).Result;
                }

                else
                {
                    var query = "UPDATE LAB_PriceListMaster SET PriceName=@PriceName WHERE RecID=@RecID";
                    var res = Task.FromResult(_services.ExcuteScaler<TablePriceMaster>(query, dbParams, commandType: CommandType.Text)).Result;
                }

            }
            catch (Exception ex) { }

            return model;
        }

        public async Task<int> DeletePriceListMaster(int RecID)
        {
            var res = 0;
            try
            {
                var dbParams = new DynamicParameters();
                var query = "DELETE FROM LAB_PriceListMaster WHERE RecID=@RecID";
                query += " AND DATAAREAID=@DATAAREAID";

                dbParams.Add("@RecID", RecID);
                dbParams.Add("@DATAAREAID", _services!.DATAAREAID());

                res = Task.FromResult(_services.ExcuteScaler<TablePriceMaster>(query, dbParams, commandType: CommandType.Text)).Result;
            }
            catch (Exception ex) { }
            return res;
        }
    }
}
