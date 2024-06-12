namespace WPFCore.Test
{
    public class DBTest : IClassFixture<Context>
    {
        private readonly Context _ctx;

        public DBTest(Context ctx)
        {
            this._ctx = ctx;
        }

        [Fact]
        public async Task Should_get_motors()
        {
            var repo = _ctx.Repo;
            var lst = await repo.GetMotor();

            Assert.True(lst.Count() > 0);
        }
    }
}