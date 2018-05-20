using System;

/*
3.3.8. MR RDATA format (EXPERIMENTAL)

    +--+--+--+--+--+--+--+--+--+--+--+--+--+--+--+--+
    /                   NEWNAME                     /
    /                                               /
    +--+--+--+--+--+--+--+--+--+--+--+--+--+--+--+--+

where:

NEWNAME         A <domain-name> which specifies a mailbox which is the
                proper rename of the specified mailbox.

MR records cause no additional section processing.  The main use for MR
is as a forwarding entry for a user who has moved to a different
mailbox.
*/

namespace Ubiety.Dns.Core.Records
{
    /// <summary>
    ///     Mailbox rename DNS record
    /// </summary>
    public class RecordMr : Record
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="RecordMr" /> class
        /// </summary>
        /// <param name="rr"><see cref="RecordReader" /> for the record data</param>
        public RecordMr(RecordReader rr)
        {
            this.NewName = rr.ReadDomainName();
        }

        /// <summary>
        ///     Gets or sets the new name
        /// </summary>
        public String NewName { get; set; }

        /// <summary>
        ///     String representation of the record data
        /// </summary>
        /// <returns>Rename domain from the record</returns>
        public override String ToString()
        {
            return this.NewName;
        }
    }
}
