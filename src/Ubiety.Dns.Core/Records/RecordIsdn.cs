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

/* http://tools.ietf.org/rfc/rfc1183.txt

3.2. The ISDN RR

   The ISDN RR is defined with mnemonic ISDN and type code 20 (decimal).

   An ISDN (Integrated Service Digital Network) number is simply a
   telephone number.  The intent of the members of the CCITT is to
   upgrade all telephone and data network service to a common service.

   The numbering plan (E.163/E.164) is the same as the familiar
   international plan for POTS (an un-official acronym, meaning Plain
   Old Telephone Service).  In E.166, CCITT says "An E.163/E.164
   telephony subscriber may become an ISDN subscriber without a number
   change."

   ISDN has the following format:

   <owner> <ttl> <class> ISDN <ISDN-address> <sa>

   The <ISDN-address> field is required; <sa> is optional.

   <ISDN-address> identifies the ISDN number of <owner> and DDI (Direct
   Dial In) if any, as defined by E.164 [8] and E.163 [7], the ISDN and
   PSTN (Public Switched Telephone Network) numbering plan.  E.163
   defines the country codes, and E.164 the form of the addresses.  Its
   format in master files is a <character-string> syntactically
   identical to that used in TXT and HINFO.

   <sa> specifies the subaddress (SA).  The format of <sa> in master
   files is a <character-string> syntactically identical to that used in
   TXT and HINFO.

   The format of ISDN is class insensitive.  ISDN RRs cause no
   additional section processing.

   The <ISDN-address> is a string of characters, normally decimal
   digits, beginning with the E.163 country code and ending with the DDI
   if any.  Note that ISDN, in Q.931, permits any IA5 character in the
   general case.

   The <sa> is a string of hexadecimal digits.  For digits 0-9, the
   concrete encoding in the Q.931 call setup information element is
   identical to BCD.

   For example:

   Relay.Prime.COM.   IN   ISDN      150862028003217
   sh.Prime.COM.      IN   ISDN      150862028003217 004

   (Note: "1" is the country code for the North American Integrated
   Numbering Area, i.e., the system of "area codes" familiar to people
   in those countries.)

   The RR data is the ASCII representation of the digits.  It is encoded
   as one or two <character-string>s, i.e., count followed by
   characters.

   CCITT recommendation E.166 [9] defines prefix escape codes for the
   representation of ISDN (E.163/E.164) addresses in X.121, and PSDN
   (X.121) addresses in E.164.  It specifies that the exact codes are a
   "national matter", i.e., different on different networks.  A host
   connected to the ISDN may be able to use both the X25 and ISDN
   addresses, with the local prefix added.


 */

namespace Ubiety.Dns.Core.Records
{
    /// <summary>
    ///     ISDN DNS record.
    /// </summary>
    public class RecordIsdn : Record
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="RecordIsdn" /> class.
        /// </summary>
        /// <param name="reader"><see cref="RecordReader" /> for the record data.</param>
        public RecordIsdn(RecordReader reader)
            : base(reader)
        {
            IsdnAddress = Reader.ReadString();
            SA = Reader.ReadString();
        }

        /// <summary>
        ///     Gets or sets the ISDN address.
        /// </summary>
        public string IsdnAddress { get; set; }

        /// <summary>
        ///     Gets or sets SA.
        /// </summary>
        public string SA { get; set; }

        /// <summary>
        ///     String representation of the record data.
        /// </summary>
        /// <returns>String version of the data.</returns>
        public override string ToString()
        {
            return $"{IsdnAddress} {SA}";
        }
    }
}
