namespace Domain.Entities.UNS
{
    public class UnsOrderOperationstMap
    { 
        public UnsOrder UnsOrder { get; set; }
        public string UnsOrderId { get; set; }

        public OperationsInstruction OperationsInstruction { get; set; }
        public string OperationsInstructionId { get; set; }
    }
}
