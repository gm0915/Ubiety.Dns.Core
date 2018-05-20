using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;
using Ubiety.Dns.Core.Records;

namespace Ubiety.Dns.Core
{
    /// <summary>
    ///     DNS response
    /// </summary>
    public class Response
    {
        /// <summary>
        /// List of Question records
        /// </summary>
        public List<Question> Questions;
        /// <summary>
        /// List of AnswerRR records
        /// </summary>
        public List<AnswerRR> Answers;
        /// <summary>
        /// List of AuthorityRR records
        /// </summary>
        public List<AuthorityRR> Authorities;
        /// <summary>
        /// List of AdditionalRR records
        /// </summary>
        public List<AdditionalRR> Additionals;

        /// <summary>
        /// </summary>
        public Header header;

        /// <summary>
        /// Error message, empty when no error
        /// </summary>
        public string Error;

        /// <summary>
        /// The Size of the message
        /// </summary>
        public int MessageSize;

        /// <summary>
        /// TimeStamp when cached
        /// </summary>
        public DateTime TimeStamp;

        /// <summary>
        /// Server which delivered this response
        /// </summary>
        public IPEndPoint Server;

        /// <summary>
        ///     Initializes a new instance of the <see cref="Response" /> class
        /// </summary>
        public Response()
        {
            this.Questions = new List<Question>();
            this.Answers = new List<AnswerRR>();
            this.Authorities = new List<AuthorityRR>();
            this.Additionals = new List<AdditionalRR>();

            this.Server = new IPEndPoint(0,0);
            this.Error = "";
            this.MessageSize = 0;
            this.TimeStamp = DateTime.Now;
            this.header = new Header();
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="Response" /> class
        /// </summary>
        /// <param name="iPEndPoint">Address of the response</param>
        /// <param name="data">Response data</param>
        public Response(IPEndPoint iPEndPoint, byte[] data)
        {
            this.Error = "";
            this.Server = iPEndPoint;
            this.TimeStamp = DateTime.Now;
            this.MessageSize = data.Length;
            RecordReader rr = new RecordReader(data);

            this.Questions = new List<Question>();
            this.Answers = new List<AnswerRR>();
            this.Authorities = new List<AuthorityRR>();
            this.Additionals = new List<AdditionalRR>();

            this.header = new Header(rr);

            for (int intI = 0; intI < header.QuestionCount; intI++)
            {
                this.Questions.Add(new Question(rr));
            }

            for (int intI = 0; intI < header.AnswerCount; intI++)
            {
                this.Answers.Add(new AnswerRR(rr));
            }

            for (int intI = 0; intI < header.NameserverCount; intI++)
            {
                this.Authorities.Add(new AuthorityRR(rr));
            }
            for (int intI = 0; intI < header.AdditionalRecordsCount; intI++)
            {
                this.Additionals.Add(new AdditionalRR(rr));
            }
        }

        /// <summary>
        /// List of RecordMX in Response.Answers
        /// </summary>
        public RecordMx[] RecordsMX
        {
            get
            {
                List<RecordMx> list = new List<RecordMx>();
                foreach (AnswerRR answerRR in this.Answers)
                {
                    RecordMx record = answerRR.Record as RecordMx;
                    if(record!=null)
                        list.Add(record);
                }
                list.Sort();
                return list.ToArray();
            }
        }

        /// <summary>
        /// List of RecordTXT in Response.Answers
        /// </summary>
        public RecordTxt[] RecordsTXT
        {
            get
            {
                List<RecordTxt> list = new List<RecordTxt>();
                foreach (AnswerRR answerRR in this.Answers)
                {
                    RecordTxt record = answerRR.Record as RecordTxt;
                    if (record != null)
                        list.Add(record);
                }
                return list.ToArray();
            }
        }

        /// <summary>
        /// List of RecordA in Response.Answers
        /// </summary>
        public RecordA[] RecordsA
        {
            get
            {
                List<RecordA> list = new List<RecordA>();
                foreach (AnswerRR answerRR in this.Answers)
                {
                    RecordA record = answerRR.Record as RecordA;
                    if (record != null)
                        list.Add(record);
                }
                return list.ToArray();
            }
        }

        /// <summary>
        /// List of RecordPTR in Response.Answers
        /// </summary>
        public RecordPtr[] RecordsPTR
        {
            get
            {
                List<RecordPtr> list = new List<RecordPtr>();
                foreach (AnswerRR answerRR in this.Answers)
                {
                    RecordPtr record = answerRR.Record as RecordPtr;
                    if (record != null)
                        list.Add(record);
                }
                return list.ToArray();
            }
        }

        /// <summary>
        /// List of RecordCNAME in Response.Answers
        /// </summary>
        public RecordCname[] RecordsCNAME
        {
            get
            {
                List<RecordCname> list = new List<RecordCname>();
                foreach (AnswerRR answerRR in this.Answers)
                {
                    RecordCname record = answerRR.Record as RecordCname;
                    if (record != null)
                        list.Add(record);
                }
                return list.ToArray();
            }
        }

        /// <summary>
        /// List of RecordAAAA in Response.Answers
        /// </summary>
        public RecordAAAA[] RecordsAAAA
        {
            get
            {
                List<RecordAAAA> list = new List<RecordAAAA>();
                foreach (AnswerRR answerRR in this.Answers)
                {
                    RecordAAAA record = answerRR.Record as RecordAAAA;
                    if (record != null)
                        list.Add(record);
                }
                return list.ToArray();
            }
        }

        /// <summary>
        /// List of RecordNS in Response.Answers
        /// </summary>
        public RecordNs[] RecordsNS
        {
            get
            {
                List<RecordNs> list = new List<RecordNs>();
                foreach (AnswerRR answerRR in this.Answers)
                {
                    RecordNs record = answerRR.Record as RecordNs;
                    if (record != null)
                        list.Add(record);
                }
                return list.ToArray();
            }
        }

        /// <summary>
        /// List of RecordSOA in Response.Answers
        /// </summary>
        public RecordSoa[] RecordsSOA
        {
            get
            {
                List<RecordSoa> list = new List<RecordSoa>();
                foreach (AnswerRR answerRR in this.Answers)
                {
                    RecordSoa record = answerRR.Record as RecordSoa;
                    if (record != null)
                        list.Add(record);
                }
                return list.ToArray();
            }
        }

        /// <summary>
        /// List of RecordCERT in Response.Answers
        /// </summary>
        public RecordCert[] RecordsCERT
        {
            get
            {
                List<RecordCert> list = new List<RecordCert>();
                foreach (AnswerRR answerRR in this.Answers)
                {
                    RecordCert record = answerRR.Record as RecordCert;
                    if (record != null)
                        list.Add(record);
                }
                return list.ToArray();
            }
        }

        /// <summary>
        ///     List of SRV records
        /// </summary>
        public RecordSrv[] RecordsSRV
        {
            get
            {
                List<RecordSrv> list = new List<RecordSrv>();
                foreach (AnswerRR answerRR in this.Answers)
                {
                    RecordSrv record = answerRR.Record as RecordSrv;
                    if (record != null)
                        list.Add(record);
                }
                return list.ToArray();
            }
        }

        /// <summary>
        ///     List of resource records
        /// </summary>
        public ResourceRecord[] RecordsRR
        {
            get
            {
                List<ResourceRecord> list = new List<ResourceRecord>();
                foreach (ResourceRecord rr in this.Answers)
                {
                    list.Add(rr);
                }
                foreach (ResourceRecord rr in this.Answers)
                {
                    list.Add(rr);
                }
                foreach (ResourceRecord rr in this.Authorities)
                {
                    list.Add(rr);
                }
                foreach (ResourceRecord rr in this.Additionals)
                {
                    list.Add(rr);
                }
                return list.ToArray();
            }
        }
    }
}
