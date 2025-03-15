using ClosedXML;
using Dapper;
using DocumentFormat.OpenXml.Bibliography;
using DocumentFormat.OpenXml.Wordprocessing;
using LabManagement.Infrastructure.IRespository;
using LabManagement.Models;
using System.Data;
using System.Reflection;
using System.Reflection.Metadata;

namespace LabManagement.Infrastructure.Respository
{
   

    public class EmployeeResposity : IEmployeeResposity
    {
        private readonly IDapperServices _services;
        public EmployeeResposity(IDapperServices services)
        {
            _services = services;
        }

        public async Task<List<Employee>> GetAll(string EmplID, string Search)
        {
            var query = @"LAB_GetFullEmployees";

            var lst = new List<Employee>();

            try
            {
                var dbParams = new DynamicParameters();
                dbParams.Add("@EmplID", EmplID);
                dbParams.Add("@Search", Search);
               

                lst = Task.FromResult(_services.GetAll<Employee>(query, dbParams, commandType: CommandType.StoredProcedure)).Result;
            }
            catch (Exception ex) { }

            return lst;
        }

        public async Task<List<Models.Department>> GetAllDepartment(string DeptID, string Search)
        {
            var query = @"Select * FROM Departments WHERE 1=1";
            query += " AND (@DeptID='' OR DeptID=@DeptID)";
            query += " AND (@Search='' OR DeptName LIKE '%@Search%')";

            var lst = new List<Models.Department>();

            try
            {
                var dbParams = new DynamicParameters();
                dbParams.Add("@DeptID", DeptID);
                dbParams.Add("@Search", Search);


                lst = Task.FromResult(_services.GetAll<Models.Department>(query, dbParams, commandType: CommandType.Text)).Result;
            }
            catch (Exception ex) { }

            return lst;
        }

        public async Task<Employee> Add(Employee model)
        {

            try
            {
                model.DATAAREAID = _services.DATAAREAID();

                var dbParams = new DynamicParameters();
                Type classType = typeof(Employee);                
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

                var query = "INSERT INTO EmplTable(" + fieldList.Trim(',') + ",EmplRefID)  OUTPUT INSERTED.RecID VALUES(" + fieldData.Trim(',') + ",NEWID())";
                model.RecID = Task.FromResult(_services.ExcuteScaler<Employee>(query, dbParams, commandType: CommandType.Text)).Result;

            }
            catch (Exception ex) { }

            return model;

        }

        public async Task<Employee> Update(Employee model)
        {
            try
            {
                var dbParams = new DynamicParameters();
                Type classType = typeof(Employee);

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
                        if(fieldName != "RecID")
                        {
                            updateData += fieldName + "=@" + fieldName + ",";
                        }
                                                                     
                        dbParams.Add("@" + fieldName, propertyInfo.GetValue(model));                       
                    }
                }

                var query = "UPDATE EmplTable SET " + updateData.Trim(',') + " WHERE RecID=@RecID";

                var res = Task.FromResult(_services.ExcuteScaler<Employee>(query, dbParams, commandType: CommandType.Text)).Result;

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
                var query = "DELETE FROM EmplTable WHERE RecID=@RecID";

                dbParams.Add("@RecID", id);

                res = Task.FromResult(_services.ExcuteScaler<Employee>(query, dbParams, commandType: CommandType.Text)).Result;
            }
            catch (Exception ex) { }

            return res;
        }

    }
}
