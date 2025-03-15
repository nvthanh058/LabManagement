using Dapper;

using LabManagement.Infrastructure.IRespository;
using LabManagement.Models;
using LabManagement.Infrastructure.IRespository;
using LabManagement.Models;
using System.Data;
using System.Reflection;

namespace LabManagement.Infrastructure.Respository
{
    public class CustomerResposity : ICustomerResposity
    {
        private readonly IDapperServices _services;
        public CustomerResposity(IDapperServices services)
        {
            _services = services;
        }

        public async Task<Customer> Add(Customer model)
        {
            try
            {
                var dbParams = new DynamicParameters();
                Type classType = typeof(Customer);

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

                var query = "INSERT INTO LAB_Customers(" + fieldList.Trim(',') + ",CustomerID)  OUTPUT INSERTED.RecID VALUES(" + fieldData.Trim(',') + ",NewID())";
                model.RecID = Task.FromResult(_services.ExcuteScaler<Customer>(query, dbParams, commandType: CommandType.Text)).Result;

            }
            catch (Exception ex) { }

            return model;
        }

        public async Task<int> Delete(int Id)
        {
            var res = 0;
            try
            {
                var dbParams = new DynamicParameters();
                var query = "DELETE FROM LAB_Customers WHERE RecID=@RecID";

                dbParams.Add("@RecID", Id);

                res = Task.FromResult(_services.ExcuteScaler<Employee>(query, dbParams, commandType: CommandType.Text)).Result;
            }
            catch (Exception ex) { }

            return res;
        }

        public async Task<List<Customer>> GetAll(string CustomerID, string Search)
        {
            var query = @"LAB_GetCustomers";

            var lst = new List<Customer>();

            try
            {
                var dbParams = new DynamicParameters();
                dbParams.Add("@CustomerID", CustomerID);
                dbParams.Add("@Search", Search);
                dbParams.Add("@DATAAREAID", _services.DATAAREAID());

                lst = Task.FromResult(_services.GetAll<Customer>(query, dbParams, commandType: CommandType.StoredProcedure)).Result;
            }
            catch (Exception ex) { }

            return lst;
        }


        public async Task<Customer> Update(Customer model)
        {
            try
            {
                var dbParams = new DynamicParameters();
                Type classType = typeof(Customer);

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

                var query = "UPDATE LAB_Customers SET " + updateData.Trim(',') + " WHERE RecID=@RecID";

                var res = Task.FromResult(_services.ExcuteScaler<Customer>(query, dbParams, commandType: CommandType.Text)).Result;

            }
            catch (Exception ex) { }

            return model;
        }
    }
}
