using System;

namespace DSM.UI.Api.Models.DatabasePortal
{
    public class EnvanterPostgres
    {
        public int ID { get; set; }
        public string Host { get; set; }
        public string Owner { get; set; }
        public string Enviroment { get; set; }
        public string IP_Address { get; set; }
        public string Service_Name { get; set; }
        public string Port { get; set; }
        public string Aciklama { get; set; }
        public int Logical_CPU { get; set; }
        public int Physical_CPU { get; set; }
        public int Memory { get; set; }
        public string Operating_System { get; set; }
        public string Postgres_Version { get; set; }
        public string MachineType { get; set; }
        public DateTime RecordDate { get; set; }
        public DateTime DeleteDate { get; set; }
        public string AktifPasif { get; set; }
        public string ASM { get; set; }

    }
}
