using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Infra.Entity;
using System.Data.Entity;
using Tools.Utils;

namespace Infra.Repository
{
    public class TransacaoRepository : Context, ITransacaoRepository
    {
        public Context Contexto { get; set; }
        public TransacaoRepository()
        {
            Contexto = new Context();
            
        }
        public void Save(Transacao transaction)
        {
            //TODO  
        }

        public void SaveList(List<Transacao> listTransaction)
        {
            
            using (DbContextTransaction transCtrl = Contexto.Database.BeginTransaction())
            {
                try
                {
                    if (listTransaction.Count > 0)
                    {
                        foreach (var item in listTransaction)
                        {
                            bool trans = Check(item);
                            if (!trans)
                            {
                                item.Identificador = Identificador.GerarIdentificadorAutomatico(item.Type, item.Data.ToString("ddMMyyyy"), item.Amount,item.FitId.ToString());
                                Contexto.DBSetTransactions.Add(item);
                            }
                        }
                        Contexto.SaveChanges();
                        transCtrl.Commit();
                    }

                }
                catch (Exception)
                {
                    transCtrl.Rollback();
                    throw;
                }
            }
            
        }

        public bool Check(Transacao transaction)
        {
            bool retorno = false;
            try
            {
                Transacao t = new Transacao();
                string valorId = Identificador.GerarIdentificadorAutomatico(transaction.Type, transaction.Data.ToString("ddMMyyyy"), transaction.Amount,transaction.FitId.ToString());
                t = Contexto.DBSetTransactions.FirstOrDefault(a => a.Identificador == valorId);
                if (t != null)
                {
                    retorno = true;
                }

                return retorno;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public List<Transacao> List()
        {
            return Contexto.DBSetTransactions.ToList();
        }

       
    }
}
