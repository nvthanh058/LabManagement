using Dapper;
using DocumentFormat.OpenXml.Office2010.Excel;
using LabManagement.Infrastructure.IRespository;
using LabManagement.Models;
using System.Data;
using System.Reflection;

namespace LabManagement.Infrastructure.Respository
{
    public class PayCodeResposity : IPayCodeResposity
    {
        private readonly IDapperServices _services;

        public PayCodeResposity(IDapperServices services)
        {
            _services = services;
        }
        public async Task<PayCodeInfo> Add(PayCodeInfo model)
        {
            try
            {
                var dbParams = new DynamicParameters();
                Type classType = typeof(PayCodeInfo);
                string modelName = nameof(PayCodeInfo);

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
                        if (fieldName != "RecID")
                        {
                            fieldList += "" + fieldName + ",";
                            fieldData += "@" + fieldName + ",";
                        }
                        dbParams.Add("@" + fieldName, propertyInfo.GetValue(model));
                    }
                }

                var query = "INSERT INTO PayCodeTable(" + fieldList.Trim(',') + ")  OUTPUT INSERTED.RecID VALUES(" + fieldData.Trim(',') + ")";
                model.RecID = Task.FromResult(_services.ExcuteScaler<PayCodeInfo>(query, dbParams, commandType: CommandType.Text)).Result;
            }
            catch (Exception ex) { }

            return model;
        }

        public async Task<int> Delete(int id)
        {
            var res = 0;
            try
            {
                var dbParams = new DynamicParameters();
                var query = "DELETE FROM PayCodeTable WHERE RecID=@RecID";

                dbParams.Add("@RecID", id);

                res = Task.FromResult(_services.ExcuteScaler<PayCodeInfo>(query, dbParams, commandType: CommandType.Text)).Result;
            }
            catch (Exception ex) { }
            return res;
        }

        public async Task<PayCodeInfo>  Get(int id)
        {
            var query = @"Select * FROM PayCodeTable where RecID=@RecID";
            var model = new PayCodeInfo();
            try
            {
                var dbParams = new DynamicParameters();
                dbParams.Add("@RecID", id);

                model = Task.FromResult(_services.Get<PayCodeInfo>(query, dbParams, commandType: CommandType.Text)).Result;
            }
            catch (Exception ex) { }
            return model;
        }

        public async Task<List<PayCodeInfo>> GetAll(int RecID,string PayCode, int PayType)
        {
            var query = @"WEB_GetPayCodes";
            var lst = new List<PayCodeInfo>();

            try
            {
                var dbParams = new DynamicParameters();
                dbParams.Add("@RecID", RecID);
                dbParams.Add("@PayCode", PayCode);
                dbParams.Add("@PayType", PayType);

                lst = Task.FromResult(_services.GetAll<PayCodeInfo>(query, dbParams, commandType: CommandType.StoredProcedure)).Result;
            }
            catch (Exception ex) { }
            return lst;
        }

        public async Task<PayCodeInfo> Update(PayCodeInfo model)
        {
            try
            {

                var dbParams = new DynamicParameters();
                Type classType = typeof(PayCodeInfo);

                PropertyInfo[] propertyInfos = classType.GetProperties();

                var updateData = "";
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
                        if (fieldName != "RecID")
                        {
                            updateData += fieldName + "=@" + fieldName + ",";
                        }
                        dbParams.Add("@" + fieldName, propertyInfo.GetValue(model));

                    }
                   
                }

                var query = "UPDATE PayCodeTable SET " + updateData.Trim(',') + " WHERE RecID=@RecID";

                var res = Task.FromResult(_services.ExcuteScaler<PayCodeInfo>(query, dbParams, commandType: CommandType.Text)).Result;

            }
            catch (Exception ex) { }

            return model;
        }
    }
}
