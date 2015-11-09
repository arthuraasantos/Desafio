using Infra.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infra.Repository
{
    public interface ITransacaoRepository
    {
        void Save(Transacao transaction);
        void SaveList(List<Transacao> listTransaction);

        bool Check(Transacao transaction);

        List<Transacao> List();
    }
}
