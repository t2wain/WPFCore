using System.Data;
using System.Runtime.InteropServices;
using WPFCore.Data.Report;

namespace WPFCore.Test
{
    public class ReportTest : IClassFixture<Context>
    {
        private readonly Context _ctx;

        public ReportTest(Context ctx)
        {
            this._ctx = ctx;
        }

        [Fact]
        public async Task Should_deserialize_report()
        {
            var r = await ReportUtil.DeserializeReportDefinitionFromFile(@"C:\devgit\Data\Reports\Cable_Schedule_SPEL.xml");
            Assert.NotNull(r);
        }

        [Fact]
        public async Task Should_load_cable_schedule()
        {
            var dv = await LoadReport(@"C:\devgit\Data\Reports\Cable_Schedule_SPEL.xml");
            Assert.True(dv.Count > 0);
        }

        [Fact]
        public async Task Should_load_cable_quantity()
        {
            var dv = await LoadReport(@"C:\devgit\Data\Reports\Cable_Quantity_Per_Cable_Code.xml");
            Assert.True(dv.Count > 0);
        }

        [Fact]
        public async Task Should_get_report_column_def()
        {
            var r = await ReportUtil.DeserializeReportDefinitionFromFile(@"C:\devgit\Data\Reports\Cable_Quantity_Per_Cable_Code.xml");
            var cols = await _ctx.ReportDS.GetUpdatedColumnDefinitions(r);
            Assert.Equal(cols.Count, r.Columns!.Count);
        }

        protected async Task<DataView> LoadReport(string defxml)
        {
            var r = await ReportUtil.DeserializeReportDefinitionFromFile(defxml);
            var dv = await _ctx.ReportDS.GetReportData(r);
            return dv;
        }
    }
}
