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
using System.Net;
using System.Text;
using Ubiety.Dns.Core.Common;

namespace Ubiety.Dns.Core
{
    /// <summary>
    ///     DNS Question record.
    /// </summary>
    public class Question
    {
        private string _questionName;

        /// <summary>
        ///     Initializes a new instance of the <see cref="Question" /> class.
        /// </summary>
        /// <param name="questionName">Query name.</param>
        /// <param name="questionType">Question type.</param>
        /// <param name="questionClass">Question class.</param>
        public Question(string questionName, QuestionType questionType, QuestionClass questionClass)
        {
            QuestionName = questionName;
            QuestionType = questionType;
            QuestionClass = questionClass;
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="Question" /> class.
        /// </summary>
        /// <param name="rr"><see cref="RecordReader" /> of the record.</param>
        internal Question(RecordReader rr)
        {
            QuestionName = rr.ReadDomainName();
            QuestionType = (QuestionType)rr.ReadUInt16();
            QuestionClass = (QuestionClass)rr.ReadUInt16();
        }

        /// <summary>
        ///     Gets the question name.
        /// </summary>
        public string QuestionName
        {
            get => _questionName;

            private set
            {
                _questionName = value;
                if (!_questionName.EndsWith(".", StringComparison.InvariantCulture))
                {
                    _questionName += ".";
                }
            }
        }

        /// <summary>
        ///     Gets the query type.
        /// </summary>
        public QuestionType QuestionType { get; }

        /// <summary>
        ///     Gets the query class.
        /// </summary>
        public QuestionClass QuestionClass { get; }

        /// <summary>
        ///     String representation of the question.
        /// </summary>
        /// <returns>String of the question.</returns>
        public override string ToString()
        {
            return $"{QuestionName,-32}\t{QuestionClass}\t{QuestionType}";
        }

        /// <summary>
        ///     Gets the question as a byte array.
        /// </summary>
        /// <returns>Byte array of the question data.</returns>
        public IEnumerable<byte> GetData()
        {
            var data = new List<byte>();
            data.AddRange(WriteName(QuestionName));
            data.AddRange(WriteShort((ushort)QuestionType));
            data.AddRange(WriteShort((ushort)QuestionClass));
            return data.ToArray();
        }

        private static IEnumerable<byte> WriteName(string src)
        {
            if (!src.EndsWith(".", StringComparison.InvariantCulture))
            {
                src += ".";
            }

            if (src == ".")
            {
                return new byte[1];
            }

            var sb = new StringBuilder();
            int intI, intJ, intLen = src.Length;
            sb.Append('\0');
            for (intI = 0, intJ = 0; intI < intLen; intI++, intJ++)
            {
                sb.Append(src[intI]);
                if (src[intI] != '.')
                {
                    continue;
                }

                sb[intI - intJ] = (char)(intJ & 0xff);
                intJ = -1;
            }

            sb[sb.Length - 1] = '\0';
            return Encoding.ASCII.GetBytes(sb.ToString());
        }

        private static IEnumerable<byte> WriteShort(ushort value)
        {
            return BitConverter.GetBytes(IPAddress.HostToNetworkOrder((short)value));
        }
    }
}