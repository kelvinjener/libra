using System;
using System.Collections.Generic;
using System.Data.Linq;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Libra.Entity
{
    partial class LibraDataContext
    {
        partial void OnCreated()
        {
            this.Connection.ConnectionString = FactoryConnection.GetConnectionStringLibra();
            this.CommandTimeout = 360;
        }

        public override void SubmitChanges(ConflictMode failureMode)
        {
            ChangeSet changeSet = this.GetChangeSet();

            LoginUser loginUser = new LoginUser();
            LogLoginUser logUser = new LogLoginUser();

            base.SubmitChanges(failureMode);
        }
    }
}
