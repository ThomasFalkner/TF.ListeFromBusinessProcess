using Sagede.OfficeLine.Shared.RealTimeData.BusinessProcesses;
using Sagede.Shared.RealTimeData.Common;
using System;
using System.Linq;

namespace TF.ListeFromBusinessProcess
{
    /// <summary>
    /// BusinessProcess - Bearbeitung von Daten in einer Liste
    /// </summary>
    /// <seealso cref="Sagede.OfficeLine.Shared.RealTimeData.BusinessProcesses.BusinessProcessBase" />
    public class FremdsystemdatenBusinessProcess : BusinessProcessBase
    {

        private RowSet zeilen;


        public FremdsystemdatenBusinessProcess()
        {
            zeilen = new RowSet();
        }

        /// <summary>
        /// Behandlung der Datensätze (Werte berechnen, Datensätze vervollständigen/löschen/kummulieren usw.)
        /// </summary>
        /// <param name="rows"></param>
        /// <returns></returns>
        protected override Sagede.Shared.RealTimeData.Common.RowSet GetData(Sagede.Shared.RealTimeData.Common.RowSet rows)
        {
            RowSet zeilen = new RowSet();

            var bundesland = string.Empty;
            if (base.CustomParameters != null)
            {
                var paramBundesland = base.CustomParameters.TryGetItem("Bundesland");
                if (null != paramBundesland)
                {
                     bundesland = paramBundesland.Value;
                }        
            }

            var rkiDaten = RkiData.GetData();
            foreach (var item in rkiDaten)
            {
                if (!string.IsNullOrEmpty(bundesland) && item.Name != bundesland)
                    continue;
                var row = new Row();
                row[nameof(item.Id)] = item.Id;
                row["Bundesland"] = item.Name;
                row["Todesfaelle"] = item.Deaths;
                row["Infizierte"] = item.Cases;
                row["Genesene"] = item.Recovered;
                row["Datum"] = DateTime.Today;
                zeilen.Add(row);
            }
            return zeilen;
        }

        /// <summary>
        /// Liefert eine Summenzeile für den BusinessProcess
        /// </summary>
        protected override Sagede.Shared.RealTimeData.Common.Row GetTotalLine(Sagede.Shared.RealTimeData.Common.RowSet originalData, Sagede.Shared.RealTimeData.Common.RowSet handledData, Sagede.Shared.RealTimeData.Common.RowSet totalLineData)
        {
            var sumRow = new Row();
            sumRow["ID"] = zeilen.Count();
            sumRow["Text"] = "Summe";
            sumRow["Betrag"] = zeilen.Sum(sum => (long)sum["Betrag"]);
            return sumRow;
        }

        /// <summary>
        /// Name des Geschäftsprozesses
        /// </summary>
        protected override string Name
        {
            get
            {
                return "DatenAusGeschäftsprozess";
            }
        }

        /// <summary>
        /// Vorbereitung der Ausführung
        /// </summary>
        protected override void Prepare()
        {
        }

        /// <summary>
        /// Validierung des Ergebnisses
        /// </summary>
        /// <param name="result"></param>
        protected override void ValidateResult(Sagede.Shared.RealTimeData.Core.BusinessProcessResult result)
        {
        }
    }
}