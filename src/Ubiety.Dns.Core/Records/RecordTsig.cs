/*
 *      Copyright (C) 2020 Dieter (coder2000) Lunn
 *
 *      This program is free software: you can redistribute it and/or modify
 *      it under the terms of the GNU General Public License as published by
 *      the Free Software Foundation, either version 3 of the License, or
 *      (at your option) any later version.
 *
 *      This program is distributed in the hope that it will be useful,
 *      but WITHOUT ANY WARRANTY; without even the implied warranty of
 *      MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
 *      GNU General Public License for more details.
 *
 *      You should have received a copy of the GNU General Public License
 *      along with this program.  If not, see <https://www.gnu.org/licenses/>.
 */

using System;
using System.Collections.Generic;
using System.Globalization;

/*
 * http://www.ietf.org/rfc/rfc2845.txt
 *
 * Field Name       Data Type      Notes
      --------------------------------------------------------------
      Algorithm Name   domain-name    Name of the algorithm
                                      in domain name syntax.
      Time Signed      u_int48_t      seconds since 1-Jan-70 UTC.
      Fudge            u_int16_t      seconds of error permitted
                                      in Time Signed.
      MAC Size         u_int16_t      number of octets in MAC.
      MAC              octet stream   defined by Algorithm Name.
      Original ID      u_int16_t      original message ID
      Error            u_int16_t      expanded RCODE covering
                                      TSIG processing.
      Other Len        u_int16_t      length, in octets, of
                                      Other Data.
      Other Data       octet stream   empty unless Error == BADTIME

 */

namespace Ubiety.Dns.Core.Records
{
    /// <summary>
    ///     Transaction signature DNS record.
    /// </summary>
    public class RecordTsig : Record
    {
        private readonly byte[] _mac;
        private readonly byte[] _otherData;

        /// <summary>
        ///     Initializes a new instance of the <see cref="RecordTsig" /> class.
        /// </summary>
        /// <param name="reader"><see cref="RecordReader" /> for the record data.</param>
        public RecordTsig(RecordReader reader)
            : base(reader)
        {
            AlgorithmName = Reader.ReadDomainName();
            TimeSigned = (Reader.ReadUInt32() << 32) | Reader.ReadUInt32();
            Fudge = Reader.ReadUInt16();
            MacSize = Reader.ReadUInt16();
            _mac = Reader.ReadBytes(MacSize);
            OriginalId = Reader.ReadUInt16();
            Error = Reader.ReadUInt16();
            OtherLength = Reader.ReadUInt16();
            _otherData = Reader.ReadBytes(OtherLength);
        }

        /// <summary>
        ///     Gets or sets the algorithm name.
        /// </summary>
        public string AlgorithmName { get; set; }

        /// <summary>
        ///     Gets or sets the time signed.
        /// </summary>
        public long TimeSigned { get; set; }

        /// <summary>
        ///     Gets or sets the number of seconds of error.
        /// </summary>
        public ushort Fudge { get; set; }

        /// <summary>
        ///     Gets or sets the MAC size.
        /// </summary>
        public ushort MacSize { get; set; }

        /// <summary>
        ///     Gets the MAC.
        /// </summary>
        public List<byte> Mac => new List<byte>(_mac);

        /// <summary>
        ///     Gets or sets the original id.
        /// </summary>
        public ushort OriginalId { get; set; }

        /// <summary>
        ///     Gets or sets the record error.
        /// </summary>
        public ushort Error { get; set; }

        /// <summary>
        ///     Gets or sets the length of other data.
        /// </summary>
        public ushort OtherLength { get; set; }

        /// <summary>
        ///     Gets the other record data.
        /// </summary>
        public List<byte> OtherData => new List<byte>(_otherData);

        /// <summary>
        ///     String representation of the record data.
        /// </summary>
        /// <returns>Signature as a string.</returns>
        public override string ToString()
        {
            var dateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0);
            dateTime = dateTime.AddSeconds(TimeSigned);
            var printDate = dateTime.ToShortDateString() + " " + dateTime.ToShortTimeString();
            return string.Format(
                CultureInfo.InvariantCulture,
                "{0} {1} {2} {3} {4}",
                AlgorithmName,
                printDate,
                Fudge,
                OriginalId,
                Error);
        }
    }
}
