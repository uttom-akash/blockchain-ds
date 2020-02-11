using System.Collections.Generic;
using System.Linq;
namespace chain
{
    public class MarkleRoot
    {
        public byte[] root;
        public MarkleRoot(List<Transaction> transactions)
        {
            Transactions = transactions;
            root=GetRoot();
        }
        public List<Transaction> Transactions { get; }
        public byte[] Root=>root; 

        public byte[] GetRoot(){
            Queue<byte[]> leaves=new Queue<byte[]>();
            foreach (var transaction in Transactions)
            {
                leaves.Enqueue(transaction.GetHash());
            }

            while (leaves.Count>1)
            {
                var left=leaves.Dequeue();
                var right=leaves.Dequeue();
                var merged=left.Union(right).ToArray();
                var parent=Hash.ComputeSha256(merged);
                leaves.Enqueue(parent);
            }

            return leaves.Dequeue();
        }

    }
}