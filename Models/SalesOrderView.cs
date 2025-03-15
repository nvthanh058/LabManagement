using Microsoft.FluentUI.AspNetCore.Components;

namespace LabManagement.Models
{
    public class SalesOrderView
    {       
        public DateTime? fromDate { get; set; } =DateTime.Now;
        public DateTime? toDate { get; set; } = DateTime.Now;

        public IQueryable<SalesTable>? items=default!;
        public IQueryable<SalesLine>? lineitems=default!;

        public IEnumerable<SalesTable> SelectedItems
        {
            get
            {
                return items.Where(p => p.Selected);
            }
        }

        public List<Option<int>> SalesStatusOptions = new()
        {
            { new Option<int> { Value = 0, Text = "Open Order"} },
            { new Option<int> { Value = 1, Text = "Invoiced" } },
            { new Option<int> { Value =2, Text = "Cancelled" } }
        };

        public SalesTable FirstOrder
        {
            get
            {
                return items.Where(p => p.Selected).FirstOrDefault();
            }
        }
    }
}
