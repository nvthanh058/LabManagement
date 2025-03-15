using Dapper;
using DocumentFormat.OpenXml.Office2010.Excel;
using LabManagement.Infrastructure.IRespository;
using LabManagement.Models;
using System.Data;
using System.Reflection;

namespace LabManagement.Infrastructure.Respository
{
    public class FixedPayCodeResposity : IFixedPayCodeResposity
    {
        private readonly IDapperServices _services;
        private readonly string TableName = "EmplFixedPayCode";
        public FixedPayCodeResposity(IDapperServices services)
        {
            _services = services;
        }

        public async Task<FixedPayCode> Add(FixedPayCode model)
        {
            try
            {
                var dbParams = new DynamicParameters();
                Type classType = typeof(FixedPayCode);

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

                var query = "INSERT INTO " + TableName + "(" + fieldList.Trim(',') + ")  OUTPUT INSERTED.RecID VALUES(" + fieldData.Trim(',') + ")";


                model.RecID = Task.FromResult(_services.ExcuteScaler<FixedPayCode>(query, dbParams, commandType: CommandType.Text)).Result;
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
                var query = "DELETE FROM " + TableName + " WHERE RecID=@RecID";

                dbParams.Add("@RecID", id);

                res = Task.FromResult(_services.ExcuteScaler<FixedPayCode>(query, dbParams, commandType: CommandType.Text)).Result;
            }
            catch (Exception ex) { }

            return res;
        }

        public async Task<FixedPayCode> Get(int id)
        {
            var query = @"SELECT F.*,E.FullName,P.DESCRIPTION FROM EmplFixedPayCode F
	            INNER JOIN EmplTable E ON F.EmplID=E.EmplID
	            INNER JOIN PayCodeTable P ON P.PAYCODE=F.PayCode
	            where F.RecID=@RecID";

            var model = new FixedPayCode();
            try
            {
                var dbParams = new DynamicParameters();
                dbParams.Add("@RecID", id);

                model = Task.FromResult(_services.Get<FixedPayCode>(query, dbParams, commandType: CommandType.Text)).Result;
            }
            catch (Exception ex) { }
            return model;
        }

        public async Task<List<FixedPayCode>> GetAll(string EmplID, string Search)
        {
            var query = @"WEB_GetFixedPayCode";
          
            var lst = new List<FixedPayCode>();

            try
            {
                var dbParams = new DynamicParameters();
                dbParams.Add("@EmplID", EmplID);
                dbParams.Add("@Search", Search);
                lst = Task.FromResult(_services.GetAll<FixedPayCode>(query, dbParams, commandType: CommandType.StoredProcedure)).Result;
            }
            catch (Exception ex) { }
            return lst;
        }

        public async Task<FixedPayCode> Update(FixedPayCode model)
        {
            try
            {

                var dbParams = new DynamicParameters();
                Type classType = typeof(FixedPayCode);

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

                var query = "UPDATE " + TableName + " SET " + updateData.Trim(',') + " WHERE RecID=@RecID";

                var res = Task.FromResult(_services.ExcuteScaler<FixedPayCode>(query, dbParams, commandType: CommandType.Text)).Result;

            }
            catch (Exception ex) { }

            return model;
        }
    }
}
