using Dapper;
using DocumentFormat.OpenXml.Office2010.Excel;
using LabManagement.Infrastructure.IRespository;
using LabManagement.Models.SaleModels;
using System.Data;
using System.Diagnostics.Eventing.Reader;
using System.Reflection;

namespace LabManagement.Infrastructure.Respository
{
    public class SystemResposity : ISystemResposity
    {

        private readonly IDapperServices _services;
        
        public SystemResposity(IDapperServices services)
        {
            _services = services;
        }

       
     
        public async Task<List<LabInfo>> GetAllLabs(int RecID, string DATAAREAID, string Search)
        {
            var query = @"Select * FROM LAB_Areas WHERE 1=1";
            query += " AND (@RecID=0 OR RecID=@RecID)";
            query += " AND (@DATAAREAID='' OR DATAAREAID=@DATAAREAID)";
            query += " AND (@Search='' OR LabName = @Search)";
            var lst = new List<LabInfo>();

            try
            {
                var dbParams = new DynamicParameters();
                dbParams.Add("@RecID", RecID);
                dbParams.Add("@DATAAREAID", DATAAREAID);
                dbParams.Add("@Search", Search);
                
                lst = Task.FromResult(_services.GetAll<LabInfo>(query, dbParams, commandType: CommandType.Text)).Result;
            }
            catch (Exception ex) { }
            return lst;
        }

        public async Task<LabInfo> SaveLab(LabInfo model)
        {
            var query="";
            if (model.RecID == 0){
                query = "IF NOT EXISTS(Select LabName FROM LAB_Areas WHERE LabName=@LabName)";
                query += " INSERT INTO LAB_Areas(DATAAREAID,LabName,LabAddress,Tel,Website,Email) ";
                query += " OUTPUT INSERTED.RecID VALUES(NewID(),@LabName,@LabAddress,@Tel,@Website,@Email)";
            }
            else{
                query = "UPDATE LAB_Areas SET LabName=@LabName,LabAddress=@LabAddress,Tel=@Tel,Website=@Website,Email=@Email WHERE RecID=@RecID";
            }

             var dbParams = new DynamicParameters();
            dbParams.Add("@RecID", model.RecID);
            dbParams.Add("@DATAAREAID", model.DATAAREAID);
            dbParams.Add("@LabName", model.LabName);
            dbParams.Add("@LabAddress", model.LabAddress);
            dbParams.Add("@Tel", model.Tel);
            dbParams.Add("@Website", model.Website);
            dbParams.Add("@Email", model.Email);

            var RecID = Task.FromResult(_services.ExcuteScalerObject<LabInfo>(query, dbParams, commandType: CommandType.Text)).Result;
            if(RecID !=null){
                model.RecID = int.Parse(RecID.ToString()!);
            }
            return model;
        }

        public async Task<int> DeleteLab(int RecID)
        {
            var res = 0;
            try
            {
                var dbParams = new DynamicParameters();
                var query = "DELETE FROM LAB_Areas WHERE RecID=@RecID";
                
                dbParams.Add("@RecID", RecID);
               
                res = Task.FromResult(_services.ExcuteScaler<LabInfo>(query, dbParams, commandType: CommandType.Text)).Result;
            }
            catch (Exception ex) { }
            return res;
        }
    }
}
