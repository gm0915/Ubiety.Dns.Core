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

/*
 * http://tools.ietf.org/rfc/rfc1712.txt
 *
3. RDATA Format

        MSB                                        LSB
        +--+--+--+--+--+--+--+--+--+--+--+--+--+--+--+
        /                 LONGITUDE                  /
        +--+--+--+--+--+--+--+--+--+--+--+--+--+--+--+
        /                  LATITUDE                  /
        +--+--+--+--+--+--+--+--+--+--+--+--+--+--+--+
        /                  ALTITUDE                  /
        +--+--+--+--+--+--+--+--+--+--+--+--+--+--+--+

   where:

   LONGITUDE The real number describing the longitude encoded as a
             printable string. The precision is limited by 256 charcters
             within the range -90..90 degrees. Positive numbers
             indicate locations north of the equator.

   LATITUDE The real number describing the latitude encoded as a
            printable string. The precision is limited by 256 charcters
            within the range -180..180 degrees. Positive numbers
            indicate locations east of the prime meridian.

   ALTITUDE The real number describing the altitude (in meters) from
            mean sea-level encoded as a printable string. The precision
            is limited by 256 charcters. Positive numbers indicate
            locations above mean sea-level.

   Latitude/Longitude/Altitude values are encoded as strings as to avoid
   the precision limitations imposed by encoding as unsigned integers.
   Although this might not be considered optimal, it allows for a very
   high degree of precision with an acceptable average encoded record
   length.

 */

using Ubiety.Dns.Core.Common.Extensions;

namespace Ubiety.Dns.Core.Records.Obsolete
{
    /// <summary>
    ///     GPOS DNS record.
    /// </summary>
    public class RecordGpos : Record
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="RecordGpos" /> class.
        /// </summary>
        /// <param name="rr"><see cref="RecordReader" /> for the record data.</param>
        public RecordGpos(RecordReader rr)
        {
            var reader = rr.ThrowIfNull(nameof(rr));
            Longitude = reader.ReadString();
            Latitude = reader.ReadString();
            Altitude = reader.ReadString();
        }

        /// <summary>
        ///     Gets the longitude.
        /// </summary>
        public string Longitude { get; }

        /// <summary>
        ///     Gets the latitude.
        /// </summary>
        public string Latitude { get; }

        /// <summary>
        ///     Gets the altitude.
        /// </summary>
        public string Altitude { get; }

        /// <summary>
        ///     String representation of the position.
        /// </summary>
        /// <returns>String of the version.</returns>
        public override string ToString()
        {
            return $"{Longitude} {Latitude} {Altitude}";
        }
    }
}