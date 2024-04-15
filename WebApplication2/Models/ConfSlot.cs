using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication2.Models
{
    public class ConfSlot
    {
        private SqlDataProvider _dataProvider;

        public ConfSlot()
        {
            _dataProvider = new SqlDataProvider();
        }

        public int ID { get; set; }
        public string Priest { get; set; }
        public string Date { get; set; }
        public string fromDate { get; set; }
        public string toDate { get; set; }
        public bool? isDeleted { get; set; }
        public bool? isBooked { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime BookedDate { get; set; }
        public DateTime DeleteDate { get; set; }

        public List<ConfSlot> GetConfSlotbyPriest()
        {
            var list = _dataProvider.GetConfSlotbyPriest();
            return list;
        }
        public List<ConfSlot> GetAllConfSlot()
        {
            var list = _dataProvider.GetAllConfSlot();
            return list;
        }
        public int DeleteConfSlot(int ID)
        {
            var result = _dataProvider.DeleteConfSlot(ID);
            return result;
        }
        public bool UpdateConfSlot(int ID)
        {
            var result = _dataProvider.UpdateConfSlot(ID);
            return Convert.ToBoolean(result);
        }

        public int InserConfSlotsConfSlot(ConfSlot Data)
        {
            var result = _dataProvider.InserConfSlots(Data);
            return result;
        }
        public ConfSlot GetConfSlotbyID(int ID)
        {
            var result = _dataProvider.GetConfSlotbyID(ID);
            return result;
        }
    }
}