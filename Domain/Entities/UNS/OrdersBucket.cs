using Domain.Entities.UNS;
using Domain.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
namespace Domain.Entities
{
    public class OrdersBucket
    {
        public int Id { get; set; }

        public string UnsOrderId { get; set; }

        [ForeignKey("UnsOrderId")]
        public virtual UnsOrder UnsOrder { get; set; }
        public int RequestCount { get; set; }
        public BucketOrdersState State { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public DateTime Created {  get; set; }
        public virtual ICollection<UnsOrder> UnsOrders { get; set; }
    }
}
