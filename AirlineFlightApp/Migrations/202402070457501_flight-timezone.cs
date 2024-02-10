namespace AirlineFlightApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class flighttimezone : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Flights", "TimeZoneFrom", c => c.String());
            AddColumn("dbo.Flights", "TimeZoneTo", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Flights", "TimeZoneTo");
            DropColumn("dbo.Flights", "TimeZoneFrom");
        }
    }
}
