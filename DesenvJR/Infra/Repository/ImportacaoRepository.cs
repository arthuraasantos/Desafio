using Infra.Entity;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using Tools.Imports;
using System.IO;
using System.Collections.Generic;
using Tools.Utils;

namespace Infra.Repository
{
    public static class ImportacaoRepository
    {

        public static List<Transacao> LerArquivoOFX(string url)
        {
            XElement doc = ImportacaoOFX.toXElement(url);
            DataSet d = new DataSet();
            List<Transacao> listaTransacoes = new List<Transacao>();


            var imps = (from c in doc.Descendants("STMTTRN")
                        select new
                        {
                            Tipo = c.Element("TRNTYPE").Value,
                            Amount = decimal.Parse(c.Element("TRNAMT").Value.Replace(",",".")),
                            Data = DateTime.ParseExact(c.Element("DTPOSTED").Value,
                                                       "yyyyMMdd", null),
                            Description = c.Element("MEMO").Value,
                            FitId = c.Element("FITID").Value
                        });

            var lista = imps.ToList();

            foreach (var item in lista)
            {
                Transacao t = new Transacao();
                if (item.Amount > 0)
                    t.Type = "CREDIT";
                else 
                    t.Type = "DEBIT";
                
                t.Amount = item.Amount;
                t.FitId = Convert.ToInt64(item.FitId);
                t.Data = item.Data;
                t.Description = item.Description;
                
                listaTransacoes.Add(t);
            }
            
            return listaTransacoes;

        }
    }
}
