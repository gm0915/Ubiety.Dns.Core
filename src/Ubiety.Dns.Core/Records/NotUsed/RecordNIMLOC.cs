using System;

namespace Ubiety.Dns.Core.Records.NotUsed
{
        /// <summary>
        /// </summary>
    public class RecordNIMLOC : Record
    {
        /// <summary>
        /// </summary>
        public byte[] RDATA { get; set; }

        /// <summary>
        /// </summary>
        public RecordNIMLOC(RecordReader rr)
        {
            // re-read length
            ushort RDLENGTH = rr.ReadUInt16(-2);
            RDATA = rr.ReadBytes(RDLENGTH);
        }

        /// <summary>
        /// </summary>
        public override string ToString()
        {
            return "not-used";
        }

    }
}