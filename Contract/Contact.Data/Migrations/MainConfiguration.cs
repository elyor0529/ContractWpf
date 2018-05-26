using System.Data.Entity.Migrations;
using System.Text;
using Contract.Data.Models;
using Contract.Data.Models.Enums;

namespace Contract.Data.Migrations
{
    internal sealed class MainConfiguration : DbMigrationsConfiguration<MainContext>
    {
        public MainConfiguration()
        {
            AutomaticMigrationsEnabled = false; 
        }

        protected override void Seed(MainContext context)
        {
            var departments = new Department[]
            {
                new Department {Name = "Col27", Code = DepartmentCode.Col27, Description = "топография"},
                new Department {Name = "Col28", Code = DepartmentCode.Col28, Description = "геология"},
                new Department {Name = "Col29", Code = DepartmentCode.Col29, Description = "геодезия"},
                new Department {Name = "Col30", Code = DepartmentCode.Col30, Description = "проектная группа"},
                new Department {Name = "Col31", Code = DepartmentCode.Col31, Description = "Прочие"}
            };
            context.Departments.AddOrUpdate(a => a.Id, departments);

            var periods = new Period[]
            {
                new Period {Title = "1 месяц"},
                new Period {Title = "2 месяц"},
                new Period {Title = "3 месяц"},
                new Period {Title = "4 месяц"},
                new Period {Title = "5 месяц"},
                new Period {Title = "6 месяц"},
                new Period {Title = "7 месяц"},
                new Period {Title = "8 месяц"},
                new Period {Title = "9 месяц"},
                new Period {Title = "10 месяц"},
                new Period {Title = "11 месяц"},
                new Period {Title = "12 месяц"}
            };
            context.Periods.AddOrUpdate(a => a.Id, periods);

            var workTypes = new WorkType[]
            {
                new WorkType
                {
                    Code = WorkTypeCode.Col13,
                    Name = "Col13",
                    Description = "топография, без НДС"
                },
                new WorkType
                {
                    Code = WorkTypeCode.Col14,
                    Name = "Col14",
                    Description = "геология, без НДС"
                },
                new WorkType
                {
                    Code = WorkTypeCode.Col15,
                    Name = "Col15",
                    Description = "геодезия (в т.ч. перенос в натуру,вынос красных линий и другие), с НДС"
                },
                new WorkType
                {
                    Code = WorkTypeCode.Col16,
                    Name = "Col16",
                    Description =
                        "освидетельствование котлована (в т.ч. опр.плотн.грунта в обратной засыпке пазух фунд.и в подсыпке под полы), с НДС"
                },
                new WorkType
                {
                    Code = WorkTypeCode.Col17,
                    Name = "Col17",
                    Description = "проектные работы идругие, с НДС"
                },
                new WorkType
                {
                    Code = WorkTypeCode.Col18,
                    Name = "Col18",
                    Description = "техническое обследование, с НДС"
                },
                new WorkType
                {
                    Code = WorkTypeCode.Col19,
                    Name = "Col19",
                    Description = "СМР, с НДС"
                },
                new WorkType
                {
                    Code = WorkTypeCode.Col20,
                    Name = "Col20",
                    Description = "картографические работы, с НДС"
                },
                new WorkType
                {
                    Code = WorkTypeCode.Col21,
                    Name = "Col21",
                    Description = "геоинформатика в т.ч. ГИС,ГГК и ЭГИТИ, без НДС"
                },
                new WorkType
                {
                    Code = WorkTypeCode.Col22,
                    Name = "Col22",
                    Description = "выдача архивных материалов, с НДС"
                },
                new WorkType
                {
                    Code = WorkTypeCode.Col22,
                    Name = "Col22",
                    Description = "выдача архивных материалов, с НДС"
                },
                new WorkType
                {
                    Code = WorkTypeCode.Col23,
                    Name = "Col23",
                    Description = "выдача копии топопланов, с НДС"
                },
                new WorkType
                {
                    Code = WorkTypeCode.Col24,
                    Name = "Col24",
                    Description = "прочие работы"
                }
            };
            context.WorkTypes.AddOrUpdate(a => a.Id, workTypes);

            var categories = new Category[]
            {
                new Category
                {
                    Code = CategoryCode.Page1,
                    Counter = 10,
                    Note = "Договара с прочими заказчиками"
                },
                new Category
                {
                    Code = CategoryCode.Page2,
                    Counter = 20,
                    Note = "Счет-договора с прочими заказчиками"
                }
                , new Category
                {
                    Code = CategoryCode.Page3,
                    Counter = 30,
                    Note = "Договара с ИКСЕЗами хокимятов областей"
                },
                new Category
                {
                    Code = CategoryCode.Page4,
                    Counter = 40,
                    Note = "Договара с областными филиалами ККИ"
                },
                new Category
                {
                    Code = CategoryCode.Page5,
                    Counter = 50,
                    Note = "Договара на ПСД-Л"
                }
            };
            context.Categories.AddOrUpdate(a => a.Id, categories);

            var branches = new Branch[]
            {
                new Branch
                {
                    Code = BranchCode.Brach1,
                    Title = "Центральное производство"
                },
                new Branch
                {
                    Code = BranchCode.Brach2,
                    Title = "Джизакский филиал"
                },
                new Branch
                {
                    Code = BranchCode.Brach3,
                    Title = "Наманганский филиал"
                },
                new Branch
                {
                    Code = BranchCode.Brach4,
                    Title = "Андижанский филиал"
                },
                new Branch
                {
                    Code = BranchCode.Brach5,
                    Title = "Бухаранский филиал"
                },
                new Branch
                {
                    Code = BranchCode.Brach6,
                    Title = "Кашкадарьинский филиал"
                },
                new Branch
                {
                    Code = BranchCode.Brach7,
                    Title = "Хорезмский филиал"
                },
                new Branch
                {
                    Code = BranchCode.Brach8,
                    Title = "Сурхандарьинский филиал"
                },
                new Branch
                {
                    Code = BranchCode.Brach9,
                    Title = "Навоийский филиал"
                },
                new Branch
                {
                    Code = BranchCode.Brach10,
                    Title = "Сырдарьинский филиал"
                },
                new Branch
                {
                    Code = BranchCode.Brach11,
                    Title = "Ферганский филиал"
                },
                new Branch
                {
                    Code = BranchCode.Brach12,
                    Title = "Нукусский филиал"
                },
                new Branch
                {
                    Code = BranchCode.Brach13,
                    Title = "Самаркандский филиал"
                },
                new Branch
                {
                    Code = BranchCode.Brach14,
                    Title = "Ташкентский городской филиал"
                }
            };
            context.Branches.AddOrUpdate(a => a.Id, branches);

        }
    }
}