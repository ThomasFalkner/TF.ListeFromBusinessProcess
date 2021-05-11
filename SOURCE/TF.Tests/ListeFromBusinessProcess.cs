using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace TF.Tests
{
    [TestClass]
    public class ListeFromBusinessProcess
    {
        [TestMethod]
        public void ItServiceAvaible()
        {
            //Daten für 16 Bundesländer
            Assert.IsTrue(TF.ListeFromBusinessProcess.RkiData.GetData().Count == 16);
        }
    }
}
