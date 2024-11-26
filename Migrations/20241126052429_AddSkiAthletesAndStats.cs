using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CS330_Fall2024_FinalProject.Migrations
{
    /// <inheritdoc />
    public partial class AddSkiAthletesAndStats : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            

          

            migrationBuilder.CreateTable(
                name: "SkiStats",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BestTime = table.Column<double>(type: "float", nullable: false),
                    TopSpeed = table.Column<double>(type: "float", nullable: false),
                    BestDistance = table.Column<double>(type: "float", nullable: false),
                    VerticalDrop = table.Column<double>(type: "float", nullable: false),
                    Ranking = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SkiStats", x => x.Id);
                });

            

            
            

            
            
            migrationBuilder.CreateTable(
                name: "Athletes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Number = table.Column<int>(type: "int", nullable: false),
                    StatsId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Athletes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Athletes_SkiStats_StatsId",
                        column: x => x.StatsId,
                        principalTable: "SkiStats",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

      

     



        

         

           
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            

            migrationBuilder.DropTable(
                name: "Athletes");

       

            migrationBuilder.DropTable(
                name: "SkiStats");
        }
    }
}
