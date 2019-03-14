namespace BillsPaymentSystem.App.Core
{
    using Data;
    using Models;

    public class Engine
    {
        public void Run()
        {
            using (var context = new BillsPaymentSystemContext())
            {
                DbSeeder seeder = new DbSeeder(context);
                seeder.Seed();
                
                DbController controller = new DbController(context);
                controller.Read();
                controller.PayBills();
            }
        }
    }
}
